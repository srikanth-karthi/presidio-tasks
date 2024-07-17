import React from 'react';

const Box = ({ char, color, clickListener }) => {
  return (
    <div 
      onClick={clickListener} 
      style={{
        width: '50px',
        height: '50px',
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: color,
        border: '1px solid black'
      }}
    >
      {char}
    </div>
  );
};

export default Box;
