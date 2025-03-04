import axios from "axios";
import { API_URL } from "./config";

export const login = async (login) => {
  try {
    const response = await axios.post(`${API_URL}login`, login, {
      headers: {
        "Content-Type": "application/json",
      },
    });

    // Verifica si la respuesta es exitosa (200 o 201)
    if (response.status === 200 || response.status === 201) {
      localStorage.setItem("token", response.data);
      return true; 
    } else {
      throw new Error(`Unexpected response status: ${response.status}`);
    }
  } catch (error) {
    // Verifica si el error es un error de Axios
    if (axios.isAxiosError(error)) {

      // Devuelves un mensaje de error desde la api
      throw new Error(error.response?.data || "Error in the request.");
    } else {
      // Maneja errores que no son de Axios
      throw new Error("Unknown error when trying to log in.");
    }
  }
};
