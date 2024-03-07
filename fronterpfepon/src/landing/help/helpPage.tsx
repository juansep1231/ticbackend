import React from "react";
import Questions from "./components/Questions";
import questionsBank from "../../../public/data/dataQuestions.json";
import glosaryBank from "../../../public/data/dataGlosary.json";
import MyIcon from "../../components/Generic/MyIcon";
import { TypeFi } from "../../components/Generic/MyIcon";
import GlosaryCard from "./components/GlosaryCard";

interface IGlosaryCard {
  word: string;
  meaning: string;
  ico?: TypeFi;
}

const HelpPage = () => {
  const questionList = questionsBank.questionsBank.map((q, i) => {
    return (
      <Questions
        key={i}
        question={q.question}
        text={q.text}
        image={q.image}
        video={q.video}
      />
    );
  });

  const FeatIcons: TypeFi[] = [
    "FiMessageCircle",
    "FiX",
    "FiCheckSquare",
    "FiCalendar",
    "FiFile",
    "FiTag",
    "FiCreditCard",
    "FiMic",
    "FiSmile",
  ];

  const glosaryList = glosaryBank.glosaryBank.map((g, i) => {
    const iconIndex = i % FeatIcons.length;
    const icon = FeatIcons[iconIndex];

    return (
      <GlosaryCard key={i} word={g.word} meaning={g.meaning} icon={icon} />
    );
  });

  return (
    <div className="border py-16 w-full h-screen">
      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl pt-6 lg:px-0">
        <div>
          <h1 className="text-5xl">Ayuda</h1>
        </div>
        <div className="pb-8 flex flex-col gap-3">
          <h2 className="text-xl">
            Recuerda cambiar tu contraseña la primera vez que ingreses al
            sistema. Si tienes dudas de cómo hacerlo, aquí tienes un video
            tutorial del proceso.
          </h2>
        </div>
        <div className="max-w-6xl mx-auto">
          <video controls autoPlay muted src=""></video>
        </div>
        <div></div>
      </div>

      <div className="bg-gray-100 px-8 py-16">
        <div className="md:max-w-7xl mx-auto justify-center flex flex-col gap-8">
          <h2 className="text-h4 text-2xl">Glosario</h2>
          <div className="w-full flex justify-center">
            <div className="flex flex-col justify-center gap-5 md:max-w-6xl">
              {glosaryList}
            </div>
          </div>
        </div>
      </div>

      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl py-16 lg:px-0">
        <h2 className="text-h4 text-2xl">Preguntas frecuentes</h2>
        <div className="flex flex-col justify-center gap-5">{questionList}</div>
      </div>
    </div>
  );
};

export default HelpPage;
