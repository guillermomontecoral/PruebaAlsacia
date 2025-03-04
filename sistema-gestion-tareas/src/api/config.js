import { jwtDecode } from "jwt-decode";

export const API_URL = "http://localhost:5133/api/"

export const getToken = () => {
    const token = localStorage.getItem("token");

    // Verificar si el token ha expirado
    if (isTokenExpired(token)) {
    
      return null; // No proceder si el token ha expirado
    } else {
      return token;
     }
  };

  const isTokenExpired = (token) => {
    try {
      if(!token){
        return true;
      }
      const decodedToken = jwtDecode(token);
  
      if (!decodedToken.exp) {
        return true; // Considerar el token expirado si no tiene campo exp
      }
  
      const currentTime = Date.now() / 1000; // Tiempo actual en segundos
      return decodedToken.exp < currentTime; // Token expirado si exp < tiempo actual
    } catch (error) {
      console.error("Error decodificando el token:", error);
      return true; // Considerar el token expirado si hay un error al decodificar
    }
  };

  export const getIdUser = () => {
    const token = localStorage.getItem("token");
    if (!token) {
      console.log("Token no disponible");
      return null;
    }
  
    try {
      const id = jwtDecode(token);
      return id.sub; // El ID del usuario
    } catch (error) {
      console.error("Error decodificando el token:", error);
      return null;
    }
  };