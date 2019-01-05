const { Review, ReviewCategory, Application, Image, Thumbnail, sequelize } = require('../database/models');
const fs = require('fs');
const sharp = require('sharp');
sharp.cache(false);

const saveDir = process.env.IMAGE_SAVE_DIR || './review_images/';
const thumbnailDir = process.env.THUMBNAIL_SAVE_DIR || './thumbnails/';
const thumbnailSize = parseInt(process.env.THUMBNAIL_SIZE) || 150;

const generateThumbnail = async (image, review) => {
    let fileName = 'thumbnail-' + review.id + '.png';
    try {
        let thumbnailImage = await sharp(saveDir + image.fileName)
            .resize(thumbnailSize, thumbnailSize, { fit: "inside" })
            .png()
            .toFile(thumbnailDir + fileName);
        
        await sequelize.transaction(async transaction => {
            let thumbnail = Thumbnail.build({
                fileName
            });
            await thumbnail.save({transaction});

            review.thumbnailId = thumbnail.id;
            await review.save({transaction});

            return Promise.resolve();
        });


    } catch(error) {
        //Remove the image in case of an error
        await fs.unlink(thumbnailDir + fileName, error => {
        });
        throw new Error('Failed to create a thumbnail: ' + error);
    }
}

module.exports = {
    create: async(req, res, next) => {
        const {
            appId,
            categoryName,
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
            return Promise.reject(new Error(error));
        }

        //Fetch category
        let category = await ReviewCategory.findOne({where: {categoryName}});
        if(!category) {
            let error = 'Could not find a corresponding category for the review';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Build a new review
        let newReview = Review.build({
            userId: req.user.id,
            appId,
            categoryId: category.id,
            temporalContext,
            spatialContext,
            socialContext,
            textReview
        });
        await newReview.save();
        newReview = await newReview.reload({include: ['category', 'application']});

        res.status(200).json(newReview.toJSON());

        return Promise.resolve('next');
    },

    edit: async(req, res, next) => {
        const {
            id,
            categoryName,
            appId,
            temporalContext,
            spatialContext,
            socialContext,
            textReview
        } = req.values.body;

        //Check that id was given
        if (!id) {
            let error = 'No id for the review was given for it to be edited';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Fetch the review for editing
        let review = await Review.findOne({where: {id, userId: req.user.id}});

        if(!review) {
            let error = 'Could not find a review with given id and credentials';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Check that there's data for the application with given appId
        if( !await Application.findOne({where: {id: appId}}) ) {
            let error = 'Could not find application data with given appId';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        let category = await ReviewCategory.findOne({where: {categoryName}});
        if(!category) {
            let error = 'Could not find a corresponding category for the review';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Update values
        review = Object.assign(review, { appId, categoryId: category.id, temporalContext, spatialContext, socialContext, textReview })
        await review.save();
        review = await review.reload({include: ['category', 'application']});

        res.status(200).json(review);

        return Promise.resolve('next');
    },

    fetch: async(req, res, next) => {
        const id = req.params.id;
        if(!id) {
            let error = 'No review id given!';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        const review = await Review.findOne({where: {id, userId: req.user.id}, include: ['application', 'category', 'images', 'thumbnail']});
        if(!review) {
            let error = 'Could not find a review with given id and credentials';
            res.status(404)
                .json({error})
            return Promise.reject(new Error(error));
        }

        res.status(200).json(review.toJSON());

        return Promise.resolve('next');
    },

    fetchAll: async(req, res, next) => {
        let reviews = await Review.findAll({where: {userId: req.user.id}, include: ['application', 'category', 'thumbnail']});
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
            return Promise.reject(new Error(error));
        }

        let review = await Review.findOne({where: {id: reviewId, userId: req.user.id}});
        if(!review) {
            //Remove the image, since we don't want to save it if we can't link it to a review
            await fs.unlink(image.destination + image.filename, error => {
            });

            let error = 'No review id was given or couldn\'t find a review with given id.';
            res.status(404)
                .json({error});
            return Promise.reject(new Error(error));
        }

        const newImage = Image.build({
            reviewId,
            fileName: image.filename
        });
        await newImage.save();

        res.status(200).json(newImage.toJSON());

        //Generate thumbnail if it doesn't exist
        if(!review.thumbnailId) {
            await generateThumbnail(newImage, review);
        }

        return Promise.resolve('next');
    },

    imageDownload: async(req, res, next) => {
        const fileName = req.params.filename;
        if(!fileName) {
            let error = 'Filename missing!';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Retrieve the image from the db
        const image = await Image.findOne({where: {fileName}});
        if(!image) {
            let error = 'Image doesn\'t exist';
            res.status(404)
                .json({error});
            return Promise.reject(new Error(error));
        }

        //Retrieve the corresponding review from db
        const review = await Review.findOne({where : {id: image.reviewId, userId: req.user.id}});
        if(!review) {
            let error = 'Couldn\'t find a review containing the image';
            res.status(404)
                .json({error});
            return Promise.reject(new Error(error));
        }

        res.download(saveDir + image.fileName, function(err) {
            if(err) {
                console.log('Failed to download image:', err);
                res.status(404).json({
                    error: "Failed to retrieve the image file"
                });
                return Promise.reject(new Error(err));
            } else {
                res.status(200);
                return Promise.resolve('next');
            }
        });
    },

    fetchThumbnail: async(req, res, next) => {
        const id = req.params.id;
        if(!id) {
            let error = 'No review id given!';
            res.status(400)
                .json({error});
            return Promise.reject(new Error(error));
        }

        let review = await Review.findOne({ where: {id, userId: req.user.id}, include: ['thumbnail'] });
        if(!review) {
            let error = 'Could not find a review with given id and credentials';
            res.status(404)
                .json({error});
            return Promise.reject(new Error(error));
        }

        if(!review.thumbnail) {
            let error = 'A thumbnail has not been created yet for this review';
            res.status(404)
                .json({error});
            return Promise.reject(new Error(error));
        }

        res.download(thumbnailDir + review.thumbnail.fileName, function(err) {
            if(err) {
                console.log('Failed to download thumbnail:', err);
                res.status(404).json({
                    error: "Failed to locate the thumbnail"
                });
                return Promise.reject(new Error(err));
            } else {
                res.status(200);
                return Promise.resolve('next');
            }
        });
    },

    fetchCategories: async(req, res, next) => {
        let categories = await ReviewCategory.findAll();
        
        categories = categories.map(category => {
            return category.categoryName;
        });

        res.status(200).json(categories);

        return Promise.resolve('next');
    }
};