//Set up Sequelize
const Sequelize = require('sequelize');
const sequelize = new Sequelize(process.env.DB_CONNECTION, {
    logging: (process.env.NODE_ENV == 'test') ? false : true
});

models = {
    Sequelize: Sequelize,
    sequelize: sequelize,
    Application: sequelize.import(__dirname + '/models/application'),
    User: sequelize.import(__dirname + '/models/user'),
    Review: sequelize.import(__dirname + '/models/review'),
    Image: sequelize.import(__dirname + '/models/image'),
    Thumbnail: sequelize.import(__dirname + '/models/thumbnail')
}

//Set up association relations
models.Review.hasMany(models.Image, {as: 'images', foreignKey: 'reviewId', sourceKey: 'id'});
models.Review.belongsTo(models.Thumbnail, {as: 'thumbnail', foreignKey: 'id', sourceKey: 'thumbnailId', constraints: false});
models.Review.belongsTo(models.Application, {as: 'application', foreignKey: 'id', sourceKey: 'appId', constraints: false});

module.exports = models;