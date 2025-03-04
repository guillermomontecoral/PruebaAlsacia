import axios from "axios";
import { API_URL, getToken } from "./config";
import { t } from "i18next";

export const changeStatus = async (id, status) => {
  let value;
  switch (status) {
    case "Created":
      value = 1;
      break;
    case "InProgress":
      value = 2;
      break;
  }

  try {
    const response = await axios.put(
      `${API_URL}task/change-status/${id}`, value, 
      {
        headers: {
          "Content-Type": "application/json",
          "Authorization": `Bearer ${getToken()}`,
        },
      }
    );

    // Verificamos si la respuesta fue exitosa
    if (response.status === 204) {
      return t("status_change");
    } else {
        throw new Error(`Unexpected response status: ${response.status}`)
    }
  } catch (error) {
    // Manejo de errores de Axios
    if (axios.isAxiosError(error)) {
      throw new Error(error.response.data);
    } else {
      // Manejo de otros errores inesperados
      throw new Error("An unexpected error occurred.");
    }
  }
};