import React, { useState, useEffect } from "react";
import Box from "./Box";

const GridBox = () => {
  const rows = 6;
  const cols = 5;

  let currentRow = 0;
  let currentCol = 0;

  const [gridItems, setGridItems] = useState(
    Array.from({ length: rows }, () =>
      Array.from({ length: cols }, () => ({ letter: " ", color: "white" }))
    )
  );

  const words = "CHAIR";
  let Isfnished = false;
  const clickHandler = (ri, ci) => {
    console.log(gridItems[ri][ci]);
  };

  const checkWord = () => {
    const currentWord = gridItems[currentRow]
      .map((item) => item.letter)
      .join("");
    if (words === currentWord) {
      setGridItems((prev) => {
        const temp = [...prev];
        for (let i = 0; i < cols; i++) {
          temp[currentRow][i].color = "green";
        }
        currentRow++;
        currentCol = 0;
        return temp;
      });

      return true;
    } else {
      setGridItems((prev) => {
        const temp = [...prev];
        for (let i = 0; i < cols; i++) {
          if (currentWord[i] === words[i]) {
            temp[currentRow][i].color = "green";
          } else if (words.includes(currentWord[i])) {
            temp[currentRow][i].color = "yellow";
          } else {
            temp[currentRow][i].color = "grey";
          }
        }
        currentRow++;
        currentCol = 0;
        return temp;
      });

      return false;
    }
  };

  useEffect(() => {
    const handleKeyPress = (e) => {
      if (!Isfnished) {
        switch (e.keyCode) {
          case 13:
            if (currentCol < cols) {
              alert("Enter all letters");
              return;
            }
            if (checkWord()) {
              setTimeout(() => {
                alert("Yep You Got it 😊");
              }, 500);
              Isfnished = true;
            }
            if (rows - 1 == currentRow) {
              Isfnished = true;
              alert("May be Next time 😒😒");
            }
            break;

          case 8:
            setGridItems((prev) => {
              currentCol > 0 ? currentCol-- : currentCol;
              const temp = [...prev];
              temp[currentRow][currentCol] = { letter: "", color: "white" };
              return temp;
            });
            break;

          default:
            if (e.keyCode >= 65 && e.keyCode <= 90) {
              if (currentCol > cols - 1) {
                return;
              }
              setGridItems((prev) => {
                const temp = [...prev];
                temp[currentRow][currentCol] = {
                  letter: e.key.toUpperCase(),
                  color: "white",
                };
                currentCol++;
                return temp;
              });
            }
            break;
        }
      }
    };

    window.addEventListener("keydown", handleKeyPress);

    return () => {
      window.removeEventListener("keydown", handleKeyPress);
    };
  }, [currentCol, currentRow, cols]);

  return (
    <div
      style={{
        display: "grid",
        gridTemplateColumns: `repeat(${cols}, 1fr)`,
        gap: "10px",
      }}
    >
      {gridItems.map((row, rowIndex) =>
        row.map((item, colIndex) => (
          <Box
            key={`${rowIndex}-${colIndex}`}
            char={item.letter}
            color={item.color}
            clickListener={() => clickHandler(rowIndex, colIndex)}
          />
        ))
      )}
    </div>
  );
};

export default GridBox;
