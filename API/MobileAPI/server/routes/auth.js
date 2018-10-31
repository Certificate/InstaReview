const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const AuthController = require('../controllers/auth');

const passport = require('passport');
const passportConfig = require('../passport-config');
const passportAuthenticate = {
    local: passport.authenticate('local', { session: false }),
    jwt: passport.authenticate('jwt', { session: false }),
    google: passport.authenticate('googleToken', { session: false }),
    facebook: passport.authenticate('facebookToken', { session: false })
}

router.use('/signup', validateBody(schemas.authSchema), AuthController.signUp);

router.use('/login', validateBody(schemas.authSchema), passportAuthenticate.local, AuthController.logIn);

if(process.env.USE_GOOGLE_AUTH) {
    router.use('/google', passportAuthenticate.google, AuthController.logInGoogle);
}

if(process.env.USE_FACEBOOK_AUTH) {
    router.use('/facebook', passportAuthenticate.facebook, AuthController.logInFacebook);
}

router.use('/secret', passportAuthenticate.jwt, AuthController.secret);

module.exports = router;