import MyInput from "../../../components/Generic/MyInput";
import { useState } from "react";
import MyLink from "../../../components/Generic/MyLink";
import PasswordForm from "../../reset-password/components/PasswordForm";

interface Props {
  onSubmit: (formData: { username: string; password: string }) => void;
}

const LoginForm: React.FC<Props> = ({ onSubmit }) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    onSubmit({ username, password });
  };

  return (
    <div className="flex flex-col items-center justify-center border bg-white rounded-tl-2xl rounded-bl-2xl w-96 h-4/5 gap-8">
      <div>
        <h1 className="text-2xl">Inicio de sesión</h1>
      </div>
      <form onSubmit={handleSubmit} className="flex flex-col gap-5">
        <div className="flex flex-col gap-5">
          <MyInput
            label="Correo institucional"
            name="username"
            type="text"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            placeholder="Ingrese su correo institucional"
            icon="FiAtSign"
          />
          <MyInput
            label="Contraseña"
            name="password"
            type="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            placeholder="Ingrese su usuario"
            icon="FiLock"
          />
        </div>
        <div className="text-right">
          <MyLink
            href={`/reset-password`}
            className="text-small m-auto text-blue-600"
          >
            ¿Olvidaste tu contraseña?
          </MyLink>
        </div>
        <div className="flex justify-center">
          <button
            type="submit"
            className="btn btn-primary p-3 rounded-xl bg-[#0f70b7] w-2/5 text-white"
          >
            Iniciar sesión
          </button>
        </div>
      </form>
    </div>
  );
};

export default LoginForm;
