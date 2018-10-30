const express = require('express');
const morgan = require('morgan');
const bodyParser = require('body-parser');

require('dotenv').config();

const app = express();

//Test database connection
const Sequelize = require('sequelize');
const sequelize = new Sequelize(process.env.DB_CONNECTION);

sequelize.authenticate()
    .then(() => {
        console.log('Connected to database');
    })
    .catch(err => {
        console.error('Couldn\'t connect to database:', err);
        process.exit(1);
    });

//Load up models
const models = require('./database/models');

//In development, force tables to the db
if(process.env.NODE_ENV == 'dev' || process.env.NODE_ENV == 'test') {
    models.sequelize.sync({force: true})
        .then(() => {
            console.log('Models loaded and synced. Database emptied.');
        });
} else {
    models.sequelize.sync()
        .then(() => {
            console.log('Models loaded and synced.');
        });
}


//Middlewares
if(!process.env.NODE_ENV === 'test') {
    app.use(morgan('dev'));
}
app.use(bodyParser.json());

//Routes
app.use('/auth', require('./routes/auth'));

module.exports = app;