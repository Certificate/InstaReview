const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const ReviewController = require('../controllers/review');

const passport = require('passport');
const passportConfig = require('../passport-config');
const passportAuth = passport.authenticate('jwt', { session: false });

//Set up multer for file uploads
const multer = require('multer');
const path = require('path');
const storage = multer.diskStorage({
    destination: process.env.IMAGE_SAVE_DIR || './review_images/',
    filename: function(req, file, next) {
        return next(null, file.fieldname + '-' + Date.now() + path.extname(file.originalname));
    }
});
const uploader = multer({
    storage
});

/**
 * @apiDefine UserAuthenticationFailed
 * 
 * @apiError UserAuthenticationFailed Failed to authenticate user with the authentication token (might be missing).
 * @apiErrorExample UserAuthenticationFailed
 *  HTTP/1.1 401 Unauthorized
 *  Unauthorized
 */

/**
  * @apiDefine ValidationError
  * 
  * @apiError ValidationError Failed to validate body
  * @apiErrorExample {json} ValidationError
  *     HTTP/1.1 400 Bad Request
  *     {
  *         "name": "ValidationError",
  *         "details": [
  *             {
  *                 ...
  *             }
  *         ]
  *     }
  */

 /**
  * @apiDefine ReviewExampleObject
  * 
  * @apiSuccess {Number} id Id of the review in the database
  * @apiSuccess {Number} userId Id of the user in the database
  * @apiSuccess {Number} appId Database ID of the application this review is about
  * @apiSuccess {String} temporalContext Temporal context of the review. Valid values are ["Intensive", "Allocative"].
  * @apiSuccess {String} spatialContext Spatial context of the review. Valid values are ["Visiting", "Traveling", "Wandering"].
  * @apiSuccess {String} socialContext Social context of the review. Valid values are ['Constraining', 'Encouraging'].
  * @apiSuccess {String} textReview The text review itself.
  * @apiSuccess {String} updatedAt Timestamp of last modification
  * @apiSuccess {String} createdAt Timestamp of creation
  * @apiSuccess {Image[]} images List of Image-objects containing information of the screenshots for this review. This is only included when fetching a single review (see "Request Review Information").
  * @apiSuccess {Application} application The application object corresponding the appId. Object structure can be seen in the documentation for the Application routes.
  * @apiSuccess {Category} category Category of the review corresponding the categoryId. The most relevant value within this object is the attribute categoryName.
  * @apiSuccess {Thumbnail} thumbnail Thumbnail object containing the fileName-attribute required for downloading thumbnails. This is only included when fetching reviews.
  * @apiSuccessExample {json} Success-Response:
  *  HTTP/1.1 200 OK
  *  {
  *      "id": 1,
  *      "userId": 1,
  *      "appId": 1,
  *      "categoryId": 1,
  *      "thumbnailId": 1,
  *      "temporalContext": "Allocative",
  *      "spatialContext": "Wandering",
  *      "socialContext": "Encouraging",
  *      "textReview": "I found an issue with...",
  *      "updatedAt": "1970-01-01T00:00:00.00Z",
  *      "createdAt": "1970-01-01T00:00:00.00Z",
  *      "images": [],
  *      "application": {
  *         "id": 1,
  *         "name": "Test Application",
  *         "operatingSystem": "Android"
  *      },
  *      "category": {
  *         "id": 1,
  *         "categoryName": "Lagging"
  *      },
  *      "thumbnail": {
  *         "id": 1,
  *         "fileName": "thumbnail-1.png"
  *      }
  *  }
  */

/**
 * @api {post} /review/create Create a new Review
 * @apiName CreateReview
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {Number} appId Database ID of the application this review is about
 * @apiParam {String} categoryName Name of the category of the review (See the list of categories, found in ie. the api-documentation folder, server uses ones defined in categories.json)
 * @apiParam {String} temporalContext Temporal context of the review. Valid values are ["Intensive", "Allocative"].
 * @apiParam {String} spatialContext Spatial context of the review. Valid values are ["Visiting", "Traveling", "Wandering"].
 * @apiParam {String} socialContext Social context of the review. Valid values are ['Constraining', 'Encouraging'].
 * @apiParam {String} textReview The text review itself.
 * @apiParamExample {json} Request-Example:
 *  {
 *      "appId": 1,
 *      "temporalContext": "Intensive",
 *      "spatialContext": "Visiting",
 *      "socialContext": "Constraining",
 *      "textReview": "This is a review."
 *  }
 * 
 * @apiUse ReviewExampleObject
 * 
 * @apiUse UserAuthenticationFailed
 * @apiUse ValidationError
 * 
 * @apiError ApplicationNotFound Failed to fetch data for the application the review is supposedly written about.
 * @apiErrorExample {json} ApplicationNotFound
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "Could not find application data with given appId"
 *  }
 */
router.use('/create', passportAuth, validateBody(schemas.reviewSchema), ReviewController.create);

/**
 * @api {post} /review/edit Edit a review
 * @apiName EditReview
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {Number} id Id of the review to be edited
 * @apiParam {Number} appId Database ID of the application this review is about
 * @apiParam {String} categoryName Name of the category of the review (See the list of categories, found in ie. the api-documentation folder, server uses ones defined in categories.json)
 * @apiParam {String} temporalContext Temporal context of the review. Valid values are ["Intensive", "Allocative"].
 * @apiParam {String} spatialContext Spatial context of the review. Valid values are ["Visiting", "Traveling", "Wandering"].
 * @apiParam {String} socialContext Social context of the review. Valid values are ['Constraining', 'Encouraging'].
 * @apiParam {String} textReview The text review itself.
 * @apiParamExample {json} Request-Example:
 *  {
 *      "id": 1,
 *      "appId": 1,
 *      "temporalContext": "Intensive",
 *      "spatialContext": "Visiting",
 *      "socialContext": "Constraining",
 *      "textReview": "This is a review."
 *  }
 * 
 * @apiUse ReviewExampleObject
 * 
 * @apiUse UserAuthenticationFailed
 * @apiUse ValidationError
 * 
 * @apiError NoReviewID No review ID was given (parameter missing).
 * @apiErrorExample {json} NoReviewID
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "No id for the review was given for it to be edited"
 *  }
 * 
 * @apiError ApplicationNotFound Failed to fetch data for the application the review is supposedly written about.
 * @apiErrorExample {json} ApplicationNotFound
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "Could not find application data with given appId"
 *  }
 * 
 * @apiError ReviewNotFound Failed to find a review with the id and user credentials
 * @apiErrorExample {json} ReviewNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Could not find a review with given id and credentials"
 *  }
 */
router.use('/edit', passportAuth, validateBody(schemas.reviewSchema), ReviewController.edit);

/**
 * @api {get} /review/get/:id Request Review information
 * @apiName GetReview
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {Number} id Review unique ID.
 * 
 * @apiUse ReviewExampleObject
 * 
 * @apiUse UserAuthenticationFailed
 * 
 * @apiError NoReviewID No review ID was given (parameter missing).
 * @apiErrorExample {json} NoReviewID
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "No review id given!"
 *  }
 * 
 * @apiError ReviewNotFound Failed to find a review with the id and user credentials
 * @apiErrorExample {json} ReviewNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Could not find a review with given id and credentials"
 *  }
 */
router.use('/get/:id', passportAuth, ReviewController.fetch);

/**
 * @api {get} /review/list Request Reviews made by user
 * @apiName GetReviewsAll
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * 
 * @apiUse ReviewExampleObject
 * 
 * @apiUse UserAuthenticationFailed
 */
router.use('/list', passportAuth, ReviewController.fetchAll);

/**
 * @api {get} /review/get/:id Delete Review information
 * @apiName RemoveReview
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {Number} id Review unique ID.
 * 
 * @apiUse ReviewExampleObject
 * 
 * @apiUse UserAuthenticationFailed
 * 
 * @apiError NoReviewID No review ID was given (parameter missing).
 * @apiErrorExample {json} NoReviewID
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "No review id given!"
 *  }
 * 
 * @apiError ReviewNotFound Failed to find a review with the id and user credentials
 * @apiErrorExample {json} ReviewNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Could not find a review with given id and credentials"
 *  }
 */
router.use('/remove/:id', passportAuth, ReviewController.removeById);

/**
 * @api {post} /review/image/upload Upload an image for a review
 * @apiDescription Unlike others, this requests body uses content-type 'multipart/form-data' to send files.
 * @apiName UploadReviewImage
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {FILE} screenshot Image file to upload
 * @apiParam {Number} reviewId ID of the review to link the image to.
 * 
 * @apiSuccess {Number} id Database ID of the Image-object
 * @apiSuccess {Number} reviewId ID of the review the image is linked to
 * @apiSuccess {String} fileName Filename of the actual image on disk
 * @apiSuccess {String} updatedAt Timestamp of last modification
 * @apiSuccess {String} createdAt Timestamp of creation
 * @apiSuccessExample {json} Success-Response:
 *  HTTP/1.1 200 OK
 *  {
 *      "id": 1,
 *      "reviewId": 1,
 *      "fileName": "screenshot-1541941036667.jpg",
 *      "updatedAt": "1970-01-01T00:00:00.00Z",
 *      "createdAt": 1970-01-01T00:00:00.00Z
 *  }
 * 
 * @apiUse UserAuthenticationFailed
 * 
 * @apiError ImageNotReceived Image wasn't received
 * @apiErrorExample {json} ImageNotReceived
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "No image received."
 *  }
 * 
 * @apiError ReviewNotFound Review ID wasn't given or couldn't find a review with the ID.
 * @apiErrorExample {json} ReviewNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "No review id was given or couldn't find a review with given id."
 *  }
 */
router.use('/image/upload', passportAuth, uploader.single('screenshot'), ReviewController.imageUpload);

/**
 * @api {get} /review/image/download/:filename Download an image by filename
 * @apiName DownloadReviewImage
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {String} filename Filename of the image
 * 
 * @apiSuccess {image/format} image The image file
 * 
 * @apiUse UserAuthenticationFailed
 * 
 * @apiError MissingFilename Filename parameter is empty
 * @apiErrorExample {json} MissingFilename
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "Filename missing!"
 *  }
 * 
 * @apiError ImageNotFound Failed to retrieve an image with given filename
 * @apiErrorExample {json} ImageNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Image doesn't exist"
 *  }
 * 
 * @apiError ReviewNotFound Failed to retrieve a review containing the image.
 * @apiDescription Basically should never happen due to database constrictions.
 * @apiErrorExample {json} ReviewNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Couldn't find a review containing the image"
 *  }
 * 
 * @apiError NoAccessPermission The provided user doesn't have permission to this image (not the one that created the review)
 * @apiErrorExample {json} NoAccessPermission
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "No permissions to access the file"
 *  }
 * 
 * @apiError FileNotFound Failed to find the actual image on the disk. (Shouldn't happen as long as images aren't being removed manually on the server)
 * @apiErrorExample {json} FileNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Failed to retrieve the image file"
 *  }
 */
router.use('/image/download/:filename', passportAuth, ReviewController.imageDownload);

/**
 * @api {get} /review/thumbnail/:id Download the review thumbnail
 * @apiName DownloadThumbnail
 * @apiGroup Review
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {String} id Id of the review
 * 
 * @apiSuccess {image/png} thumbnail The thumbnail image
 * 
 * @apiUse UserAuthenticationFailed
 * 
 * @apiError NoReviewID No review ID was given (parameter missing).
 * @apiErrorExample {json} NoReviewID
 *  HTTP/1.1 400 Bad Request
 *  {
 *      "error": "No review id given!"
 *  }
 * 
 * @apiError ReviewNotFound Failed to find a review with the id and user credentials
 * @apiErrorExample {json} ReviewNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Could not find a review with given id and credentials"
 *  }
 * 
 * @apiError ThumbnailNotCreated Thumbnail hasn't been created yet
 * @apiErrorExample {json} ThumbnailNotCreated
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "A thumbnail has not been created yet for this review"
 *  }
 * 
 * @apiError ThumbnailNotFound Failed to download the file from disk
 * @apiErrorExample {json} ThumbnailNotFound
 *  HTTP/1.1 404 Not Found
 *  {
 *      "error": "Failed to locate the thumbnail"
 *  }
 */
router.use('/thumbnail/:id', passportAuth, ReviewController.fetchThumbnail);

/**
 * @api {get} /review/categories Fetch a list of available categories
 * @apiName FetchCategories
 * @apiGroup Review
 * 
 * @apiSuccess {String[]} categories List of categories
 * @apiSuccessExample {json} Success-Response:
 *  HTTP/1.1 200 OK
 *  [
 *      "Lagging",
 *      "Functional Error",
 *      ...
 *  ]
 */
router.use('/categories', ReviewController.fetchCategories);

module.exports = router;