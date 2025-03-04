import { t } from "i18next";

export const validate = (name, value) => {
    let errorMessage;
  
    if (name === "title" || name === "description") {
      if (value.trim() === '') {
        errorMessage = name === "title" ? t("title_empty_error") : t("description_empty_error");
      } else if (value.trim().length < 2) {
        errorMessage =
          name === "title"
            ? t("title_contain_error")
            : t("description_contain_error");
      }
    }
    
    if (name === "expirationDate") {
      const expirationDate = value;
      
      // Obtener la fecha de hoy y restar un día
      const today = new Date();
      today.setDate(today.getDate() - 1);  // Restamos un día a la fecha de hoy
    
      // Convertir ambas fechas al formato YYYY-MM-DD para compararlas
      const formattedExpirationDate = new Date(expirationDate).toISOString().split("T")[0];
      const formattedToday = today.toISOString().split("T")[0];  // Fecha de hoy - 1 día
    
      console.log(formattedExpirationDate);
      console.log(formattedToday);
    
      if (formattedExpirationDate < formattedToday) {
        errorMessage = t("expiration_date_error");
      } else {
        errorMessage = '';
      }
    }
    return errorMessage;
  };