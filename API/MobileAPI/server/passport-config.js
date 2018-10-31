const passport = require('passport');

//Import strategies
const LocalStrategy = require('passport-local').Strategy;

const JwtStrategy = require('passport-jwt').Strategy;
const { ExtractJwt } = require('passport-jwt');
const { User } = require('./database/models');

const GoogleAuthStrategy = require('passport-google-id-token');
const FacebookAuthStrategy = require('passport-facebook-token');

// JWT STRATEGY
passport.use(new JwtStrategy({
    jwtFromRequest: ExtractJwt.fromHeader('authorization'),
    secretOrKey: process.env.JWT_SECRET
}, async (payload, done) => {
    try {
        // Find the user specified in the token
        const user = await User.findOne({ where: {id: payload.sub} });

        // If user doesn't exist
        if(!user) {
            return done(null, false);
        }

        // Otherwise return user
        done(null, user);
    } catch(error) {
        done(error, false);
    }
}));

// LOCAL STRATEGY
passport.use(new LocalStrategy({
    usernameField: 'email'
}, async(email, password, done) => {
    try {
        // Find the user
        const user = await User.findOne({ where: { email } });

        if(!user) {
            return done(null, false);
        }

        // Check password
        if(!await user.isCorrectPassword(password)) {
            return done(null, false);
        }

        done(null, user);
    } catch(error) {
        done(error, false);
    }
}));

if(process.env.USE_GOOGLE_AUTH) {
    passport.use('googleToken', new GoogleAuthStrategy({
        clientID: process.env.GOOGLE_CLIENT_ID
    }, (parsedToken, google_id, done) => {
        try {
            User.findOrCreate({where: 
                {
                    googleId: google_id, 
                    email: parsedToken.payload.email,
                    authMethod: User.authMethods.GOOGLE
                }})
                .spread((user, created) => {
                    return done(null, user);
                });
        } catch(error) {
            done(error, false);
        }
    }));
}

if(process.env.USE_FACEBOOK_AUTH) {
    passport.use('facebookToken', new FacebookAuthStrategy({
        clientID: process.env.FACEBOOK_APP_ID,
        clientSecret: process.env.FACEBOOK_APP_SECRET
    }, (accessToken, refreshToken, profile, done) => {
        try {
            User.findOrCreate({where: 
                {
                    facebookId: profile.id, 
                    email: profile.emails[0].value,
                    authMethod: User.authMethods.FACEBOOK
                }})
                .spread((user, created) => {
                    return done(null, user);
                });
        } catch(error) {
            done(error, false);
        }
    }));
}