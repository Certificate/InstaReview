define({ "api": [
  {
    "type": "post",
    "url": "/application/add",
    "title": "Add a new application data",
    "name": "AddApplication",
    "group": "Application",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "name",
            "description": "<p>Name of the application</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "operatingSystem",
            "description": "<p>Operating system of the application. Valid values are [&quot;Android&quot;, &quot;iOS&quot;].</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n  \"name\": \"Google\",\n  \"operatingSystem\": \"Android\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Database ID of the data</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "name",
            "description": "<p>Name of the application</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Timestamp of last modification</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Timestamp of creation</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"name\": \"Application Name\",\n    \"operatingSystem\": \"Android\",\n    \"updatedAt\": \"1970-01-01T00:00:00.00Z\",\n    \"createdAt\": \"1970-01-01T00:00:00.00Z\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ApplicationExists",
            "description": "<p>Application data already exists for that operating system</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ValidationError",
            "description": "<p>Failed to validate body</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "ApplicationExists",
          "content": "HTTP/1.1 403 Bad Request\n{\n    \"error\": \"That application already exists for that operating system.\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        },
        {
          "title": "ValidationError",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"name\": \"ValidationError\",\n    \"details\": [\n        {\n            ...\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/application.js",
    "groupTitle": "Application"
  },
  {
    "type": "get",
    "url": "/application/find/:searchParameter",
    "title": "Find application data by parameter",
    "name": "GetApplicationByName",
    "group": "Application",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "searchParameter",
            "description": "<p>ID/Name/Operating system of application(s)</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Application[]",
            "optional": false,
            "field": "applications",
            "description": "<p>List of applications matching given parameter</p>"
          }
        ]
      }
    },
    "version": "0.0.0",
    "filename": "server/routes/application.js",
    "groupTitle": "Application"
  },
  {
    "type": "get",
    "url": "/application/list",
    "title": "Lists data of all saved applications",
    "name": "GetApplicationsAll",
    "group": "Application",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Application[]",
            "optional": false,
            "field": "applications",
            "description": "<p>List of the applications</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n[\n    {\n        \"id\": 1,\n        \"name\": \"Application Name\",\n        \"operatingSystem\": \"Android\",\n    }\n]",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/application.js",
    "groupTitle": "Application",
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "post",
    "url": "/auth/signup",
    "title": "Register a new user",
    "name": "AddUser",
    "group": "Authentication",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "email",
            "description": "<p>Email to register with</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "password",
            "description": "<p>Plain-text password, will be encrypted before saving</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "name",
            "description": "<p>Name of the user (optional)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "gender",
            "description": "<p>Gender of the user, valid values are [&quot;Male&quot;, &quot;Female&quot;] (optional)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "birthday",
            "description": "<p>Date of birth of the user (optional)</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n    \"email\": \"example@example.com\",\n    \"password\": \"password\",\n    \"name\": \"Tom Tester\",\n    \"gender\": \"Male\",\n    \"birthday\": \"12/24/2018\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserExists",
            "description": "<p>User already exists</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ValidationError",
            "description": "<p>Failed to validate body</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "UserExists",
          "content": "HTTP/1.1 409 Conflict\n{\n    \"error\": \"User already exists.\"\n}",
          "type": "json"
        },
        {
          "title": "ValidationError",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"name\": \"ValidationError\",\n    \"details\": [\n        {\n            ...\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/auth.js",
    "groupTitle": "Authentication",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "token",
            "description": "<p>Authorization token</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"token\": \"signed.JWT.token\"\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "post",
    "url": "/auth/login",
    "title": "Login with an account",
    "name": "LoginUser",
    "group": "Authentication",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "email",
            "description": "<p>Email to login with</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "password",
            "description": "<p>Plain-text password</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n    \"email\": \"example@example.com\",\n    \"password\": \"password\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "WrongLoginDetails",
            "description": "<p>Wrong username or password</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ValidationError",
            "description": "<p>Failed to validate body</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "WrongLoginDetails",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        },
        {
          "title": "ValidationError",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"name\": \"ValidationError\",\n    \"details\": [\n        {\n            ...\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/auth.js",
    "groupTitle": "Authentication",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "token",
            "description": "<p>Authorization token</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"token\": \"signed.JWT.token\"\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "post",
    "url": "/auth/facebook",
    "title": "Login/Register with a Facebook account",
    "name": "LoginUserFacebook",
    "group": "Authentication",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "access_token",
            "description": "<p>Token returned by Facebook after OAuth is done on the mobile app</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "AuthenticationFailed",
            "description": "<p>Failed to trade the given token for user information with Facebook</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "AuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/auth.js",
    "groupTitle": "Authentication",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "token",
            "description": "<p>Authorization token</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"token\": \"signed.JWT.token\"\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "post",
    "url": "/auth/google",
    "title": "Login/Register with a Google account",
    "name": "LoginUserGoogle",
    "group": "Authentication",
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "id_token",
            "description": "<p>Token returned by Google after OAuth is done on the mobile app</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "AuthenticationFailed",
            "description": "<p>Failed to confirm and use the given token</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "AuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/auth.js",
    "groupTitle": "Authentication",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "token",
            "description": "<p>Authorization token</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"token\": \"signed.JWT.token\"\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "post",
    "url": "/review/create",
    "title": "Create a new Review",
    "name": "CreateReview",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Number",
            "optional": false,
            "field": "appId",
            "description": "<p>Database ID of the application this review is about</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "categoryName",
            "description": "<p>Name of the category of the review (See the list of categories, found in ie. the api-documentation folder, server uses ones defined in categories.json)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "temporalContext",
            "description": "<p>Temporal context of the review. Valid values are [&quot;Intensive&quot;, &quot;Allocative&quot;].</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "spatialContext",
            "description": "<p>Spatial context of the review. Valid values are [&quot;Visiting&quot;, &quot;Traveling&quot;, &quot;Wandering&quot;].</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "socialContext",
            "description": "<p>Social context of the review. Valid values are ['Constraining', 'Encouraging'].</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "textReview",
            "description": "<p>The text review itself.</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n    \"appId\": 1,\n    \"temporalContext\": \"Intensive\",\n    \"spatialContext\": \"Visiting\",\n    \"socialContext\": \"Constraining\",\n    \"textReview\": \"This is a review.\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ApplicationNotFound",
            "description": "<p>Failed to fetch data for the application the review is supposedly written about.</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ValidationError",
            "description": "<p>Failed to validate body</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "ApplicationNotFound",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"Could not find application data with given appId\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        },
        {
          "title": "ValidationError",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"name\": \"ValidationError\",\n    \"details\": [\n        {\n            ...\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Id of the review in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "userId",
            "description": "<p>Id of the user in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "appId",
            "description": "<p>Database ID of the application this review is about</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "temporalContext",
            "description": "<p>Temporal context of the review. Valid values are [&quot;Intensive&quot;, &quot;Allocative&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "spatialContext",
            "description": "<p>Spatial context of the review. Valid values are [&quot;Visiting&quot;, &quot;Traveling&quot;, &quot;Wandering&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "socialContext",
            "description": "<p>Social context of the review. Valid values are ['Constraining', 'Encouraging'].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "textReview",
            "description": "<p>The text review itself.</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Timestamp of last modification</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Timestamp of creation</p>"
          },
          {
            "group": "Success 200",
            "type": "Image[]",
            "optional": false,
            "field": "images",
            "description": "<p>List of Image-objects containing information of the screenshots for this review. This is only included when fetching a single review (see &quot;Request Review Information&quot;).</p>"
          },
          {
            "group": "Success 200",
            "type": "Application",
            "optional": false,
            "field": "application",
            "description": "<p>The application object corresponding the appId. Object structure can be seen in the documentation for the Application routes.</p>"
          },
          {
            "group": "Success 200",
            "type": "Category",
            "optional": false,
            "field": "category",
            "description": "<p>Category of the review corresponding the categoryId. The most relevant value within this object is the attribute categoryName.</p>"
          },
          {
            "group": "Success 200",
            "type": "Thumbnail",
            "optional": false,
            "field": "thumbnail",
            "description": "<p>Thumbnail object containing the fileName-attribute required for downloading thumbnails. This is only included when fetching reviews.</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"userId\": 1,\n    \"appId\": 1,\n    \"categoryId\": 1,\n    \"thumbnailId\": 1,\n    \"temporalContext\": \"Allocative\",\n    \"spatialContext\": \"Wandering\",\n    \"socialContext\": \"Encouraging\",\n    \"textReview\": \"I found an issue with...\",\n    \"updatedAt\": \"1970-01-01T00:00:00.00Z\",\n    \"createdAt\": \"1970-01-01T00:00:00.00Z\",\n    \"images\": [],\n    \"application\": {\n       \"id\": 1,\n       \"name\": \"Test Application\",\n       \"operatingSystem\": \"Android\"\n    },\n    \"category\": {\n       \"id\": 1,\n       \"categoryName\": \"Lagging\"\n    },\n    \"thumbnail\": {\n       \"id\": 1,\n       \"fileName\": \"thumbnail-1.png\"\n    }\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "get",
    "url": "/review/image/download/:filename",
    "title": "Download an image by filename",
    "name": "DownloadReviewImage",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "filename",
            "description": "<p>Filename of the image</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "image/format",
            "optional": false,
            "field": "image",
            "description": "<p>The image file</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "MissingFilename",
            "description": "<p>Filename parameter is empty</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ImageNotFound",
            "description": "<p>Failed to retrieve an image with given filename</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ReviewNotFound",
            "description": "<p>Failed to retrieve a review containing the image.</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "NoAccessPermission",
            "description": "<p>The provided user doesn't have permission to this image (not the one that created the review)</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "FileNotFound",
            "description": "<p>Failed to find the actual image on the disk. (Shouldn't happen as long as images aren't being removed manually on the server)</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "MissingFilename",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"Filename missing!\"\n}",
          "type": "json"
        },
        {
          "title": "ImageNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Image doesn't exist\"\n}",
          "type": "json"
        },
        {
          "title": "ReviewNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Couldn't find a review containing the image\"\n}",
          "type": "json"
        },
        {
          "title": "NoAccessPermission",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"No permissions to access the file\"\n}",
          "type": "json"
        },
        {
          "title": "FileNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Failed to retrieve the image file\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    },
    "description": "<p>Basically should never happen due to database constrictions.</p>",
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review"
  },
  {
    "type": "get",
    "url": "/review/thumbnail/:id",
    "title": "Download the review thumbnail",
    "name": "DownloadThumbnail",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "id",
            "description": "<p>Id of the review</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "image/png",
            "optional": false,
            "field": "thumbnail",
            "description": "<p>The thumbnail image</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "NoReviewID",
            "description": "<p>No review ID was given (parameter missing).</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ReviewNotFound",
            "description": "<p>Failed to find a review with the id and user credentials</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ThumbnailNotCreated",
            "description": "<p>Thumbnail hasn't been created yet</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ThumbnailNotFound",
            "description": "<p>Failed to download the file from disk</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "NoReviewID",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"No review id given!\"\n}",
          "type": "json"
        },
        {
          "title": "ReviewNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Could not find a review with given id and credentials\"\n}",
          "type": "json"
        },
        {
          "title": "ThumbnailNotCreated",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"A thumbnail has not been created yet for this review\"\n}",
          "type": "json"
        },
        {
          "title": "ThumbnailNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Failed to locate the thumbnail\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review"
  },
  {
    "type": "post",
    "url": "/review/edit",
    "title": "Edit a review",
    "name": "EditReview",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Id of the review to be edited</p>"
          },
          {
            "group": "Parameter",
            "type": "Number",
            "optional": false,
            "field": "appId",
            "description": "<p>Database ID of the application this review is about</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "categoryName",
            "description": "<p>Name of the category of the review (See the list of categories, found in ie. the api-documentation folder, server uses ones defined in categories.json)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "temporalContext",
            "description": "<p>Temporal context of the review. Valid values are [&quot;Intensive&quot;, &quot;Allocative&quot;].</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "spatialContext",
            "description": "<p>Spatial context of the review. Valid values are [&quot;Visiting&quot;, &quot;Traveling&quot;, &quot;Wandering&quot;].</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "socialContext",
            "description": "<p>Social context of the review. Valid values are ['Constraining', 'Encouraging'].</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "textReview",
            "description": "<p>The text review itself.</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n    \"id\": 1,\n    \"appId\": 1,\n    \"temporalContext\": \"Intensive\",\n    \"spatialContext\": \"Visiting\",\n    \"socialContext\": \"Constraining\",\n    \"textReview\": \"This is a review.\"\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "NoReviewID",
            "description": "<p>No review ID was given (parameter missing).</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ApplicationNotFound",
            "description": "<p>Failed to fetch data for the application the review is supposedly written about.</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ReviewNotFound",
            "description": "<p>Failed to find a review with the id and user credentials</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ValidationError",
            "description": "<p>Failed to validate body</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "NoReviewID",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"No id for the review was given for it to be edited\"\n}",
          "type": "json"
        },
        {
          "title": "ApplicationNotFound",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"Could not find application data with given appId\"\n}",
          "type": "json"
        },
        {
          "title": "ReviewNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Could not find a review with given id and credentials\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        },
        {
          "title": "ValidationError",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"name\": \"ValidationError\",\n    \"details\": [\n        {\n            ...\n        }\n    ]\n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Id of the review in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "userId",
            "description": "<p>Id of the user in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "appId",
            "description": "<p>Database ID of the application this review is about</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "temporalContext",
            "description": "<p>Temporal context of the review. Valid values are [&quot;Intensive&quot;, &quot;Allocative&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "spatialContext",
            "description": "<p>Spatial context of the review. Valid values are [&quot;Visiting&quot;, &quot;Traveling&quot;, &quot;Wandering&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "socialContext",
            "description": "<p>Social context of the review. Valid values are ['Constraining', 'Encouraging'].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "textReview",
            "description": "<p>The text review itself.</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Timestamp of last modification</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Timestamp of creation</p>"
          },
          {
            "group": "Success 200",
            "type": "Image[]",
            "optional": false,
            "field": "images",
            "description": "<p>List of Image-objects containing information of the screenshots for this review. This is only included when fetching a single review (see &quot;Request Review Information&quot;).</p>"
          },
          {
            "group": "Success 200",
            "type": "Application",
            "optional": false,
            "field": "application",
            "description": "<p>The application object corresponding the appId. Object structure can be seen in the documentation for the Application routes.</p>"
          },
          {
            "group": "Success 200",
            "type": "Category",
            "optional": false,
            "field": "category",
            "description": "<p>Category of the review corresponding the categoryId. The most relevant value within this object is the attribute categoryName.</p>"
          },
          {
            "group": "Success 200",
            "type": "Thumbnail",
            "optional": false,
            "field": "thumbnail",
            "description": "<p>Thumbnail object containing the fileName-attribute required for downloading thumbnails. This is only included when fetching reviews.</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"userId\": 1,\n    \"appId\": 1,\n    \"categoryId\": 1,\n    \"thumbnailId\": 1,\n    \"temporalContext\": \"Allocative\",\n    \"spatialContext\": \"Wandering\",\n    \"socialContext\": \"Encouraging\",\n    \"textReview\": \"I found an issue with...\",\n    \"updatedAt\": \"1970-01-01T00:00:00.00Z\",\n    \"createdAt\": \"1970-01-01T00:00:00.00Z\",\n    \"images\": [],\n    \"application\": {\n       \"id\": 1,\n       \"name\": \"Test Application\",\n       \"operatingSystem\": \"Android\"\n    },\n    \"category\": {\n       \"id\": 1,\n       \"categoryName\": \"Lagging\"\n    },\n    \"thumbnail\": {\n       \"id\": 1,\n       \"fileName\": \"thumbnail-1.png\"\n    }\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "get",
    "url": "/review/categories",
    "title": "Fetch a list of available categories",
    "name": "FetchCategories",
    "group": "Review",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "String[]",
            "optional": false,
            "field": "categories",
            "description": "<p>List of categories</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n[\n    \"Lagging\",\n    \"Functional Error\",\n    ...\n]",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review"
  },
  {
    "type": "get",
    "url": "/review/get/:id",
    "title": "Request Review information",
    "name": "GetReview",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Review unique ID.</p>"
          }
        ]
      }
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "NoReviewID",
            "description": "<p>No review ID was given (parameter missing).</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ReviewNotFound",
            "description": "<p>Failed to find a review with the id and user credentials</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "NoReviewID",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"No review id given!\"\n}",
          "type": "json"
        },
        {
          "title": "ReviewNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"Could not find a review with given id and credentials\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Id of the review in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "userId",
            "description": "<p>Id of the user in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "appId",
            "description": "<p>Database ID of the application this review is about</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "temporalContext",
            "description": "<p>Temporal context of the review. Valid values are [&quot;Intensive&quot;, &quot;Allocative&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "spatialContext",
            "description": "<p>Spatial context of the review. Valid values are [&quot;Visiting&quot;, &quot;Traveling&quot;, &quot;Wandering&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "socialContext",
            "description": "<p>Social context of the review. Valid values are ['Constraining', 'Encouraging'].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "textReview",
            "description": "<p>The text review itself.</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Timestamp of last modification</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Timestamp of creation</p>"
          },
          {
            "group": "Success 200",
            "type": "Image[]",
            "optional": false,
            "field": "images",
            "description": "<p>List of Image-objects containing information of the screenshots for this review. This is only included when fetching a single review (see &quot;Request Review Information&quot;).</p>"
          },
          {
            "group": "Success 200",
            "type": "Application",
            "optional": false,
            "field": "application",
            "description": "<p>The application object corresponding the appId. Object structure can be seen in the documentation for the Application routes.</p>"
          },
          {
            "group": "Success 200",
            "type": "Category",
            "optional": false,
            "field": "category",
            "description": "<p>Category of the review corresponding the categoryId. The most relevant value within this object is the attribute categoryName.</p>"
          },
          {
            "group": "Success 200",
            "type": "Thumbnail",
            "optional": false,
            "field": "thumbnail",
            "description": "<p>Thumbnail object containing the fileName-attribute required for downloading thumbnails. This is only included when fetching reviews.</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"userId\": 1,\n    \"appId\": 1,\n    \"categoryId\": 1,\n    \"thumbnailId\": 1,\n    \"temporalContext\": \"Allocative\",\n    \"spatialContext\": \"Wandering\",\n    \"socialContext\": \"Encouraging\",\n    \"textReview\": \"I found an issue with...\",\n    \"updatedAt\": \"1970-01-01T00:00:00.00Z\",\n    \"createdAt\": \"1970-01-01T00:00:00.00Z\",\n    \"images\": [],\n    \"application\": {\n       \"id\": 1,\n       \"name\": \"Test Application\",\n       \"operatingSystem\": \"Android\"\n    },\n    \"category\": {\n       \"id\": 1,\n       \"categoryName\": \"Lagging\"\n    },\n    \"thumbnail\": {\n       \"id\": 1,\n       \"fileName\": \"thumbnail-1.png\"\n    }\n}",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "get",
    "url": "/review/list",
    "title": "Request Reviews made by user",
    "name": "GetReviewsAll",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review",
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Id of the review in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "userId",
            "description": "<p>Id of the user in the database</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "appId",
            "description": "<p>Database ID of the application this review is about</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "temporalContext",
            "description": "<p>Temporal context of the review. Valid values are [&quot;Intensive&quot;, &quot;Allocative&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "spatialContext",
            "description": "<p>Spatial context of the review. Valid values are [&quot;Visiting&quot;, &quot;Traveling&quot;, &quot;Wandering&quot;].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "socialContext",
            "description": "<p>Social context of the review. Valid values are ['Constraining', 'Encouraging'].</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "textReview",
            "description": "<p>The text review itself.</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Timestamp of last modification</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Timestamp of creation</p>"
          },
          {
            "group": "Success 200",
            "type": "Image[]",
            "optional": false,
            "field": "images",
            "description": "<p>List of Image-objects containing information of the screenshots for this review. This is only included when fetching a single review (see &quot;Request Review Information&quot;).</p>"
          },
          {
            "group": "Success 200",
            "type": "Application",
            "optional": false,
            "field": "application",
            "description": "<p>The application object corresponding the appId. Object structure can be seen in the documentation for the Application routes.</p>"
          },
          {
            "group": "Success 200",
            "type": "Category",
            "optional": false,
            "field": "category",
            "description": "<p>Category of the review corresponding the categoryId. The most relevant value within this object is the attribute categoryName.</p>"
          },
          {
            "group": "Success 200",
            "type": "Thumbnail",
            "optional": false,
            "field": "thumbnail",
            "description": "<p>Thumbnail object containing the fileName-attribute required for downloading thumbnails. This is only included when fetching reviews.</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"userId\": 1,\n    \"appId\": 1,\n    \"categoryId\": 1,\n    \"thumbnailId\": 1,\n    \"temporalContext\": \"Allocative\",\n    \"spatialContext\": \"Wandering\",\n    \"socialContext\": \"Encouraging\",\n    \"textReview\": \"I found an issue with...\",\n    \"updatedAt\": \"1970-01-01T00:00:00.00Z\",\n    \"createdAt\": \"1970-01-01T00:00:00.00Z\",\n    \"images\": [],\n    \"application\": {\n       \"id\": 1,\n       \"name\": \"Test Application\",\n       \"operatingSystem\": \"Android\"\n    },\n    \"category\": {\n       \"id\": 1,\n       \"categoryName\": \"Lagging\"\n    },\n    \"thumbnail\": {\n       \"id\": 1,\n       \"fileName\": \"thumbnail-1.png\"\n    }\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "post",
    "url": "/review/image/upload",
    "title": "Upload an image for a review",
    "description": "<p>Unlike others, this requests body uses content-type 'multipart/form-data' to send files.</p>",
    "name": "UploadReviewImage",
    "group": "Review",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "FILE",
            "optional": false,
            "field": "screenshot",
            "description": "<p>Image file to upload</p>"
          },
          {
            "group": "Parameter",
            "type": "Number",
            "optional": false,
            "field": "reviewId",
            "description": "<p>ID of the review to link the image to.</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Database ID of the Image-object</p>"
          },
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "reviewId",
            "description": "<p>ID of the review the image is linked to</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "fileName",
            "description": "<p>Filename of the actual image on disk</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Timestamp of last modification</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Timestamp of creation</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"reviewId\": 1,\n    \"fileName\": \"screenshot-1541941036667.jpg\",\n    \"updatedAt\": \"1970-01-01T00:00:00.00Z\",\n    \"createdAt\": 1970-01-01T00:00:00.00Z\n}",
          "type": "json"
        }
      ]
    },
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ImageNotReceived",
            "description": "<p>Image wasn't received</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "ReviewNotFound",
            "description": "<p>Review ID wasn't given or couldn't find a review with the ID.</p>"
          },
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "ImageNotReceived",
          "content": "HTTP/1.1 400 Bad Request\n{\n    \"error\": \"No image received.\"\n}",
          "type": "json"
        },
        {
          "title": "ReviewNotFound",
          "content": "HTTP/1.1 404 Not Found\n{\n    \"error\": \"No review id was given or couldn't find a review with given id.\"\n}",
          "type": "json"
        },
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/review.js",
    "groupTitle": "Review"
  },
  {
    "type": "post",
    "url": "/user/edit",
    "title": "Edit user information",
    "name": "EditUserInfo",
    "group": "User",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "parameter": {
      "fields": {
        "Parameter": [
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "email",
            "description": "<p>Email to be changed to (optional)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "password",
            "description": "<p>New password (optional)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "name",
            "description": "<p>Name to be changed to (optional)</p>"
          },
          {
            "group": "Parameter",
            "type": "String",
            "optional": false,
            "field": "gender",
            "description": "<p>Gender to be changed to, valid values are ['Male', 'Female'] (optional)</p>"
          },
          {
            "group": "Parameter",
            "type": "Date",
            "optional": false,
            "field": "birthday",
            "description": "<p>Birthday to be changed to (optional)</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Request-Example:",
          "content": "{\n    \"email\": \"newEmail@email.com\",\n    \"name\": \"New Name\",\n    \"password\": \"thisisadifferentpassword\",\n    \"gender\": \"Male\",\n    \"birthday\": \"12/24/2018\"\n}",
          "type": "json"
        }
      ]
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Database id of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "email",
            "description": "<p>Email address of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "name",
            "description": "<p>Name of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "gender",
            "description": "<p>Gender of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "Date",
            "optional": false,
            "field": "birthday",
            "description": "<p>Birthday of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "Date",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Time of the user's creation</p>"
          },
          {
            "group": "Success 200",
            "type": "Date",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Time of the user's last change</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"email\": \"email@gmail.com\",\n    \"name\": \"Tom Tester\",\n    \"gender\": \"Male\",\n    \"birthday\": \"1/1/1990\",\n    \"createdAt\": \"2018-12-09T14:07:57.000Z\",\n    \"updatedAt\": \"2018-12-09T14:07:57.000Z\" \n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/user.js",
    "groupTitle": "User",
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    }
  },
  {
    "type": "get",
    "url": "/user/info",
    "title": "Fetch user information",
    "name": "GetUserInfo",
    "group": "User",
    "header": {
      "fields": {
        "Authentication": [
          {
            "group": "Authentication",
            "type": "String",
            "optional": false,
            "field": "Authorization",
            "description": "<p>Authorization token</p>"
          }
        ]
      }
    },
    "success": {
      "fields": {
        "Success 200": [
          {
            "group": "Success 200",
            "type": "Number",
            "optional": false,
            "field": "id",
            "description": "<p>Database id of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "email",
            "description": "<p>Email address of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "name",
            "description": "<p>Name of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "String",
            "optional": false,
            "field": "gender",
            "description": "<p>Gender of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "Date",
            "optional": false,
            "field": "birthday",
            "description": "<p>Birthday of the user</p>"
          },
          {
            "group": "Success 200",
            "type": "Date",
            "optional": false,
            "field": "createdAt",
            "description": "<p>Time of the user's creation</p>"
          },
          {
            "group": "Success 200",
            "type": "Date",
            "optional": false,
            "field": "updatedAt",
            "description": "<p>Time of the user's last change</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "Success-Response:",
          "content": "HTTP/1.1 200 OK\n{\n    \"id\": 1,\n    \"email\": \"email@gmail.com\",\n    \"name\": \"Tom Tester\",\n    \"gender\": \"Male\",\n    \"birthday\": \"1/1/1990\",\n    \"createdAt\": \"2018-12-09T14:07:57.000Z\",\n    \"updatedAt\": \"2018-12-09T14:07:57.000Z\" \n}",
          "type": "json"
        }
      ]
    },
    "version": "0.0.0",
    "filename": "server/routes/user.js",
    "groupTitle": "User",
    "error": {
      "fields": {
        "Error 4xx": [
          {
            "group": "Error 4xx",
            "optional": false,
            "field": "UserAuthenticationFailed",
            "description": "<p>Failed to authenticate user with the authentication token (might be missing).</p>"
          }
        ]
      },
      "examples": [
        {
          "title": "UserAuthenticationFailed",
          "content": "HTTP/1.1 401 Unauthorized\nUnauthorized",
          "type": "json"
        }
      ]
    }
  }
] });
