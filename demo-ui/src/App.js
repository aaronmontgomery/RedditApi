import './App.css';
import React, { useEffect, useState } from 'react';

function App() {
  
  const [data, setData] = useState('Loading . . . . ')
  
  useEffect(() => {
    
    const socket = new WebSocket(process.env.REACT_APP_WSS_URL);
    
    socket.onopen = function(event) {
      console.log('WebSocket connection established.');
    };
    
    socket.onmessage = function(event) {
      console.log('Received message:', event.data);
      setData(event.data);
    };
    
    socket.onclose = function(event) {
      if (event.wasClean) {
        console.log(`WebSocket connection closed cleanly, code=${event.code} reason=${event.reason}`);
      }
      
      else {
        console.error('WebSocket connection abruptly closed.');
      }
    };
    
    socket.onerror = function(error) {
      console.error('WebSocket encountered error:', error);
    };

  }, [])
  
  return (
    <div>
      <div>{data}</div>
    </div>
  );
}

export default App;
