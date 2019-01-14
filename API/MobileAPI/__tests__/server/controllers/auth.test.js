const chai = require('chai');
const faker = require('faker');
const sinon = require('sinon')
const sinonChai = require('sinon-chai');
const rewire = require('rewire');

require('dotenv').config();

const { expect, assert, should } = chai;

const { User } = require('../../../server/database/models');
const controller = rewire('../../../server/controllers/auth.js');

chai.use(sinonChai);
chai.use(require('chai-as-promised'));

let sandbox = null;

describe('Auth controller', () => {
    let req = {
        user: { id: faker.random.number() },
        values: {
            body: {
                email: faker.internet.email(),
                password: faker.internet.password()
            }
        }
    };

    let res = {
        json: function() {
            return this;
        },
        status: function() {
            return this;
        }
    };

    beforeEach(() => {
        sandbox = sinon.createSandbox();
    });

    afterEach(() => {
        sandbox.restore();
    });

    describe('login', () => {
        it('should return token when login is called', () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');

            return controller.logIn(req, res).then(() => {
                expect(res.status).to.have.been.calledWith(200);
                expect(res.json.callCount).to.equal(1);
            });
        });

        it('should return fake token using rewire', () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');

            //fake jwt token
            let signToken = controller.__set__('signToken', user => 'fakeToken');

            return controller.logIn(req, res).then(() => {
                expect(res.json).to.have.been.calledWith({ token: 'fakeToken'});
                signToken();
            });
        });
    });

    describe('signup', () => {
        it('should return 409 if the user is already saved in the db', () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(User, 'findOne').returns(Promise.resolve({ id: faker.random.number() }));

            return expect(controller.signUp(req, res).then(() => {
                expect(res.status).to.have.been.calledWith(409);
                expect(res.json).to.have.been.calledWith({ error: 'User already exists.' });
            })).to.be.rejectedWith(Error, "User already exists.");
        });

        it('should return 200 if user is not in db and was saved', () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(User, 'findOne').returns(Promise.resolve(false));
            sandbox.stub(User.prototype, 'save').returns(Promise.resolve({ id: faker.random.number() }));

            return controller.signUp(req, res).then(() => {
                expect(res.status).to.have.been.calledWith(200);
                expect(res.json.callCount).to.equal(1);
            });
        });

        it('should return fake token in res.json', () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(User, 'findOne').returns(Promise.resolve(false));
            sandbox.stub(User.prototype, 'save').returns(Promise.resolve({ id: faker.random.number() }));

            let signToken = controller.__set__('signToken', user => 'fakeTokenNumberTwo');

            return controller.signUp(req, res).then(() => {
                expect(res.json).to.have.been.calledWith({ token: 'fakeTokenNumberTwo' });
                signToken();
            });
        });
    });
});