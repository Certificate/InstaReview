const passport = require('passport');

const LocalStrategy = require('passport-local').Strategy;

const JwtStrategy = require('passport-jwt').Strategy;
const { ExtractJwt } = require('passport-jwt');
const { User } = require('./database/models');

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

        // If user doesn't exist
        if(!user) {
            return done(null, false);
        }

        // Check password
        if(!await user.isCorrectPassword(password)) {
            return done(null, false);
        }

        // Otherwise return user
        done(null, user);
    } catch(error) {
        done(error, false);
    }
}));