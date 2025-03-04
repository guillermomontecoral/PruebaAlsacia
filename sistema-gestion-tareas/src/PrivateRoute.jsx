import React from 'react';
import { getToken } from './api/config';
import { Navigate } from 'react-router-dom';

export function PrivateRoute({ element }) {
  const isAuthenticated = getToken();

  return isAuthenticated ? element : <Navigate to="/" />; // Si no está autenticado, rediriges al login
}