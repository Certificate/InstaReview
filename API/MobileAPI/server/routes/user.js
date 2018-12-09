const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const UserController = require('../controllers/user');

const passport = require('passport');
const passportConfig = require('../passport-config');
const passportAuth = passport.authenticate('jwt', { session: false });

/**
 * @apiDefine UserAuthenticationFailed
 * 
 * @apiError UserAuthenticationFailed Failed to authenticate user with the authentication token (might be missing).
 * @apiErrorExample UserAuthenticationFailed
 *  HTTP/1.1 401 Unauthorized
 *  Unauthorized
 */

/**
 * @api {get} /user/info Fetch user information
 * @apiName GetUserInfo
 * @apiGroup User
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * 
 * @apiSuccess {Number} id Database id of the user
 * @apiSuccess {String} email Email address of the user
 * @apiSuccess {String} name Name of the user
 * @apiSuccess {String} gender Gender of the user
 * @apiSuccess {Date} birthday Birthday of the user
 * @apiSuccess {Date} createdAt Time of the user's creation
 * @apiSuccess {Date} updatedAt Time of the user's last change
 * @apiSuccessExample {json} Success-Response:
 *  HTTP/1.1 200 OK
 *  {
 *      "id": 1,
 *      "email": "email@gmail.com",
 *      "name": "Tom Tester",
 *      "gender": "Male",
 *      "birthday": "1/1/1990",
 *      "createdAt": "2018-12-09T14:07:57.000Z",
 *      "updatedAt": "2018-12-09T14:07:57.000Z" 
 *  }
 * 
 * @apiUse UserAuthenticationFailed
 */
router.use('/info', passportAuth, UserController.getInfo);

/**
 * @api {post} /user/edit Edit user information
 * @apiName EditUserInfo
 * @apiGroup User
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {String} email Email to be changed to (optional)
 * @apiParam {String} password New password (optional)
 * @apiParam {String} name Name to be changed to (optional)
 * @apiParam {String} gender Gender to be changed to, valid values are ['Male', 'Female'] (optional)
 * @apiParam {Date} birthday Birthday to be changed to (optional)
 * @apiParamExample {json} Request-Example:
 *  {
 *      "email": "newEmail@email.com",
 *      "name": "New Name",
 *      "password": "thisisadifferentpassword",
 *      "gender": "Male",
 *      "birthday": "12/24/2018"
 *  }
 * 
 * @apiSuccess {Number} id Database id of the user
 * @apiSuccess {String} email Email address of the user
 * @apiSuccess {String} name Name of the user
 * @apiSuccess {String} gender Gender of the user
 * @apiSuccess {Date} birthday Birthday of the user
 * @apiSuccess {Date} createdAt Time of the user's creation
 * @apiSuccess {Date} updatedAt Time of the user's last change
 * @apiSuccessExample {json} Success-Response:
 *  HTTP/1.1 200 OK
 *  {
 *      "id": 1,
 *      "email": "email@gmail.com",
 *      "name": "Tom Tester",
 *      "gender": "Male",
 *      "birthday": "1/1/1990",
 *      "createdAt": "2018-12-09T14:07:57.000Z",
 *      "updatedAt": "2018-12-09T14:07:57.000Z" 
 *  }
 * 
 * @apiUse UserAuthenticationFailed
 */
router.use('/edit', passportAuth, validateBody(schemas.userSchema), UserController.edit);

module.exports = router;