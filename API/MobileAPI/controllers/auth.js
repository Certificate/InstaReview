const { User } = require('../database/models');

module.exports = {
    signUp: async(req, res, next) => {
        console.log('AuthController.signUp() called', req.values.body);
        const { email, password } = req.values.body;
        
        //Check for existing user accounts
        if( await User.findOne({where: {email}}) ) {
            return res
                .status(409)
                .json({
                    success: false,
                    status: 'User already exists.'
                });
        }

        //Create a new user
        const newUser = User.build({
            email,
            password
        });
        await newUser.save();

        res
            .status(201)
            .json({
                success: true,
                status: 'Successfully signed up'
            });
    },

    logIn: async(req, res, next) => {
        console.log('AuthController.logIn() called');
    },

    secret: async(req, res, next) => {
        console.log('AuthController.secret() called');
    }
}