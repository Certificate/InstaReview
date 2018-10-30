const { app, dbSync } = require('./server/app');

dbSync().then(() => {    
    //Start server
    const port = process.env.PORT || 80;
    const host = process.env.HOST || '0.0.0.0';

    app.listen(port, host, () => console.log(`API is running on ${host}:${port}`));
});