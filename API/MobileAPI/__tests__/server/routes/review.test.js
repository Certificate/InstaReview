const chai = require('chai');
const chaiHttp = require('chai-http');
const faker = require('faker');

require('dotenv').config();

const { expect, assert, should } = chai;

const { app, dbSync } = require('../../../server/app')
const server = app;

chai.use(chaiHttp);

let token;
let wrongToken;
let categories;

describe('Review route', () => {
    const routes = {
        create: '/review/create',
        edit: '/review/edit',
        get: '/review/get',
        list: '/review/list',
        imageUpload: '/review/image/upload',
        imageDownload: '/review/image/download',
        thumbnail: '/review/thumbnail',
        categories: '/review/categories'
    };

    const authUser = { 
        email: 'test.user@gmail.com',
        password: faker.internet.password(),
        name: faker.name.findName(),
        gender: "Male",
        birthday: faker.date.past()
    };

    const wrongUser = { 
        email: 'wrong.user@gmail.com',
        password: faker.internet.password(),
        name: faker.name.findName(),
        gender: "Male",
        birthday: faker.date.past()
    };

    const initApplication = {
        name: 'Test Application',
        operatingSystem: 'Android'
    };

    const initReview = {
        temporalContext: 'Allocative',
        spatialContext: 'Wandering',
        socialContext: 'Constraining',
        textReview: faker.lorem.sentence()
    };

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

    //Set a test user to get auth token
    before(done => {
        chai
            .request(server)
            .post('/auth/signup')
            .send(wrongUser)
            .end((err, res) => {
                expect(res.status).to.equal(200);
                wrongToken = res.body.token;
                done();
            });
    });

    //Add an application
    before(done => {
        chai
            .request(server)
            .post('/application/add')
            .set('Authorization', token)
            .send(initApplication)
            .end((err, res) => {
                expect(res.status).to.equal(200);
                initApplication.id = initReview.appId = res.body.id;
                done();
            });
    });

    //Fetch category list
    before(done => {
        chai
            .request(server)
            .get(routes.categories)
            .set('Authorization', token)
            .send()
            .end((err, res) => {
                expect(res.status).to.equal(200);
                categories = res.body;
                initReview.categoryName = categories[0];
                done();
            });
    });

    //Add an initial review
    before(done => {
        chai
            .request(server)
            .post(routes.create)
            .set('Authorization', token)
            .send(initReview)
            .end((err, res) => {
                expect(res.status).to.equal(200);
                initReview.id = res.body.id;
                done();
            });
    });

    /* Tests start here */
    describe('create', () => {
        it('should fail without authorization token', (done) => {
            chai
                .request(server)
                .post(routes.create)
                .send()
                .end((err, res) => {
                    expect(res.status).to.equal(401);
                    expect(res.body).to.be.empty;
                    done();
                });
        });

        it('should create a new review', (done) => {
            let newReview = {
                appId: initApplication.id,
                categoryName: categories[1],
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.create)
                .set('Authorization', token)
                .send(newReview)
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).not.to.be.empty;

                    expect(res.body).to.have.property('id');
                    expect(res.body).to.have.property('userId');
                    expect(res.body).to.have.property('appId');
                    expect(res.body).to.have.property('temporalContext');
                    expect(res.body).to.have.property('spatialContext');
                    expect(res.body).to.have.property('socialContext');
                    expect(res.body).to.have.property('textReview');
                    expect(res.body).to.have.property('updatedAt');
                    expect(res.body).to.have.property('createdAt');
                    expect(res.body).to.have.property('application');
                    expect(res.body).to.have.property('category');

                    done();
                });
        });

        it('should fail without a valid application id', (done) => {
            let newReview = {
                appId: 9999,
                categoryName: categories[1],
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.create)
                .set('Authorization', token)
                .send(newReview)
                .end((err, res) => {
                    expect(res.status).to.equal(400);
                    expect(res.body).not.to.be.empty;
                    expect(res.body.error).to.equal('Could not find application data with given appId');
                    done();
                });
        });

        it('should fail without a valid categoryName', (done) => {
            let newReview = {
                appId: initApplication.id,
                categoryName: "Invalid category",
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.create)
                .set('Authorization', token)
                .send(newReview)
                .end((err, res) => {
                    expect(res.status).to.equal(400);
                    expect(res.body).not.to.be.empty;
                    expect(res.body.error).to.equal('Could not find a corresponding category for the review');
                    done();
                });
        });
    });

    describe('edit', () => {
        it('should fail without authorization token', (done) => {
            chai
                .request(server)
                .post(routes.edit)
                .send()
                .end((err, res) => {
                    expect(res.status).to.equal(401);
                    expect(res.body).to.be.empty;
                    done();
                });
        });

        it('should edit the review', (done) => {
            let editReview = {
                id: initReview.id,
                appId: initApplication.id,
                categoryName: categories[2],
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.edit)
                .set('Authorization', token)
                .send(editReview)
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).not.to.be.empty;

                    expect(res.body.id).to.equal(editReview.id);
                    expect(res.body.category.categoryName).to.equal(editReview.categoryName);
                    expect(res.body.temporalContext).to.equal(editReview.temporalContext);
                    expect(res.body.spatialContext).to.equal(editReview.spatialContext);
                    expect(res.body.socialContext).to.equal(editReview.socialContext);
                    expect(res.body.textReview).to.equal(editReview.textReview);

                    done();
                });
        });

        it('should fail without an id', (done) => {
            let editReview = {
                appId: initApplication.id,
                categoryName: categories[2],
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.edit)
                .set('Authorization', token)
                .send(editReview)
                .end((err, res) => {
                    expect(res.status).to.equal(400);
                    expect(res.body).not.to.be.empty;
                    expect(res.body.error).to.equal('No id for the review was given for it to be edited');

                    done();
                });
        });

        it('should fail if the user isn\'t the original creator', (done) => {
            let editReview = {
                id: initReview.id,
                appId: initApplication.id,
                categoryName: categories[2],
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.edit)
                .set('Authorization', wrongToken)
                .send(editReview)
                .end((err, res) => {
                    expect(res.status).to.equal(400);
                    expect(res.body).not.to.be.empty;
                    expect(res.body.error).to.equal('Could not find a review with given id and credentials');

                    done();
                });
        });

        it('should fail if the application id is faulty', (done) => {
            let editReview = {
                id: initReview.id,
                appId: 9999,
                categoryName: categories[2],
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.edit)
                .set('Authorization', token)
                .send(editReview)
                .end((err, res) => {
                    expect(res.status).to.equal(400);
                    expect(res.body).not.to.be.empty;
                    expect(res.body.error).to.equal('Could not find application data with given appId');

                    done();
                });
        });

        it('should fail if the categoryName is faulty', (done) => {
            let editReview = {
                id: initReview.id,
                appId: initApplication.id,
                categoryName: 'Faulty category',
                temporalContext: 'Intensive',
                spatialContext: 'Visiting',
                socialContext: 'Encouraging',
                textReview: faker.lorem.sentence()
            };

            chai
                .request(server)
                .post(routes.edit)
                .set('Authorization', token)
                .send(editReview)
                .end((err, res) => {
                    expect(res.status).to.equal(400);
                    expect(res.body).not.to.be.empty;
                    expect(res.body.error).to.equal('Could not find a corresponding category for the review');

                    done();
                });
        });
    });
});