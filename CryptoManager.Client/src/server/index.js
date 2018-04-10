const express = require('express');
const publicweb = './publicweb';
const app = express();

app.use(express.static(publicweb));
console.log('serving ${publicweb}');
app.get('*', (req, res) => {
  res.sendFile('index.html', { root: publicweb });
});

const port = process.env.PORT || '3000';
app.listen(port);