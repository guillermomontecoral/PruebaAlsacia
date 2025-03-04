import axios from "axios";
import { API_URL } from "../api/config";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { login as auth } from "../api/login";
import { useTranslation } from "react-i18next";

const Login = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [login, setLogin] = useState({
    email: "",
    password: "",
  });
  const [error, setError] = useState();

  const handleChange = (e) => {
    const { name, value } = e.target;
    setLogin((prevState) => ({
      ...prevState,
      [name]: value,
    }));
  };

  const handleLogin = async (e) => {
    e.preventDefault();
    console.log(login);

    try {
      // Llamada a la función auth (que es un alias de login)
      const result = await auth(login);

      // Si el login fue exitoso, navega a la siguiente página
      if (result) {
        navigate("/tasks");
      }
    } catch (error) {
      setError(error.message);
    }
  };

  return (
    <div className="flex flex-col h-full justify-center items-center px-6  lg:px-8">
      <div className="sm:mx-auto sm:w-full  text-center">
        <h1 className="mt-10 text-3xl font-bold tracking-tight text-gray-900 uppercase">
          {t("welcome")}
        </h1>
        <h2 className="mt-10 text-2xl font-bold tracking-tight text-gray-900 uppercase">
          {t("login")}
        </h2>
      </div>

      <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
        <form onSubmit={handleLogin} className="space-y-6">
          <div>
            <label
              htmlFor="email"
              className="block text-sm/6 font-medium text-gray-900"
            >
              {t("email")}
            </label>
            <div className="mt-2">
              <input
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2  sm:text-sm/6"
                id="email"
                name="email"
                type="email"
                required
                onChange={handleChange}
              />
            </div>
          </div>

          <div>
            <div className="flex items-center justify-between">
              <label
                htmlFor="password"
                className="block text-sm/6 font-medium text-gray-900"
              >
                {t("password")}
              </label>
            </div>
            <div className="mt-2">
              <input
                className="block w-full rounded-md bg-white px-3 py-1.5 text-base text-gray-900 outline-1 -outline-offset-1 outline-gray-300 placeholder:text-gray-400 focus:outline-2 focus:-outline-offset-2  sm:text-sm/6"
                id="password"
                name="password"
                type="password"
                required
                onChange={handleChange}
              />
            </div>
          </div>

          <button
            type="submit"
            className="flex w-full justify-center rounded-md  px-3 py-1.5 text-sm/6 font-semibold  shadow-xsfocus-visible:outline-2 focus-visible:outline-offset-2  bg-black text-white cursor-pointer"
          >
            {t("login")}
          </button>
        </form>
        {error && (
          <p className="mt-4 rounded-md bg-red-600 text-white px-4 py-2">
            {error}
          </p>
        )}
      </div>
    </div>
  );
};

export default Login;
