const { User, sequelize } = require('../database/models');

module.exports = {
    getInfo: async(req, res, next) => {
        const id = req.user.id;
        const user = await User.scope('public').findOne({ where: {id} });

        res.status(200).json(user);

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

        sequelize.transaction(async transaction => {
            //Fetch user
            let user = await User.findOne({where: {id}, transaction});

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

            await user.save(transaction);

            res.status(200).json(await User.scope('public').findOne({where: {id} }));

            return Promise.resolve();
        }).then(() => {
            return Promise.resolve('next');
        }).catch(error => {
            return Promise.reject(error);
        });
    }
}