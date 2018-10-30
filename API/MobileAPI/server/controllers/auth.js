const JWT = require('jsonwebtoken');
const { User } = require('../database/models');

signToken = user => {
    return JWT.sign({
        iss: 'ReviewCollector',
        sub: user.id,
        iat: Math.round(Date.now() / 1000), //Issued at
        exp: Math.round(Date.now() / 1000) + 24*60*60 //Expiration (1 day)
    }, process.env.JWT_SECRET);
}

module.exports = {
    signUp: async(req, res, next) => {
        const { email, password } = req.values.body;
        
        //Check for existing user accounts
        if( await User.findOne({where: {email}}) ) {
            return res
                .status(409)
                .json({
                    error: 'User already exists.'
                });
        }

        //Create a new user
        const newUser = User.build({
            email,
            password
        });
        await newUser.save();
        
        //Respond with token
        const token = signToken(newUser);
        res.status(200).json({ token });
    },

    logIn: async(req, res, next) => {
        const token = signToken(req.user);
        res.status(200).json({ token });
    },

    secret: async(req, res, next) => {
        console.log('AuthController.secret() called');
        const secret = 'asd';
        res.status(200).json({ secret });
    }
}