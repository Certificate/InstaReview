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

/**
 * @apiDefine UserAuthenticationFailed
 * 
 * @apiError UserAuthenticationFailed Failed to authenticate user with the authentication token (might be missing).
 * @apiErrorExample Error-Response:
 *  HTTP/1.1 403 Unauthorized
 *  Unauthorized
 */

 /**
  * @apiDefine ValidationError
  * 
  * @apiError ValidationError Failed to validate body
  * @apiErrorExample {json} Error-Response:
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
   * @apiDefine AuthenticationSuccess
   * 
   * @apiSuccess {String} token Authorization token
   * @apiSuccessExample {json} Success-Response:
   *     HTTP/1.1 200 OK
   *     {
   *         "token": "signed.JWT.token"
   *     }
   */

 /**
  * @api {post} /auth/signup Register a new user
  * @apiName AddUser
  * @apiGroup Authentication
  * 
  * @apiParam {String} email Email to register with
  * @apiParam {String} password Plain-text password, will be encrypted before saving
  * @apiParamExample {json} Request-Example:
  *     {
  *         "email": "example@example.com",
  *         "password": "password"
  *     }
  * 
  * @apiUse AuthenticationSuccess
  * 
  * @apiUse ValidationError
  * 
  * @apiError UserExists User already exists
  * @apiErrorExample {json} UserExists
  *     HTTP/1.1 409 Conflict
  *     {
  *         "error": "User already exists."
  *     }
  */
router.use('/signup', validateBody(schemas.authSchema), AuthController.signUp);

/**
 * @api {post} /auth/login Login with an account
 * @apiName LoginUser
 * @apiGroup Authentication
 * 
 * @apiParam {String} email Email to login with
 * @apiParam {String} password Plain-text password
 * @apiParamExample {json} Request-Example:
 *     {
 *         "email": "example@example.com",
 *         "password": "password"
 *     }
 * 
 * @apiUse AuthenticationSuccess
 * 
 * @apiError WrongLoginDetails Wrong username or password
 * @apiErrorExample WrongLoginDetails
 *  HTTP/1.1 401 Unauthorized
 *  Unauthorized
 * 
 * @apiUse ValidationError
 */
router.use('/login', validateBody(schemas.authSchema), passportAuthenticate.local, AuthController.logIn);

/**
 * @api {post} /auth/google Login/Register with a Google account
 * @apiName LoginUserGoogle
 * @apiGroup Authentication
 * 
 * @apiParam {String} id_token Token returned by Google after OAuth is done on the mobile app
 * 
 * @apiUse AuthenticationSuccess
 * 
 * @apiError AuthenticationFailed Failed to confirm and use the given token
 * @apiErrorExample AuthenticationFailed
 *  HTTP/1.1 401 Unauthorized
 *  Unauthorized
 */
if(process.env.USE_GOOGLE_AUTH) {
    router.use('/google', passportAuthenticate.google, AuthController.logInGoogle);
}

/**
 * @api {post} /auth/facebook Login/Register with a Facebook account
 * @apiName LoginUserFacebook
 * @apiGroup Authentication
 * 
 * @apiParam {String} access_token Token returned by Facebook after OAuth is done on the mobile app
 * 
 * @apiUse AuthenticationSuccess
 * 
 * @apiError AuthenticationFailed Failed to trade the given token for user information with Facebook
 * @apiErrorExample AuthenticationFailed
 *  HTTP/1.1 401 Unauthorized
 *  Unauthorized
 */
if(process.env.USE_FACEBOOK_AUTH) {
    router.use('/facebook', passportAuthenticate.facebook, AuthController.logInFacebook);
}

module.exports = router;