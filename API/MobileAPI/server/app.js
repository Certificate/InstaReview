const express = require('express');
const morgan = require('morgan');
const bodyParser = require('body-parser');

require('dotenv').config();

const app = express();

//Load up models
const models = require('./database/models');


dbSync = async () => {
    //Test database connection
    await models.sequelize.authenticate()
        .then(() => {
            console.log('Connected to database');
        })
        .catch(err => {
            console.error('Couldn\'t connect to database:', err);
            process.exit(1);
        });

    //In development, force tables to the db
    /*if(process.env.NODE_ENV == 'dev' || process.env.NODE_ENV == 'test') {
        await models.sequelize.sync({force: true})
            .then(() => {
                console.log('Models loaded and forcefully synced.');
            });
    } else {*/
        await models.sequelize.sync()
            .then(() => {
                console.log('Models loaded and synced.');
            });
    //}
}

//Middlewares
if(process.env.NODE_ENV !== 'test') {
    app.use(morgan('dev'));
}
app.use(bodyParser.json());

//Routes
app.use('/auth', require('./routes/auth'));
app.use('/review', require('./routes/review'));
app.use('/application', require('./routes/application'));
app.use('/user', require('./routes/user'));

module.exports = { app, dbSync };