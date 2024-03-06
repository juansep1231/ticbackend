import React, { useState } from "react";
import { Link } from "react-router-dom";
import { FiSettings } from "react-icons/fi";

interface NavbarProps {
  toggleDarkMode: () => void;
  toggleLanguage: () => void;
}

const Navbar: React.FC<NavbarProps> = ({ toggleDarkMode, toggleLanguage }) => {
  const [isSettingsOpen, setIsSettingsOpen] = useState(false);

  const handleSettingsToggle = () => {
    setIsSettingsOpen(!isSettingsOpen);
  };

  return (
    <nav className="flex items-center justify-between bg-gray-200 text-gray-600 py-2 px-8 shadow-md relative z-10">
      <div className="flex items-center">
        <Link to="/home" className="text-xl font-bold">
          <img src="/img/fepon.png" alt="LogoFepon" className="h-10" />
        </Link>
      </div>
      <div>
        <ul className="flex gap-6 ml-16">
          <li>
            <Link to="/home">Home</Link>
          </li>
          <li>
            <Link to="/help">Help</Link>
          </li>
        </ul>
      </div>
      <div className="flex items-center">
        <Link
          to="/login"
          className="pb-2 pt-1 px-4 rounded-md border border-blue-600 text-blue-600 hover:bg-blue-200"
        >
          Ingresar
        </Link>
        <button
          className="text-xl focus:outline-none ml-8"
          onClick={handleSettingsToggle}
        >
          <FiSettings className="size-6" />
        </button>
        {isSettingsOpen && (
          <div className="absolute top-12 right-0 bg-white p-2 rounded shadow">
            <button
              onClick={toggleDarkMode}
              className="block w-full text-left py-2 px-4 hover:bg-gray-100"
            >
              Toggle Dark Mode
            </button>
            <button
              onClick={toggleLanguage}
              className="block w-full text-left py-2 px-4 hover:bg-gray-100"
            >
              Change Language
            </button>
          </div>
        )}
      </div>
    </nav>
  );
};

export default Navbar;
