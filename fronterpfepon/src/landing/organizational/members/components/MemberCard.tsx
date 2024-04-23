import React, { useState } from "react";
import MyIcon from "../../../../components/Generic/MyIcon";

interface Member {
  id: number;
  name: string;
  position: string;
  description: string;
  photoUrl: string;
}

interface Props {
  member: Member;
  onDelete: (id: number) => void;
  onEdit: (id: number) => void;
}

const MemberCard: React.FC<Props> = ({ member, onDelete, onEdit }) => {
  const [menuOpen, setMenuOpen] = useState(false);

  const toggleMenu = () => {
    setMenuOpen(!menuOpen);
  };

  const handleDelete = () => {
    onDelete(member.id);
    setMenuOpen(false);
  };

  const handleEdit = () => {
    onEdit(member.id);
    setMenuOpen(false);
  };

  return (
    <div className="bg-gray-50 border border-gray-200 rounded-lg p-5 w-96 md:w-[360px] relative">
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
      <div className="flex flex-col items-center px-5 py-8 gap-4">
        <img
          src={member.photoUrl}
          alt={member.name}
          className="w-40 h-40 rounded-full shadow-md"
        />
        <div className="flex flex-col gap-4">
          <div className="text-center">
            <h2 className="text-lg font-semibold">{member.name}</h2>
            <h3 className="text-md">{member.position}</h3>
          </div>
          <div className="text-justify">
            <p className="text-gray-500">{member.description}</p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default MemberCard;
