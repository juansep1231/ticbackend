import React from "react";
import MemberCard from "./components/MemberCard";

interface Member {
  id: number;
  name: string;
  position: string;
  description: string;
  photoUrl: string;
}

const MembersPage: React.FC = () => {
  // Arreglo de miembros
  const members: Member[] = [
    {
      id: 1,
      name: "John Doe",
      position: "Frontend Developer",
      description:
        "John Doe es un desarrollador web altamente motivado con experiencia en desarrollo frontend y backend. Con una sólida formación en tecnologías como JavaScript, React.js y Node.js.",
      photoUrl: "/img/miembro.png",
    },
    {
      id: 2,
      name: "Jane Smith",
      position: "Backend Developer",
      description:
        "John Doe es un desarrollador web altamente motivado con experiencia en desarrollo frontend y backend. Con una sólida formación en tecnologías como JavaScript, React.js y Node.js.",
      photoUrl: "/img/miembro.png",
    },
    {
      id: 2,
      name: "Jane Smith",
      position: "Backend Developer",
      description:
        "John Doe es un desarrollador web altamente motivado con experiencia en desarrollo frontend y backend. Con una sólida formación en tecnologías como JavaScript, React.js y Node.js.",
      photoUrl: "/img/miembro.png",
    },
  ];

  // Función para eliminar un miembro
  const handleDeleteMember = (id: number) => {
    // Lógica para eliminar el miembro del arreglo
    console.log(`Deleting member with ID: ${id}`);
  };

  // Función para editar un miembro
  const handleEditMember = (id: number) => {
    // Lógica para editar el miembro
    console.log(`Editing member with ID: ${id}`);
  };

  return (
    <div className="flex pt-4 flex-col gap-12">
      <div>
        <h2 className="text-4xl">Miembros Administrativos</h2>
      </div>
      <div className="flex flex-wrap gap-5 justify-center">
        {/* Mapeo del arreglo de miembros para mostrar las MemberCard */}
        {members.map((member) => (
          <div key={member.id}>
            {" "}
            {/* Establecer el ancho fijo para cada tarjeta */}
            <MemberCard
              member={member}
              onDelete={handleDeleteMember}
              onEdit={handleEditMember}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default MembersPage;
