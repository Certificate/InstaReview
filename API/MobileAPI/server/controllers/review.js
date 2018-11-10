const { Review, Application} = require('../database/models');

module.exports = {
    create: async(req, res, next) => {
        const {
            appId,
            temporalContext,
            spatialContext,
            socialContext,
            textReview
        } = req.values.body;

        //Check that there's data for the application with given appId
        if( !await Application.findOne({where: {id: appId} }) ) {
            let error = 'Could not find application data with given appId';
            res
                .status(400)
                .json({
                    error
                });
            return Promise.reject(error);
        }

        //Build a new review
        const newReview = Review.build({
            userId: req.user.id,
            appId,
            temporalContext,
            spatialContext,
            socialContext,
            textReview
        });
        await newReview.save();

        res.status(200).json(newReview.toJSON());

        return Promise.resolve('next');
    },

    fetch: async(req, res, next) => {
        const id = req.params.id;
        if(!id) {
            let error = 'No review id given!';
            res
                .status(400)
                .json({
                   error
                });
            return Promise.reject(error);
        }

        const review = await Review.findOne({where: {id, userId: req.user.id}});
        if(!review) {
            let error = 'Could not find a review with given id and credentials';
            res
                .status(404)
                .json({
                    error
                })
            return Promise.reject(error);
        }

        res.status(200).json(review.toJSON());

        return Promise.resolve('next');
    }
};