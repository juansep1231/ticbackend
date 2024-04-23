import { useState } from "react";
import NavbarLogin from "../../components/Generic/NavbarLogin";
import MembersPage from "./members/membersPage";
import OrganizationalInformationPage from "./organizational-information/organizationalInformation";
import SubscriptionPlanPage from "./subscription-plans/subscriptionPlansPage.tsx";

const OrganizationalHomePage = () => {
  return (
    <div className="w-full h-screen">
      <NavbarLogin />
      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl pt-8 lg:px-0">
        <div className="">
          <h1 className="text-5xl">Información Organizacional</h1>
        </div>
        <div className="pb-8 flex flex-col gap-3">
          <OrganizationalInformationPage />
        </div>
        <div></div>
      </div>

      <div className="bg-gray-100 px-8 py-12">
        <div className="md:max-w-7xl mx-auto justify-center flex flex-col gap-8">
          <MembersPage />
        </div>
      </div>

      <div className="flex flex-col mx-auto justify-center gap-10 md:max-w-7xl py-12 lg:px-0">
        <SubscriptionPlanPage />
      </div>
    </div>
  );
};

export default OrganizationalHomePage;
