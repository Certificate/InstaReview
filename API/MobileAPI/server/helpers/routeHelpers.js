const Joi = require('joi');

module.exports = {
    validateBody: (schema) => {
        return (req, res, next) => {
            const result = Joi.validate(req.body, schema);

            if (result.error) {
                return res.status(400).json(result.error);
            }

            if(!req.values) { req.values = {}; }
            req.values['body'] = result.value;
            next();
        };
    },

    schemas: {
        authSchema: Joi.object().keys({
            email: Joi.string().email().required(),
            password: Joi.string().required()
        }),
        reviewSchema: Joi.object().keys({
            appId: Joi.number().integer().required(),
            temporalContext: Joi.string().required().valid(['Intensive', 'Allocative']),
            spatialContext: Joi.string().required().valid(['Visiting', 'Traveling', 'Wandering']),
            socialContext: Joi.string().required().valid(['Constraining', 'Encouraging']),
            textReview: Joi.string().required()
        }),
        applicationSchema: Joi.object().keys( {
            name: Joi.string().required(),
            operatingSystem: Joi.string().required().valid(['Android', 'iOS'])
        })
    }
}