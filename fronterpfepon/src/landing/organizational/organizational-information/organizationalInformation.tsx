import React from "react";

interface Asociation {
  id: number;
  name: string;
  description: string;
  mision: string;
  vision: string;
  objective: string;
}

const OrganizationalInformationPage: React.FC = () => {
  // Datos de asociacion
  const associations: Asociation[] = [
    {
      id: 1,
      name: "FEPON",
      description:
        "FEPON es una asociación dedicada a promover la conservación y protección del medio ambiente en nuestra comunidad. Nuestro objetivo es crear conciencia sobre los problemas ambientales y trabajar en conjunto con individuos y organizaciones para implementar soluciones sostenibles.",
      mision:
        "Nuestra misión es liderar iniciativas que promuevan la conservación del medio ambiente, educar a la población sobre la importancia de la protección ambiental y colaborar con diferentes sectores de la sociedad para alcanzar un equilibrio entre el desarrollo humano y la preservación de los recursos naturales.",
      vision:
        "Nuestra visión es un mundo en el que todas las personas comprendan la interdependencia entre la humanidad y el medio ambiente, y trabajen juntas para proteger y preservar los ecosistemas para las generaciones futuras.",
      objective:
        "Nuestros objetivos incluyen la realización de campañas de sensibilización ambiental, la promoción de prácticas de consumo sostenible, la participación en proyectos de conservación de la biodiversidad y la colaboración con entidades gubernamentales y empresas para implementar políticas y acciones ambientales responsables.",
    },
  ];

  return (
    <div className="flex pt-4 flex-col">
      <div className="text-justify">
        {associations.map((asociation) => (
          <div key={asociation.id} className="flex flex-col gap-8">
            <div>
              <h2 className="text-4xl">{asociation.name}</h2>
            </div>
            <div className="flex flex-col gap-3">
              <p className="text-gray-500">{asociation.description}</p>
            </div>
            <div className="flex flex-col gap-3">
              <h3 className="text-lg font-semibold">Misión</h3>
              <p className="text-gray-500">{asociation.mision}</p>
            </div>
            <div className="flex flex-col gap-3">
              <h3 className="text-lg font-semibold">Visión</h3>
              <p className="text-gray-500">{asociation.vision}</p>
            </div>
            <div className="flex flex-col gap-3">
              <h3 className="text-lg font-semibold">Objetivo</h3>
              <p className="text-gray-500">{asociation.objective}</p>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};

export default OrganizationalInformationPage;
