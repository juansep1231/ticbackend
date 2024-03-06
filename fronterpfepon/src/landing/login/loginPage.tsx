import React from "react";
import LoginForm from "./components/LoginForm";

const LoginPage: React.FC = () => {
  const handleLogin = (formData: { username: string; password: string }) => {
    console.log("Formulario de login enviado:", formData);
  };

  return (
    <div>
      <div className="flex justify-center h-screen pt-14 bg-gray-50">
        <LoginForm onSubmit={handleLogin} />
        <div className="flex flex-col items-center justify-center bg-[#0f70b7] rounded-tr-2xl rounded-br-2xl w-96 h-4/5 gap-5">
          <div>
            <h1 className="text-white text-3xl">Bienvenido a FEPON </h1>
          </div>
          <div className="text-center">
            <p className="text-white text-base px-5">
              Para acceder a la información que necesitas inicia sesión
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
