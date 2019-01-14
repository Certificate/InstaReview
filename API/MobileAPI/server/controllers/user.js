const { User, sequelize } = require('../database/models');

module.exports = {
    getInfo: async(req, res, next) => {
        const id = req.user.id;
        const user = await User.findOne({ where: {id} });

        res.status(200).json(user.getPublic());

        return Promise.resolve('next');
    },

    edit: async(req, res, next) => {
        const id = req.user.id;
        const {
            email,
            password,
            name,
            gender,
            birthday
        } = req.values.body;

        //Fetch user
        let user = await User.findOne({where: {id}});

        //Check that the object isn't null just in case
        if(!user) {
            let error = 'Could not retrieve user';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Update values
        user = Object.assign(user, {
            email: email || user.email,
            name: name || user.name,
            gender: gender || user.gender,
            birthday: birthday || user.birthday
        });

        //Password needs to be handled a bit differently because of hashing
        if(password) {
            user.password = password;
        }

        await user.save();

        res.status(200).json(user.getPublic());

        return Promise.resolve('next');
    }
}