const chai = require('chai');
const faker = require('faker');
const sinon = require('sinon')
const sinonChai = require('sinon-chai');
const rewire = require('rewire');

require('dotenv').config();

const { expect, assert, should } = chai;

const { Application } = require('../../../server/database/models');
const controller = rewire('../../../server/controllers/application.js');

chai.use(sinonChai);
chai.use(require('chai-as-promised'));

let sandbox = null;

describe('Application controller', () => {
    let initApplications = [
        {
            name: faker.commerce.productName(),
            operatingSystem: 'Android'
        },
        {
            name: faker.commerce.productName(),
            operatingSystem: 'iOS'
        }
    ];
    
    initApplications.forEach(object => {
        object.getPublic = function() {
            return {
                id: null,
                name: this.name,
                operatingSystem: this.operatingSystem
            };
        };
        
        object.save = () => Promise.resolve();
    });

    let testApplication = initApplications[0];

    let req = {
        values: {
            body: testApplication
        },
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

    describe('add', () => {
        it('should add an application\'s data', async () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(Application, 'findOne').returns(Promise.resolve(null));
            sandbox.stub(Application.prototype, 'save').returns(Promise.resolve());

            await controller.add(req, res);
            expect(res.status).to.have.been.calledWith(200);
            expect(res.json.callCount).to.equal(1);
            expect(res.json).to.have.been.calledWith(testApplication.getPublic());
        });

        it('shouldn\'t save if the app is found in the db', () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(Application, 'findOne').returns(Promise.resolve(testApplication));

            return expect(controller.add(req, res).then(() => {
                expect(res.status).to.have.been.calledWith(403);
                expect(res.json.callCount).to.equal(1);
            })).to.be.rejectedWith(Error, "That application already exists for that operating system.");
        });
    });

    describe('listAll', () => {
       it('should return a list of applications', async () => {
          sandbox.spy(res, 'json');
          sandbox.spy(res, 'status');
          sandbox.stub(Application, 'findAll').returns(Promise.resolve(initApplications));

          await controller.listAll(req, res);
          expect(res.status).to.have.been.calledWith(200);
          expect(res.json.callCount).to.equal(1);
          expect(res.json).to.have.been.calledWith(initApplications.map(app => {
              return app.getPublic();
          }));
       });
    });

    describe('findApps', () => {
       it('should return a list of application', async () => {
            sandbox.spy(res, 'json');
            sandbox.spy(res, 'status');
            sandbox.stub(Application, 'findAll').returns(Promise.resolve(initApplications));

            await controller.findApps(req, res);
            expect(res.status).to.have.been.calledWith(200);
            expect(res.json.callCount).to.equal(1);
            expect(res.json).to.have.been.calledWith(initApplications.map(app => {
                return app.getPublic();
            }));
       }); 
    });
});