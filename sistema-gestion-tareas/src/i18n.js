import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import Backend from "i18next-http-backend";
import LanguageDetector from "i18next-browser-languagedetector";

i18n
  .use(Backend) // Carga traducciones desde archivos
  .use(LanguageDetector) // Detecta el idioma del navegador
  .use(initReactI18next) // Inicializa react-i18next
  .init({
    fallbackLng: "en", // Idioma por defecto si no se encuentra el actual
    debug: true, // Muestra logs en consola (puedes desactivarlo en producción)
    interpolation: {
      escapeValue: false, // React ya maneja la seguridad contra XSS
    },
    backend: {
      loadPath: "/locales/{{lng}}.json", // Ruta donde están los archivos JSON
    },
  });

export default i18n;
