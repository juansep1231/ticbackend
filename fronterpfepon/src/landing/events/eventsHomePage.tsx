import NavbarLogin from "../../components/Generic/NavbarLogin";
import EventCard from "./components/card";
import SearchBar from "./components/searchBar";
import MyButton from "../../components/Generic/MyButton";

const EventsHomePage = () => {
  return (
    <div className="bg-white w-full h-screen">
      <NavbarLogin />
      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl p-8">
        <div className="">
          <h1 className="text-5xl text-center pb-4">Eventos</h1>
          <SearchBar></SearchBar>
          <EventCard></EventCard>
          <EventCard></EventCard>
          <EventCard></EventCard>
          <EventCard></EventCard>
        </div>
      </div>
    </div>
  );
};

export default EventsHomePage;
