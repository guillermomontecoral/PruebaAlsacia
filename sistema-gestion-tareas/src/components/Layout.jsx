import React from "react";
import { useTranslation } from "react-i18next";
import "../i18n";
import { IoLogOutOutline } from "react-icons/io5";
import { useNavigate } from "react-router-dom";
import { getToken } from "../api/config";

const Layout = ({ children }) => {
  const { t, i18n } = useTranslation();
  const navigate = useNavigate();

  const changeLanguage = (lng) => {
    i18n.changeLanguage(lng);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    navigate("/");
  };

  return (
    <div className="h-screen flex flex-col">
      <header className="bg-gray-900 text-white py-4 px-6 flex justify-between items-center shadow-md">
        <h1 className="text-xl font-semibold">{t("welcome")}</h1>
        <div className="flex gap-x-4 items-center">
          <p>{t("change_language")}</p>
          <div className="relative inline-block group">
            <button
              className="flex flex-col items-center transition rounded-md cursor-pointer"
              onClick={() => changeLanguage("en")}
            >
              <img
                class="object-contain w-8"
                alt="United States flag"
                src="/public/images/icon-flags/united-states.png"
              />
            </button>
            <span className="absolute top-full mb-2 left-1/2 transform -translate-x-1/2 px-3 py-1 text-sm text-white bg-black rounded-md opacity-0 group-hover:opacity-100 transition-opacity">
              {t("english")}
            </span>
          </div>
          <div className="relative inline-block group">
            <button
              className="flex flex-col items-center transition rounded-md cursor-pointer"
              onClick={() => changeLanguage("es")}
            >
              <img
                class="object-contain w-8"
                alt="Spain flag"
                src="/public/images/icon-flags/spain.png"
              />
            </button>
            <span className="absolute top-full mb-2 left-1/2 transform -translate-x-1/2 px-3 py-1 text-sm text-white bg-black rounded-md opacity-0 group-hover:opacity-100 transition-opacity">
              {t("spanish")}
            </span>
          </div>
          {getToken() && (
            <div className="relative inline-block group">
              <button
                className="flex flex-col items-center transition rounded-md cursor-pointer"
                onClick={() => handleLogout()}
              >
                <IoLogOutOutline className="text-3xl" />
              </button>
              <span className="absolute top-full mb-2 left-1/2 transform -translate-x-1/2 px-3 py-1 text-sm text-white bg-black rounded-md opacity-0 group-hover:opacity-100 transition-opacity">
                {t("logout")}
              </span>
            </div>
          )}
        </div>
      </header>

      <main className="flex-1 bg-gray-100">{children}</main>

      <footer className="bg-gray-900 text-white text-center py-3 text-sm">
        {t("test")}
      </footer>
    </div>
  );
};

export default Layout;
