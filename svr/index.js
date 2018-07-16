const http = require('http');
const express = require('express');
const socketIo = require('socket.io');

const app = express();
const server = http.createServer(app);
const io = socketIo(server);

io.on('connect', (client) => {
	console.log('user connected');
  
  client.on('disconnect', () => {
    console.log('user disconnected');
  });
  
  client.on('status-ping', (data) => {
    client.emit('status-pong', data);
  });
});

server.listen(3000, () => {
  console.log('listening on *:3000');
});
