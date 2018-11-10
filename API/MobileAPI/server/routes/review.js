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
    destination: './review_images/',
    filename: function(req, file, next) {
        return next(null, file.fieldname + '-' + Date.now() + path.extname(file.originalname));
    }
});
const uploader = multer({
    storage
});

router.use('/create', passportAuth, validateBody(schemas.reviewSchema), ReviewController.create);

router.use('/get/:id', passportAuth, ReviewController.fetch);

router.use('/image/upload', passportAuth, uploader.single('screenshot'), ReviewController.imageUpload)

module.exports = router;