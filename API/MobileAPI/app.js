const express = require('express');
const app = express();
const port = 80;

app.get('/HelloWorld', (req, res) => res.send('Hello World!'));

app.listen(port, () => console.log(`App running on port ${port}`));