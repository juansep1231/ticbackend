interface Props {
  question: string;
  text?: string;
  image?: string;
  video?: string;
}

const Questions = ({ question, text, image, video }: Props) => {
  return (
    <div className="w-full flex flex-col gap-8">
      <div className="w-full flex flex-col gap-4 items-justify text-justify">
        {question && <p className="text-lg">{question}</p>}
        {text && <p className="text-gray-500">{text}</p>}
        {image && <img src={image} alt="" className="w-full" />}
        {video && (
          <div className="max-w-6xl mx-auto">
            <video controls autoPlay muted src={video}></video>
          </div>
        )}
      </div>
    </div>
  );
};

export default Questions;
