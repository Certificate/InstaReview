const { Review, Application, Image } = require('../database/models');
const fs = require('fs');

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
            res.status(400)
                .json({error});
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
            res.status(400)
                .json({error});
            return Promise.reject(error);
        }

        const review = await Review.findOne({where: {id, userId: req.user.id}, include: ['images']});
        if(!review) {
            let error = 'Could not find a review with given id and credentials';
            res.status(404)
                .json({error})
            return Promise.reject(error);
        }

        res.status(200).json(review.toJSON());

        return Promise.resolve('next');
    },

    fetchAll: async(req, res, next) => {
        let reviews = await Review.findAll({where: {userId: req.user.id}});
        if(!reviews) {
            res.status(200)
                .json([]);
            return Promise.resolve('next');
        }

        reviews = reviews.map(review => {
            return review.toJSON();
        });

        res.status(200).json(reviews);

        return Promise.resolve('next');
    },

    imageUpload: async(req, res, next) => {
        const image = req.file;
        const { reviewId } = req.body;

        if(!image) {
            let error = 'No image received.'
            res.status(400)
                .json({error});
            return Promise.reject(error);
        }

        if(!reviewId || (reviewId && !await Review.findOne({where: {id: reviewId}}))) {
            //Remove the image, since we don't want to save it if we can't link it to a review
            await fs.unlink(image.destination + image.filename);

            let error = 'No review id was given or couldn\'t find a review with given id.';
            res.status(404)
                .json({error});
            return Promise.reject(error);
        }

        const newImage = Image.build({
            reviewId,
            fileName: image.filename
        });
        await newImage.save();

        res.status(200).json(newImage.toJSON());

        return Promise.resolve('next');
    },

    imageDownload: async(req, res, next) => {
        const fileName = req.params.filename;
        if(!fileName) {
            let error = 'Filename missing!';
            res.status(400)
                .json({error});
            return Promise.reject(error);
        }

        //Retrieve the image from the db
        const image = await Image.findOne({where: {fileName}});
        if(!image) {
            let error = 'Image doesn\'t exist';
            res.status(404)
                .json({error});
            return Promise.reject(error);
        }

        //Retrieve the corresponding review from db
        const review = await Review.findOne({where : {id: image.reviewId}});
        if(!review) {
            let error = 'Couldn\'t find a review containing the image';
            res.status(404)
                .json({error});
            return Promise.reject(error);
        }

        //Check that the user is the one that also created/owns the review
        if(review.userId !== req.user.id) {
            let error = 'No permissions to access the file';
            res.status(403)
                .json({error});
            return Promise.reject(error);
        }

        const saveDir = process.env.IMAGE_SAVE_DIR || './review_images/';
        res.download(saveDir + image.fileName, function(err) {
            if(err) {
                console.log('Failed to download image:', err);
                res.status(404).json({
                    error: "Failed to retrieve the image file"
                });
                return Promise.reject(err);
            } else {
                res.status(200);
                return Promise.resolve('next');
            }
        });
    }
};