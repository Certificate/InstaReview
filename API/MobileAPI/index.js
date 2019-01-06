const { app, dbSync } = require('./server/app');

function approveDomains(opts, certs, cb) {
    opts.communityMember = false;
    opts.email = process.env.SSL_EMAIL;
    opts.agreeTos = true;
    opts.domains = [ process.env.SERVER_IP ];

    cb(null, {options: opts, certs});
}

dbSync().then(() => {    
    const port = process.env.PORT || 80;
    const host = process.env.HOST || '0.0.0.0';
    //Start server
    if(process.env.USE_SSL === 'false') {
        app.listen(port, host, () => console.log(`API is running on ${host}:${port}`));
    } else {
        //Use SSL
        const server = require('greenlock-express').create({
            server: 'https://acme-v02.api.letsencrypt.org/directory'
            , version: 'draft-11'
            , telemetry: false
            , servername: process.env.SERVER_IP
            , approveDomains: approveDomains
            , configDir: './ssl/acme'
        });

        require('http').createServer(server.middleware(require('redirect-https')())).listen(80, function () {
            console.log("Listening for ACME http-01 challenges on", this.address());
        });

        require('https').createServer(server.httpsOptions, app).listen(443, function () {
            console.log("Listening for ACME tls-sni-01 challenges and serve app on", this.address());
        });
    }
});