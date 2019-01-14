const chai = require('chai');
const chaiHttp = require('chai-http');
const faker = require('faker');

require('dotenv').config();

const { expect, assert, should } = chai;

const { app, dbSync } = require('../../../server/app')
const server = app;

chai.use(chaiHttp);

let token;

describe('Auth route', () => {
    const signup = '/auth/signup';
    const login = '/auth/login';
    const user = { email: faker.internet.email(), password: faker.internet.password() };
    const preSave = { email: 'joe.average@gmail.com', password: faker.internet.password() };

    //Set test user into the db and save the JWT token
    before(done => {
        dbSync().then(() => done());
    });

    before(done => {
        chai
            .request(server)
            .post(signup)
            .send(preSave)
            .end((err, res) => {
                expect(res.status).to.equal(200);
                token = res.body.token;
                done();
            });
    });

    //Tests

    describe('signup', () => {
        it('should create a new user if it doesn\'t exist already', done => {
            chai
                .request(server)
                .post(signup)
                .send(user)
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).not.to.be.empty;
                    expect(res.body).to.have.property('token');
                    done();
                });
        });

        it('should return 409 if user exists', done => {
            chai
                .request(server)
                .post(signup)
                .send(preSave)
                .end((err, res) => {
                    expect(res.status).to.equal(409);
                    done();
                });
        });
    });

    describe('login', () => {
        it('should return 400 if no email and password is sent', (done) => {
            let user = {};
            chai
                .request(server)
                .post(login)
                .send(user)
                .end((err, res) => {
                    expect(res.status).to.be.equal(400);
                    done();
                });
        });

        it('should return 200 and auth token', (done) => {
            chai
                .request(server)
                .post(login)
                .send(preSave)
                .end((err, res) => {
                    expect(res.status).to.be.equal(200);
                    expect(res.body).not.to.be.empty;
                    expect(res.body).to.have.property('token');
                    done();
                });
        });
    });
});