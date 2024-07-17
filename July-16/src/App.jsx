import React, {useState} from "react";
import Header from "./Components/Header";
import GridBox from "./Components/GridBox";

const App = () => {

  return (
    <div
      style={{
        display: "flex",
        justifyContent: "center",
        flexDirection:"column",
        alignItems: "center",
        marginTop: "90px",
        gap:"40px",
    
      }}
    >

      <Header />
      <GridBox />
    </div>
  );
};

export default App;
