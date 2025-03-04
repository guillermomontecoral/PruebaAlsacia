import axios from "axios";
import { API_URL, getToken } from "./config";

export const getTasks = async () => {
    try {
      const response = await axios.get(`${API_URL}task/get-all`,{
        headers:{            
          "Content-Type": "application/json",
          "Authorization": `Bearer ${getToken()}`
        }
      });

      if (response.status === 200) {

        return response.data;
        
      } else {
        console.error(`Unexpected response status: ${response.status}`);
        throw new Error(`Unexpected response status: ${response.status}`);
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        console.error("Axios error:", error);
        throw new Error(error.message);
      } else {
        console.error("Unexpected error: " + error);
        throw new Error("Unexpected error: " + error);
      }
    }
  };
