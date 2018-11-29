const router = require('express-promise-router')();
const { validateBody, schemas } = require('../helpers/routeHelpers');
const ApplicationController = require('../controllers/application');

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
 * @api {post} /application/add Add a new application data
 * @apiName AddApplication
 * @apiGroup Application
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {String} name Name of the application
 * @apiParam {String} operatingSystem Operating system of the application. Valid values are ["Android", "iOS"].
 * @apiParamExample {json} Request-Example:
 *  {
 *    "name": "Google",
 *    "operatingSystem": "Android"
 *  }
 * 
 * @apiSuccess {Number} id Database ID of the data
 * @apiSuccess {String} name Name of the application
 * @apiSuccess {String} updatedAt Timestamp of last modification
 * @apiSuccess {String} createdAt Timestamp of creation
 * @apiSuccessExample {json} Success-Response:
 *  HTTP/1.1 200 OK
 *  {
 *      "id": 1,
 *      "name": "Application Name",
 *      "operatingSystem": "Android",
 *      "updatedAt": "1970-01-01T00:00:00.00Z",
 *      "createdAt": "1970-01-01T00:00:00.00Z"
 *  }
 * 
 * @apiUse UserAuthenticationFailed
 * @apiUse ValidationError
 * 
 * @apiError ApplicationExists Application data already exists for that operating system
 * @apiErrorExample {json} ApplicationExists
 *  HTTP/1.1 403 Bad Request
 *  {
 *      "error": "That application already exists for that operating system."
 *  }
 */
router.use('/add', passportAuth, validateBody(schemas.applicationSchema), ApplicationController.add);

/**
 * @api {get} /application/list Lists data of all saved applications
 * @apiName GetApplicationsAll
 * @apiGroup Application
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * 
 * @apiSuccess {Application[]} applications List of the applications
 * @apiSuccessExample {json} Success-Response:
 *  HTTP/1.1 200 OK
 *  [
 *      {
 *          "id": 1,
 *          "name": "Application Name",
 *          "operatingSystem": "Android",
 *      }
 *  ]
 * 
 * @apiUse UserAuthenticationFailed
 */
router.use('/list', passportAuth, ApplicationController.listAll);

/**
 * @api {get} /application/find/:searchParameter Returns a list of applications matching the given parameter
 * @apiName GetApplicationByName
 * @apiGroup Application
 * 
 * @apiHeader (Authentication) {String} Authorization Authorization token
 * @apiParam {String} searchParameter ID/Name/Operating system of application(s)
 * 
 * @apiSuccess {Application[]} applications List of applications matching given parameter
 */
router.use('/find/:searchParam', passportAuth, ApplicationController.findApps);

module.exports = router;