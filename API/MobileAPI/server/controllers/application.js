const { Application, Sequelize } = require('../database/models');

module.exports = {
    add: async (req, res, next) => {
        const {
            name,
            operatingSystem
        } = req.values.body;

        //Check that it doesn't already exist
        if( await Application.findOne({where: {name, operatingSystem}}) ) {
            let error = 'That application already exists for that operating system.';
            res
                .status(403)
                .json({
                    error
                });
            return Promise.reject(error);
        }

        const newApplication = Application.build({
            name,
            operatingSystem
        });
        await newApplication.save();

        res.status(200).json(newApplication.toJSON());

        return Promise.resolve('next');
    },

    listAll: async(req, res, next) => {
        let applications = await Application.scope('public').findAll();
        applications = applications.map(application => {
            return application.toJSON();
        });

        res.status(200).json(applications);
        
        return Promise.resolve('next');
    },

    findApps: async(req, res, next) => {
        const operators = Sequelize.Op;
        const searchParam = req.params.searchParam;
        
        let applications = [];
        if(searchParam) {
            //Param given
            applications = await Application.scope('public').findAll({
                where: {
                    [operators.or]: [
                        { id: searchParam },
                        { name: { [operators.like]: '%' + searchParam + '%' } },
                        { operatingSystem: { [operators.like]: '%' + searchParam + '%' } }
                    ]
                }
            });
        } else {
            //No param, find all
            applications = await Application.scope('public').findAll();
        }

        //Convert list to json
        applications = applications.map(application => {
            return application.toJSON();
        });

        res.status(200).json(applications);

        return Promise.resolve('next');
    }
}