"use client";
import { useState } from "react";
import MyInput from "../../../components/Generic/MyInput";
import MyButton from "../../../components/Generic/MyButton";

interface Props {
  onClick: () => void;
}

const PasswordForm = ({ onClick }: Props) => {
  const [username, setUsername] = useState("");

  return (
    <form
      className="flex flex-col gap-5 w-80 justify-center mx-4"
      onSubmit={(e) => e.preventDefault()}
    >
      <div className="text-center">
        <p className="text-2xl text-black dark:text-white">
          Reestablecer contaseña
        </p>
      </div>
      <div className="flex justify-center">
        <MyInput
          label="Correo institucional"
          name="username"
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          placeholder="Ingrese su correo institucional"
          icon="FiAtSign"
        />
      </div>
      <div className="flex justify-center">
        <button
          type="submit"
          className="btn btn-primary p-3 rounded-xl bg-[#0f70b7] w-2/5 text-white"
        >
          Continuar
        </button>
      </div>
    </form>
  );
};

export default PasswordForm;
