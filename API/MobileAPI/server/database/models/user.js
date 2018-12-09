const bcrypt = require('bcryptjs');

module.exports = (sequelize, DataTypes) => {
    var User = sequelize.define('user', {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },
        email: {
            type: DataTypes.STRING,
            allowNull: false
        },
        authMethod: {
            type: DataTypes.ENUM('local', 'google', 'facebook'),
            allowNull: false
        },
        password: {
            type: DataTypes.STRING,
            allowNull: true
        },
        googleId: {
            type: DataTypes.STRING,
            allowNull: true
        },
        facebookId: {
            type: DataTypes.STRING,
            allowNull: true
        },
        name: {
            type: DataTypes.STRING,
            allowNull: true
        },
        gender: {
            type: DataTypes.ENUM('Male', 'Female'),
            allowNull: true
        },
        birthday: {
            type: DataTypes.DATEONLY,
            allowNull: true
        }
    },
    {
        indexes: [
            {
                unique: true,
                fields: ["email"]
            }
        ],
        scopes: {
            public: {
                attributes: { exclude: ['password', 'authMethod', 'googleId', 'facebookId']}
            }
        }
    });

    //Use hook to encrypt passwords
    User.addHook('beforeSave', async (user, options) => {
        try {
            if(user.authMethod !== 'local' || (!user.changed('password') && !user.isNewRecord)) {
                return Promise.resolve();
            }

            const salt = await bcrypt.genSalt(10);
            user.password = await bcrypt.hash(user.password, salt);
            return Promise.resolve();
        } catch(error) {
            return Promise.reject(error);
        }
    });

    User.prototype.isCorrectPassword = async function (password) {
        try {
            return await bcrypt.compare(password, this.password);
        } catch (error) {
            throw error;
        }
    }

    User.prototype.getPublic = function() {
        return {
            email: this.email,
            name: this.name,
            gender: this.gender,
            birthday: this.birthday
        }
    }

    //Set constants for user auth names
    User.authMethods = {
        LOCAL: 'local',
        GOOGLE: 'google',
        FACEBOOK: 'facebook'
    }

    return User;
}