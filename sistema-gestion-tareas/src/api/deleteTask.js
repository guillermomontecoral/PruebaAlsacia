import axios from "axios";
import { API_URL, getToken } from "./config";

export const deleteTask = async (id) => {
  try {
    const response = await axios.delete(`${API_URL}task/delete/${id}`, {
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${getToken()}`,
      },
    });

    // Verifica si la respuesta es exitosa (status 200)
    if (response.status === 200) {
      return true
    } else {
      const errorMessage = `Unexpected response status: ${response.status}`;
      throw new Error(errorMessage);
    }
  } catch (error) {

    // Maneja errores específicos de Axios
    if (axios.isAxiosError(error)) {
      const errorMessage = error.response?.data || error.message;
      throw new Error(`Error deleting task: ${errorMessage}`);
    } else {
      // Maneja otros errores inesperados (no relacionados con Axios)
      throw new Error("An unexpected error occurred while deleting the task.");
    }
  }
};
