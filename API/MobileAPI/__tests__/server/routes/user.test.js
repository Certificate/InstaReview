const chai = require('chai');
const chaiHttp = require('chai-http');
const faker = require('faker');

require('dotenv').config();

const { expect, assert, should } = chai;

const { app, dbSync } = require('../../../server/app')
const server = app;

chai.use(chaiHttp);

let token;

describe('User route', () => {
    const getInfo = '/user/info';
    const edit = '/user/edit';
    const login = '/auth/login';

    const authUser = { 
        email: 'test.user@gmail.com',
        password: faker.internet.password(),
        name: faker.name.findName(),
        gender: "Male",
        birthday: faker.date.past()
    };

    const editUser = {
        email: faker.internet.email(),
        password: faker.internet.password(),
        name: faker.name.findName(),
        gender: "Female",
        birthday: faker.date.past()
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

    describe('info', () => {
        it('should fail without authorization token', (done) => {
            chai.request(server)
                .get(getInfo)
                .send()
                .end((err, res) => {
                    expect(res.status).to.equal(401);
                    done();
                });
        });

        it('should return the user', (done) => {
            chai.request(server)
                .get(getInfo)
                .set('Authorization', token)
                .send()
                .end((err, res) => {
                    expect(res.status).to.equal(200);
                    expect(res.body).not.to.be.empty;

                    expect(res.body).to.have.property('email');
                    expect(res.body.email).to.equal(authUser.email);
                    expect(res.body).to.have.property('name');
                    expect(res.body.name).to.equal(authUser.name);
                    expect(res.body).to.have.property('gender');
                    expect(res.body.gender).to.equal(authUser.gender);
                    expect(res.body).to.have.property('birthday');

                    done();
                });
        });
    });

    describe('edit', () => {
        it('should fail without authorization token', (done) => {
            chai.request(server)
                .post(edit)
                .send(editUser)
                .end((err, res) => {
                    expect(res.status).to.equal(401);
                    done();
                });
        });

        it('should edit the user information (without password)', async () => {
            let user = Object.assign({}, editUser);
            delete user["password"];

            let res = await chai.request(server)
                .post(edit)
                .set('Authorization', token)
                .send(user);

            expect(res.status).to.equal(200);
            expect(res.body).not.to.be.empty;

            expect(res.body).to.have.property('email');
            expect(res.body.email).to.equal(user.email);
            expect(res.body).to.have.property('name');
            expect(res.body.name).to.equal(user.name);
            expect(res.body).to.have.property('gender');
            expect(res.body.gender).to.equal(user.gender);
            expect(res.body).to.have.property('birthday');

            //Expect the original password to still work
            user = {
                email: editUser.email,
                password: authUser.password
            };
            let loginRes = await chai.request(server)
                .post(login)
                .send(user);
            expect(loginRes.status).to.equal(200);
        });

        it('should change the password', async () => {
            let res = await chai.request(server)
                .post(edit)
                .set('Authorization', token)
                .send(editUser);
            expect(res.status).to.equal(200);

            //Expect the new password to work
            let user = {
                email: editUser.email,
                password: editUser.password
            };
            expect((await chai.request(server)
                .post(login)
                .send(user)
            ).status).to.equal(200);

            //Expect the old password to not work
            let oldUser = {
                email: editUser.email,
                password: authUser.password
            };
            user.password = authUser.password;
            expect((await chai.request(server)
                .post(login)
                .send(oldUser)
            ).status).to.equal(401);
        });
    });
});