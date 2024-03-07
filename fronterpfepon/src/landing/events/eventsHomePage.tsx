import NavbarLogin from "../../components/Generic/NavbarLogin";

const EventsHomePage = () => {
  return (
    <div className="dark:bg-neutral-950 w-full h-screen">
      <NavbarLogin />
      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl p-8">
        <div className="">
          <h1 className="text-5xl">Eventos</h1>
        </div>
      </div>
    </div>
  );
};

export default EventsHomePage;
