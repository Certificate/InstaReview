module.exports = (sequelize, DataTypes) => {
    var Thumbnail = sequelize.define('thumbnail', {
        id: {
            type: DataTypes.INTEGER,
            primaryKey: true,
            autoIncrement: true
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

    return Thumbnail;
}