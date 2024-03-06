import MyLink from "../../components/Generic/MyLink";
import NavbarLogin from "../../components/Generic/NavbarLogin";
import LinkCard from "./components/LinkCards";

const HomePage = () => {
  return (
    <div className="w-full h-screen">
      <NavbarLogin />
      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl p-8 lg:px-0">
        <div className="">
          <h1 className="text-5xl">Bienvenido a FEPON</h1>
        </div>
        <div className="pb-8 flex flex-col gap-3">
          <h2 className="text-2xl">Conoce sobre nosotros</h2>
        </div>
        <div></div>
      </div>
      <div className="bg-gray-100 px-8 pt-8 pb-12">
        <div className="md:max-w-7xl mx-auto justify-center flex flex-col gap-8">
          <h2 className="text-h4 text-2xl">
            Conoce más acerca de nuestra gestión
          </h2>
          <div className="flex flex-col md:flex-row justify-center gap-5">
            <LinkCard
              to="/organizational"
              icon="FiUsers"
              title="Nuestra información"
              description="Conoce más sobre nosotros, nuestros plan de trabajo, nuestros miembros y lo todo lo que tenemos para ofrecer."
            />
            <LinkCard
              to="/finantial"
              icon="FiTrendingUp"
              title="Detalles financieros"
              description="Conoce más sobre nosotros, nuestros plan de trabajo, nuestros miembros y lo todo lo que tenemos para ofrecer."
            />
            <LinkCard
              to="/inventory"
              icon="FiFileText"
              title="Detalles de inventario"
              description="Conoce más sobre nosotros, nuestros plan de trabajo, nuestros miembros y lo todo lo que tenemos para ofrecer."
            />
            <LinkCard
              to="/events"
              icon="FiCalendar"
              title="Próximos eventos"
              description="Conoce más sobre nosotros, nuestros plan de trabajo, nuestros miembros y lo todo lo que tenemos para ofrecer."
            />
          </div>
        </div>
      </div>
    </div>
  );
};

export default HomePage;
