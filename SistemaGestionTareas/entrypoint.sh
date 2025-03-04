#!/bin/bash
set -e

echo "Esperando a que la base de datos esté lista..."

# Extraer valores de la conexión
host="api-db"
port="5432"

# Esperar hasta que PostgreSQL esté listo
until pg_isready -h $host -p $port; do
  echo "Esperando a PostgreSQL en $host:$port..."
  sleep 2
done

echo "Base de datos lista. Ejecutando migraciones..."
dotnet ef database update --project /app/SistemaGestionTareas/DataAccess/DataAccess.csproj

echo "Iniciando la aplicación..."
exec dotnet SistemaGestionTareas.dll
