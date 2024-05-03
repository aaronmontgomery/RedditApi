import './App.css';
import React, { useEffect, useState } from 'react';

function App() {
  
  const [data, setData] = useState('Loading . . . . ')
  
  useEffect(() => {
    
    // Establish WebSocket connection
    const socket = new WebSocket('wss://localhost:7038/reddit');
    
    // WebSocket event handlers
    socket.onopen = function(event) { //debugger;
      console.log('WebSocket connection established.');
    };
    
    socket.onmessage = function(event) { //debugger;
      // Handle incoming message
      console.log('Received message:', event.data);
      setData(event.data);
    };
    
    socket.onclose = function(event) { //debugger;
      if (event.wasClean) {
        console.log(`WebSocket connection closed cleanly, code=${event.code} reason=${event.reason}`);
      }
      
      else {
        console.error('WebSocket connection abruptly closed.');
      }
    };
    
    socket.onerror = function(error) { //debugger;
      console.error('WebSocket encountered error:', error);
    };
    
    // Sending messages (optional)
    // You can send messages after the WebSocket connection is established
    // socket.send('Hello, server!');
    
    // Close WebSocket connection (optional)
    // socket.close();
  }, [])
  
  return (
    <div>
      <div>{data}</div>
    </div>
  );
}

export default App;
