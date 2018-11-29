const chai = require('chai');
const chaiHttp = require('chai-http');
const faker = require('faker');

require('dotenv').config();

const { expect, assert, should } = chai;

const { app, dbSync } = require('../../../server/app')
const server = app;

chai.use(chaiHttp);

let token;

describe('Application route', () => {
    const add = '/application/add';
    const list = '/application/list';
    const find = '/application/find';

    let androidAppName = '';
    const androidApplication = { name: faker.lorem.word(), operatingSystem: 'Android' };
    const iOSApplication = { name: faker.lorem.word(), operatingSystem: 'iOS' };
    const invalidApplication = { name: faker.lorem.word(), operatingSystem: 'Windows Phone' };

    const authUser = { email: 'test.user@gmail.com', password: faker.internet.password() };

    //Init db
    before(done => {
        dbSync().then(() => done());
    });

    //Set a test user to get auth token
    before(done => {
        chai
            .request(server)
            .post('/auth/signup')
            .send(authUser)
            .end((err, res) => {
                expect(res.status).to.equal(200);
                token = res.body.token;
                done();
            });
    });

    //Save androidApplication
    before(done => {
        chai.request(server)
            .post(add)
            .set('Authorization', token)
            .send(androidApplication)
            .end((err, res) => {
                expect(res.status).to.equal(200);
                androidAppName = res.body.name;
                done();
            });
    });


    //Tests
    describe('add', () => {
        it('should fail without authorization token', (done) => {
            chai.request(server)
                .post(add)
                .send(androidApplication)
                .end((err, res) => {
                    expect(res.status).to.equal(401);
                    done();
                });
        });

        it('should save a new application', (done) => {
            chai.request(server)
                .post(add)
                .set('Authorization', token)
                .send(iOSApplication)
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).not.to.be.empty;
                    expect(res.body).to.have.property('id');
                    expect(res.body).to.have.property('name');
                    expect(res.body).to.have.property('operatingSystem');
                    done();
                });
        });

        it('should fail to save the same application twice', (done) => {
            chai.request(server)
                .post(add)
                .set('Authorization', token)
                .send(androidApplication)
                .end((err, res) => {
                    expect(res.status).to.equal(403);
                    done();
                });
        });

        it('should be able to save an existing application for a different OS', (done) => {
            chai.request(server)
                .post(add)
                .set('Authorization', token)
                .send({
                    name: androidAppName,
                    operatingSystem: 'iOS'
                })
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).to.have.property('id');
                    expect(res.body).to.have.property('name');
                    expect(res.body).to.have.property('operatingSystem');
                    done();
                });
        });
    });

    describe('list', () => {
        it('should fail without authorization', (done) => {
            chai.request(server)
                .get(list)
                .send()
                .end((err, res) => {
                    expect(res.status).to.equal(401);
                    done();
                });
        });

        it('should return a list of applications', (done) => {
            chai.request(server)
                .get(list)
                .set('Authorization', token)
                .send()
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).not.to.be.empty;
                    expect(res.body).to.be.an('array');
                    expect(res.body.find(app => {
                        return app.name === androidApplication.name
                            && app.operatingSystem === androidApplication.operatingSystem;
                    })).to.not.be.empty;
                    done();
                });
        });
    });

    describe('find', () => {
        it('should fail without authorization', (done) => {
            chai.request(server)
                .get(find + '/' + androidApplication.name)
                .send()
                .end((err, res) => {
                    expect(res.status).to.be.equal(401);
                    done();
                });
        });

        it('should find an application by name', async () => {
            let res = await chai.request(server)
                .get(find + '/' + androidApplication.name)
                .set('Authorization', token)
                .send();

            expect(res.status).to.equal(200);
            expect(res.body).not.to.be.empty;
            expect(res.body).to.be.an('array');
            expect(res.body.find(app => {
                return app.name === androidApplication.name
                    && app.operatingSystem === androidApplication.operatingSystem;
            })).to.not.be.empty;
        });

        it('should find an application by operating system', async () => {
            let res = await chai.request(server)
                .get(find + '/' + androidApplication.operatingSystem)
                .set('Authorization', token)
                .send();
            expect(res.status).to.equal(200);
            expect(res.body).not.to.be.empty;
            expect(res.body).to.be.an('array');
            expect(res.body.find(app => {
                return app.name === androidApplication.name
                    && app.operatingSystem === androidApplication.operatingSystem;
            })).to.not.be.empty;
        });

        it('should find an application by id', async () => {
            let app;
            let res = await chai.request(server)
                .get(list)
                .set('Authorization', token)
                .send();

            app = res.body.find(application => {
                return application.name === androidApplication.name;
            });
            expect(app).not.to.be.empty;
            
            res = await chai.request(server)
                .get(find + '/' + app.id)
                .set('Authorization', token)
                .send();

            expect(res.status).to.equal(200);
            expect(res.body).not.to.be.empty;
            expect(res.body).to.be.an('array').that.has.lengthOf(1);
            expect(res.body.find(app => {
                return app.name === androidApplication.name
                    && app.operatingSystem === androidApplication.operatingSystem;
            })).to.not.be.empty;
        });
    });
});