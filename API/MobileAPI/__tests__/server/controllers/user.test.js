const chai = require('chai');
const faker = require('faker');
const sinon = require('sinon')
const sinonChai = require('sinon-chai');
const rewire = require('rewire');

require('dotenv').config();

const { expect, assert, should } = chai;

const { User } = require('../../../server/database/models');
const controller = rewire('../../../server/controllers/user.js');

chai.use(sinonChai);
chai.use(require('chai-as-promised'));

let sandbox = null;

describe('User controller', () => {
    let initUser = {
        email: faker.internet.email(),
        password: faker.internet.password(),
        name: faker.name.findName(),
        gender: "Male",
        birthday: faker.date.past(),
        save: () => Promise.resolve(),
        getPublic: function() {
            return {
                email: this.email,
                name: this.name,
                gender: this.gender,
                birthday: this.birthday
            }
        }
    };

    let editUser = {
        email: faker.internet.email(),
        password: faker.internet.password(),
        name: faker.name.findName(),
        gender: "Female",
        birthday: faker.date.past(),
        save: () => Promise.resolve(),
        getPublic: function() {
            return {
                email: this.email,
                name: this.name,
                gender: this.gender,
                birthday: this.birthday
            }
        }
    };

    let req = {
        user: { id: faker.random.number() },
        values: {
            body: initUser
        }
    };

    let res = {
        json: function() {
            return this;
        },
        status: function() {
            return this;
        }
    }

    beforeEach(() => {
        sandbox = sinon.createSandbox();
    });

    afterEach(() => {
        sandbox.restore();
    });

    describe('info', () => {
        it('should return user information', async () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(User, 'findOne').returns(Promise.resolve(initUser));

            await controller.getInfo(req, res);
            expect(res.status).to.have.been.calledWith(200);
            expect(res.json.callCount).to.equal(1);
            expect(res.json).to.have.been.calledWith(initUser.getPublic());
        });
    });

    describe('edit', () => {
        it('should edit user information', async () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(User, 'findOne').returns(Promise.resolve(initUser));
            sandbox.stub(User.prototype, 'save').returns(Promise.resolve());

            let editReq = req;
            editReq.values.body = editUser;

            let err = await controller.edit(editReq, res);
            console.log(err);
            expect(res.status).to.have.been.calledWith(200);
            expect(res.json.callCount).to.equal(1);
            expect(res.json).to.have.been.calledWith(Object.assign({}, initUser.getPublic(), editUser.getPublic()));
        });

        it('should edit only some user information', async () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(User, 'findOne').returns(Promise.resolve(initUser));

            let editReq = req;
            let user = Object.assign({}, editUser.getPublic());
            delete user["password"];
            delete user["name"];
            editReq.values.body = user;

            let err = await controller.edit(editReq, res);
            console.log(err);
            expect(res.status).to.have.been.calledWith(200);
            expect(res.json.callCount).to.equal(1);
            expect(res.json).to.have.been.calledWith(Object.assign({}, initUser.getPublic(), user));
        });
    });
});