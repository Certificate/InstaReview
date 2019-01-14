module.exports = (sequelize, DataTypes) => {
    var ReviewCategory = sequelize.define('reviewCategory', {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
        },
        categoryName: {
            type: DataTypes.STRING,
            allowNull: false,
        }
    }, 
    {
        indexes: [
            {
                unique: true,
                fields: ["categoryName"]
            }
        ]
    });

    return ReviewCategory;
}