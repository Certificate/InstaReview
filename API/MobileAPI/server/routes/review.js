const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const ReviewController = require('../controllers/review');

const passport = require('passport');
const passportConfig = require('../passport-config');
const passportAuth = passport.authenticate('jwt', { session: false });

router.use('/create', passportAuth, validateBody(schemas.reviewSchema), ReviewController.create);

router.use('/:id', passportAuth, ReviewController.fetch);

module.exports = router;