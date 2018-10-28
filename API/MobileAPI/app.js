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
    });

//Load up models
const models = require('./database/models');

//In development, force tables to the db
models.sequelize.sync({force: true})
    .then(() => {
        console.log('Models loaded');
    });

//Middlewares
app.use(morgan('dev'));
app.use(bodyParser.json());

//Routes
app.use('/auth', require('./routes/auth'));

//Start server
const port = process.env.PORT || 80;
const host = process.env.HOST || '0.0.0.0';

app.listen(port, host, () => console.log(`API is running on ${host}:${port}`));