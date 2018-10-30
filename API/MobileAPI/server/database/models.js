//Set up Sequelize
const Sequelize = require('sequelize');
const sequelize = new Sequelize(process.env.DB_CONNECTION);

models = {
    Sequelize: Sequelize,
    sequelize: sequelize,
    Application: sequelize.import(__dirname + '/models/application'),
    User: sequelize.import(__dirname + '/models/user'),
    Review: sequelize.import(__dirname + '/models/review'),
    Image: sequelize.import(__dirname + '/models/image'),
}

module.exports = models;