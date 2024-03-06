import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { useState } from "react";
import Navbar from "./components/Generic/Navbar.tsx";
import LoginPage from "./landing/login/loginPage.tsx";
import HomePage from "./landing/home/homePage.tsx";

function App() {
  const [darkMode, setDarkMode] = useState(false);
  const [language, setLanguage] = useState("en");

  const toggleDarkMode = () => {
    setDarkMode(!darkMode);
  };

  const toggleLanguage = () => {
    // Lógica para cambiar el idioma
  };
  return (
    <Router>
      <div className="h-screen">
        <Navbar
          toggleDarkMode={toggleDarkMode}
          toggleLanguage={toggleLanguage}
        />
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route path="/home" element={<HomePage />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
