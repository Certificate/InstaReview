module.exports = (sequelize, DataTypes) => {
    var Review = sequelize.define('review', {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },
        userId: {
            type: DataTypes.INTEGER,
            allowNull: false,
            references: {
                model: 'users',
                key: 'id'
            }
        },
        appId: {
            type: DataTypes.INTEGER,
            allowNull: false,
            references: {
                model: 'applications',
                key: 'id'
            }
        },
        temporalContext: {
            type: DataTypes.ENUM('Intensive', 'Allocative'),
            allowNull: false
        },
        spatialContext: {
            type: DataTypes.ENUM('Visiting', 'Traveling', 'Wandering'),
            allowNull: false
        },
        socialContext: {
            type: DataTypes.ENUM('Constraining', 'Encouraging'),
            allowNull: false
        },
        textReview: {
            type: DataTypes.TEXT,
            allowNull: false
        }
    });

    return Review;
}