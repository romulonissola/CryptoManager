const express = require('express');
const publicweb = './publicweb';
const app = express();

app.use(express.static(publicweb));
console.log('serving ${publicweb}');
app.get('*', (req, res) => {
  res.sendFile('index.html', { root: publicweb });
});

const port = process.env.SERVER_PORT || '80';
app.listen(port);