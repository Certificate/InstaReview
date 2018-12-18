# InstaReview API

This is the API for the InstaReview mobile application used to store reviews made on the app. Node.js (v8.12 was used during development) and a MySQL-server are required to use this application.

Please note that at this time the App works with MySQL server versions that came before the version 8 release (e.g v5.7). However you might be able to get it running with never ones if you try and update the MySQL driver used by Node, assuming the driver has been updates for the currently used version.

## Project structure (most important stuff)
- \_\_tests\_\_
    - Contains the unit tests
- api_documentation
    - Contains documentation on the API routes
- server
    - Contains all the controllers, routes, database models, etc.
    - Routes
        - Different routings for the API
    - Controllers
        - The actual "business logic". Routes call functions defined in here.
    - Database
        - The individual data models
        - models.js ties the models together into a package and also is used to define associations.
    - passport-config.js
        - Here lies the business logic for the different authentication methods.
    - categories.json
        - The categories as a list (JSON) that will be imported into the database on app launch.
- index.js
    - The "main class" of the application.
- .env/.env.example
    - The (environmental) settings the app uses

## Commands:
- npm run start-dev
    - Starts the server in development mode
    - Requires that you have separately installed nodemon either glovbally or to development dependencies.
- npm run start
    - Starts the server in production mode
- npm run test
    - Runs the unit tests defined in the \_\_tests\_\_ folder.
    - Tests are made for routes and controllers separately.
- npm run generate-documentation
    - Regenerates the documentation found in the api-documentation folder based on the written Javadoc-style comments

## Installation
- Run "npm install" in the directory
- Prepare a SQL server with an empty schema for the API
    - You need not worry about tables, the app will create them as it launches
- Copy/Rename the .env.example as .env and change the settings according to the environment
    - DB_CONNECTION is the connection string used to communicate with the SQL server
    - IMAGE_SAVE_DIR defines the location the images for the reviews are stored on disk, this is optional and the default location is the same as the one in .env.example.
    - THUMBNAIL_SAVE_DIR defines the location review thumbnails on disk, this is optional and the default location is the same as the one in .env.example.
    - THUMBNAIL_SIZE size of the generated thumbnails (in pixels)
    - CATEGORIES_FILE is the location of the list of categories to be imported into the database.
    - JWT_SECRET is some random secret string that should be kept secret. This is used to sign the tokens that are used to authenticate users. This is vital in order for the API to be able to confirm the authenticity of the data sent to it.
    - Google OAUTH2.0
        - USE_GOOGLE_AUTH toggles Google logins/registrations on or off.
        - GOOGLE_CLIENT_ID is the id given by the Google OAUTH2.0 API once you register the application there. You can leave this empty or unchanged if the logins are off.
    - Facebook OAUTH2.0
        - USE_FACEBOOK_AUTH toggles Facebook logins/registrations on or off
        - FACEBOOK_APP_ID the id for the application given by the FB API service once you register the app there. You can leave this empty or unchanged if the logins are off.
        - FACEBOOK_APP_SECRET the secret given by the FB API upon registration. You can leave this empty or unchanged if the logins are off.
    - Run the application (see "Commands" above)


## Docker

The app is very easy to run in Docker. By default, the Docker will expose port 80, so change this within the Dockerfile if necessary. When ready, simply use the Dockerfile to build the image. You might want to use volumes to store the images/thumbnails outside of the container, because containers cannot be used as persistent storage.

You will also find files for docker-compose in the directory above (from this file). However you might need/want to reconfigure them a bit first.