const router = require('express-promise-router')();

const { validateBody, schemas } = require('../helpers/routeHelpers');
const AuthController = require('../controllers/auth');

router.use('/signup', validateBody(schemas.authSchema), AuthController.signUp);

router.use('/login', AuthController.logIn);

router.use('/secret', AuthController.secret);

module.exports = router;