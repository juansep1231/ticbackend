import React, { useState } from "react";
import MyIcon from "../../../../components/Generic/MyIcon";

interface Plan {
  id: number;
  title: string;
  price: number;
  benefits: string[];
}

interface Props {
  plan: Plan;
  onDelete: (id: number) => void;
  onEdit: (id: number) => void;
}

const SubscriptionCard: React.FC<Props> = ({ plan, onDelete, onEdit }) => {
  const [menuOpen, setMenuOpen] = useState(false);

  const toggleMenu = () => {
    setMenuOpen(!menuOpen);
  };

  const handleDelete = () => {
    onDelete(plan.id);
    setMenuOpen(false);
  };

  const handleEdit = () => {
    onEdit(plan.id);
    setMenuOpen(false);
  };

  return (
    <div className="bg-gray-50 border border-gray-200 rounded-lg p-5 w-96 h-full md:w-[360px] relative">
      <button
        className="absolute top-5 right-5 text-gray-600 hover:text-gray-800 focus:outline-none"
        onClick={toggleMenu}
      >
        <MyIcon icon="FiMoreVertical" />
      </button>
      {menuOpen && (
        <ul className="absolute top-10 right-6 bg-white shadow-md rounded-md py-2">
          <li className="hover:bg-gray-100 px-4 py-2" onClick={handleEdit}>
            Editar
          </li>
          <li className="hover:bg-gray-100 px-4 py-2" onClick={handleDelete}>
            Borrar
          </li>
        </ul>
      )}
      <div className="flex flex-col items-center px-5 py-8 gap-8">
        <div className="text-center">
          <h2 className="text-3xl font-semibold">{plan.title}</h2>
        </div>
        <div className="">
          <p className="text-5xl">${plan.price}</p>
        </div>
        <div>
          <h2 className="font-medium mb-4 text-lg text-center">Incluye:</h2>
          {plan.benefits.map((benefit, index) => (
            <li key={index} className="flex justify-start my-2">
              <div className="w-full flex">
                <div className="flex items-center justify-center text-green-600 mr-3">
                  <MyIcon icon="FiCheck" size={16} strokeWidth={2} />{" "}
                </div>
                <span className="text-start">{benefit}</span>
              </div>
            </li>
          ))}
        </div>
      </div>
    </div>
  );
};

export default SubscriptionCard;
