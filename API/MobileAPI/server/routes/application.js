const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const ApplicationController = require('../controllers/application');

const passport = require('passport');
const passportConfig = require('../passport-config');
const passportAuth = passport.authenticate('jwt', { session: false });

router.use('/add', passportAuth, validateBody(schemas.applicationSchema), ApplicationController.add);

router.use('/list', passportAuth, ApplicationController.listAll);

module.exports = router;