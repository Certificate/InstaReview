module.exports = (sequelize, DataTypes) => {
    var Application = sequelize.define('application', {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true,
        },
        name: {
            type: DataTypes.STRING,
            allowNull: false,
        },
        operatingSystem: {
            type: DataTypes.ENUM('Android', 'iOS'),
            allowNull: false,
        }
    }, {
        scopes: {
            public: {
                attributes: { exclude: ['createdAt', 'updatedAt']}
            }
        }
    });

    Application.prototype.getPublic = function() {
        return {
            id: this.id,
            name: this.name,
            operatingSystem: this.operatingSystem
        }
    };

    return Application;
}