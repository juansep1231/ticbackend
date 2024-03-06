import React, { useState } from "react";
import { Link } from "react-router-dom";
import { FiHelpCircle } from "react-icons/fi";

const NavbarLogin: React.FC = () => {
  return (
    <nav className="flex items-center justify-between bg-gray-200 text-gray-600 py-4 px-8 shadow-md ">
      <div>
        <ul className="flex">
          <li className="mr-8">
            <Link to="/organizational">Información Organizacional</Link>
          </li>
          <li className="mr-8">
            <Link to="/inventory">Inventario</Link>
          </li>
          <li className="mr-8">
            <Link to="/finantial">Finanzas</Link>
          </li>
          <li>
            <Link to="/events">Eventos</Link>
          </li>
        </ul>
      </div>
      <div>
        <Link to="/help">
          <FiHelpCircle className="size-6" />
        </Link>
      </div>
    </nav>
  );
};

export default NavbarLogin;
