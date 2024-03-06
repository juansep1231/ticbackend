"use client";

import { useState } from "react";
import PasswordForm from "./components/PasswordForm";

const ResetPasswordPage = () => {
  const [success, setSuccess] = useState(false);

  return (
    <div className="flex w-full h-screen justify-center dark:bg-neutral-950 bg-white pt-36">
      <div className="rounded-2xl bg-gray-100 px-6 py-10 dark:bg-dark_d h-fit">
        <PasswordForm onClick={() => setSuccess(true)} />
      </div>
    </div>
  );
};

export default ResetPasswordPage;
