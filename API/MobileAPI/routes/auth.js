const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const AuthController = require('../controllers/auth');
const passport = require('passport');
const passportConfig = require('../passport');

const passportLocal = passport.authenticate('local', { session: false });
const passportJWT = passport.authenticate('jwt', { session: false });

router.use('/signup', validateBody(schemas.authSchema), AuthController.signUp);

router.use('/login', validateBody(schemas.authSchema), passportLocal, AuthController.logIn);

router.use('/secret', passportJWT, AuthController.secret);

module.exports = router;