const express = require('express');
const app = express();
const port = 80;
const host = '0.0.0.0';

app.get('/HelloWorld', (req, res) => res.send('Hello World!'));

app.listen(port, host, () => console.log(`App running on port http://${host}:${port}`));