import React, { useState } from "react";
import "./App.css";

const wordsList = [  "DEIGN", "DEIGNED", "DEIGNING", "DELEING", "DELI", "DENIED", "DIDDLE",
  "DIDDLED", "DIDDLING", "DIED", "DIGGING", "DILL", "DINDLE", "DINDLED",
  "DINE", "DINED", "DING", "DINGED", "DINGING", "DINGLE", "DINING", "DINNLE",
  "DINNLED", "DWELLING", "DWINDLE", "DWINDLED", "DWINDLING", "EDDIED", "EDGING",
  "EGGING", "ELDING", "ELIDE", "ELIDED", "ELIDING", "ENDING", "ENGILD", "ENGILDED",
  "ENGILDING", "ENGINE", "GELDING", "GELID", "GELLING", "GENIE", "GENII", "GIGGED",
  "GIGGING", "GIGGLE", "GIGGLED", "GIGGLING", "GILD", "GILDED", "GILDEN", "GILDING",
  "GILL", "GILLED", "GILLING", "GINNED", "GINNING", "GLEDGING", "GLIDE", "GLIDED",
  "GLIDING", "IDLE", "IDLED", "IDLING", "INDEED", "INDIE", "INDIGENE", "INNIE",
  "INNING", "LEGGING", "LEGGINGED", "LENDING", "LIDDED", "LIED", "LIEGE", "LIEN",
  "LINDEN", "LINE", "LINED", "LINEN", "LINING", "LINNED", "NEEDING", "NEEDLING",
  "NEGLIGEE", "NIGGLE", "NIGGLED", "NIGGLING", "NINE", "WEDDING", "WEDGIE", "WEDGING",
  "WEEDING", "WEENIE", "WELDING", "WELLING", "WENDING", "WIDE", "WIDEN", "WIDENED",
  "WIDENING", "WIELD", "WIELDED", "WIELDING", "WIENIE", "WIGGED", "WIGGING", "WIGGLE",
  "WIGGLED", "WIGGLING", "WILD", "WILDLING", "WILE", "WILL", "WILLED", "WILLING",
  "WIND", "WINDED", "WINDING", "WINE", "WINED", "WING", "WINGDING", "WINGED",
  "WINGING", "WINING", "WINNING"
];

const App = () => {
  const [word, setWord] = useState(["D", "E", "G", "L", "I", "N", "W"]);
  const [letters, setLetters] = useState("");
  const [foundWords, setFoundWords] = useState([]);

  const handleClick = (letter) => {
    setLetters((prevLetters) => prevLetters + letter);
  };

  const clearLastLetter = () => {
    setLetters((prevLetters) => prevLetters.slice(0, -1));
  };

  const shuffleWords = () => {
    const centralLetter = word[4];
    const otherLetters = [...word.slice(0, 4), ...word.slice(5)];
    for (let i = otherLetters.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [otherLetters[i], otherLetters[j]] = [otherLetters[j], otherLetters[i]];
    }
    setWord([...otherLetters.slice(0, 4), centralLetter, ...otherLetters.slice(4)]);
  };

  const checkWord = () => {
    if (letters.length <= 0) return;

    if (letters.length < 4) {
      alert("Word must have more than 4 letters");
      return;
    }

    if (!letters.includes("I")) {
      alert("Must contain character 'I'");
      return;
    }

    const foundWord = wordsList.find((w) => w === letters.toUpperCase());

    if (foundWord) {
      if (foundWords.includes(foundWord)) {
        alert("Word already found");
      } else {
        setFoundWords((prevFoundWords) => [...prevFoundWords, foundWord]);
      }
      setLetters("");
    } else {
      alert("No word found");
      setLetters("");
    }
  };

  const renderHexagon = (letter, index) => {
    const isCentral = index === 4;
    return (
      <div
        onClick={() => handleClick(letter)}
        className={`hexagon ${isCentral ? "central" : ""}`}
        key={index}
      >
        {letter}
      </div>
    );
  };

  return (
    <div>
      <h3 style={{ marginTop: "30px", marginBottom: "60px" }}>Spell BE</h3>
      <div className="letters-display">
        {letters.split("").map((letter, index) => (
          <span key={index} style={{ color: letter === "I" ? "red" : "black" }}>
            {letter}
          </span>
        ))}
      </div>
      <div className="hexagon-grid">
        {word.map((w, index) => renderHexagon(w, index))}
      </div>
      <div className="buttons">
        <button onClick={clearLastLetter}>❌</button>
        <button onClick={shuffleWords}>🔀</button>
        <button onClick={checkWord}>✅</button>
      </div>
      <div>
        <ul>
          {foundWords.map((word, index) => (
            <li key={index}>
              <span>👉</span> {word}
            </li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default App;
