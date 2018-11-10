const { Application } = require('../database/models');

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
    }
}