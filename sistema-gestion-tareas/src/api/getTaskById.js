import axios from "axios";
import { API_URL, getToken } from "./config";
import { t } from "i18next";

export const getTaskById = async (id) => {
  try {
    const response = await axios.get(`${API_URL}task/get-by-id/${id}`, {
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${getToken()}`,
      },
    });

    if (response.status === 200) {
      return response.data;
    } else {
      throw new Error(t("error_response"));
    }
  } catch (error) {
    console.log(error)
    if (axios.isAxiosError(error)) {
        throw new Error(error.response.data);
      } else {
        throw new Error("Unexpected error: " + error);
      }
  }
};
