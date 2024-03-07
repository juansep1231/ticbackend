import React from "react";
import MyIcon, { TypeFi } from "../../../components/Generic/MyIcon";

interface Props {
  icon: TypeFi;
  word: string;
  meaning: string;
}

const GlosaryCard: React.FC<Props> = ({ icon, word, meaning }) => {
  return (
    <div className="flex items-start gap-5">
      <div className="bg-blue-100 p-2 rounded-md">
        <MyIcon icon={icon} size={24} strokeWidth={2} />{" "}
      </div>
      <div className="flex flex-col">
        <h3 className="text-lg font-semibold">{word}</h3>
        <p className="text-sm text-gray-600 text-justify">{meaning}</p>
      </div>
    </div>
  );
};

export default GlosaryCard;
