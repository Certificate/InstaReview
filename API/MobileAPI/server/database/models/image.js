module.exports = (sequelize, DataTypes) => {
    var Image = sequelize.define('image', {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },
        reviewId: {
            type: DataTypes.INTEGER,
            allowNull: false,
            references: {
                model: 'reviews',
                key: 'id'
            }
        },
        fileName: {
            type: DataTypes.STRING,
            allowNull: false,
        }
    }, 
    {
        indexes: [
            {
                unique: true,
                fields: ["fileName"]
            }
        ]
    });

    return Image;
}