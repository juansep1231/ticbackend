import NavbarLogin from "../../components/Generic/NavbarLogin";
import DownloadPDFButton from "./components/DownloadPDFButton";
import InventoryTable from "./components/InventoryTable";
const InventoryHomePage = () => {
  return (
    <div className="w-full h-screen">
      <NavbarLogin />
      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl p-8">
        <div className="">
          <h1 className="text-5xl">Inventario</h1>
        </div>
        <div className="flex justify-end">
          <DownloadPDFButton />
        </div>
        <div className="flex justify-center">
          <InventoryTable />
        </div>
      </div>
    </div>
  );
};

export default InventoryHomePage;
