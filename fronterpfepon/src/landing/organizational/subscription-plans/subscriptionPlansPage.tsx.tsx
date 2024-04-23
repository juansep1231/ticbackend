import React from "react";
import SubscriptionCard from "./components/SubscriptionCard";

interface Plan {
  id: number;
  title: string;
  price: number;
  benefits: string[];
}

const SubscriptionPlanPage: React.FC = () => {
  // Arreglo de miembros
  const planes: Plan[] = [
    {
      id: 1,
      title: "Prueba",
      price: 10,
      benefits: [
        "Acceso a cursos gratuitos",
        "Colada morada en el dia de los difuntos",
        "Funda de caramelos en navidad",
      ],
    },
    {
      id: 2,
      title: "Gratis",
      price: 15,
      benefits: [
        "Acceso a cursos gratuitos",
        "Colada morada en el dia de los difuntos",
        "Funda de caramelos en navidad",
      ],
    },
    {
      id: 3,
      title: "Premium",
      price: 20,
      benefits: [
        "Acceso a cursos gratuitos",
        "Colada morada en el dia de los difuntos",
        "Funda de caramelos en navidad",
        "Acceso a cursos gratuitos",
        "Colada morada en el dia de los difuntos",
        "Funda de caramelos en navidad",
      ],
    },
  ];

  // Función para eliminar un miembro
  const handleDeletePlan = (id: number) => {
    // Lógica para eliminar el miembro del arreglo
    console.log(`Deleting member with ID: ${id}`);
  };

  // Función para editar un miembro
  const handleEditPlan = (id: number) => {
    // Lógica para editar el miembro
    console.log(`Editing member with ID: ${id}`);
  };

  return (
    <div className="flex flex-col gap-12">
      <div>
        <h2 className="text-4xl">Planes de Aportación</h2>
      </div>
      <div className="flex flex-wrap gap-5 justify-center">
        {/* Mapeo del arreglo de planes para mostrar las tarjetas */}
        {planes.map((plan) => (
          <div key={plan.id}>
            {" "}
            {/* Establecer el ancho fijo para cada tarjeta */}
            <SubscriptionCard
              plan={plan}
              onDelete={handleDeletePlan}
              onEdit={handleEditPlan}
            />
          </div>
        ))}
      </div>
    </div>
  );
};

export default SubscriptionPlanPage;
