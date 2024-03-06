import React from "react";
import { Link } from "react-router-dom";
import MyIcon, { TypeFi } from "../../../components/Generic/MyIcon";

interface LinkCardProps {
  icon: TypeFi; // Usamos el tipo definido por TypeFi para el icono
  title: string;
  description: string;
  to: string;
}

const LinkCard: React.FC<LinkCardProps> = ({
  icon,
  title,
  description,
  to,
}) => {
  return (
    <Link
      to={to}
      className="flex items-center justify-between bg-white shadow-md p-6 rounded-md w-96 hover:bg-gray-50"
    >
      <div className="flex items-start gap-5">
        <div className="bg-blue-100 p-3 rounded-md">
          <MyIcon icon={icon} size={24} strokeWidth={2} />{" "}
        </div>
        <div className="flex flex-col gap-4">
          <h3 className="text-lg font-semibold py-2">{title}</h3>
          <p className="text-sm text-gray-600 text-justify">{description}</p>
        </div>
      </div>
    </Link>
  );
};

export default LinkCard;
