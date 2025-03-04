import React from 'react';

import { getToken } from './api/config';
import { Navigate } from 'react-router-dom';

// Componente para las rutas protegidas
export function PrivateRoute({ element }) {
  const isAuthenticated = getToken();

  return isAuthenticated ? element : <Navigate to="/" />; // Si no está autenticado, rediriges al login
}