#!/bin/bash
echo "Esperando a que PostgreSQL esté disponible en api-db:5432..."

while !</dev/tcp/api-db/5432; do
  echo "PostgreSQL no está listo. Reintentando en 3 segundos..."
  sleep 3
done

echo "Base de datos lista. Ejecutando migraciones..."
dotnet ef database update --project /app/SistemaGestionTareas/SistemaGestionTareas.csproj

echo "Iniciando API..."
exec dotnet SistemaGestionTareas.dll