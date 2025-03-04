# # Esperar a que el contenedor de la base de datos esté disponible
# /app/wait-for-it.sh api-db:5432 --timeout=30 --strict -- echo "Base de datos disponible!"

# # Ejecutar migraciones de Entity Framework Core
# echo "Ejecutando migraciones..."
# dotnet ef database update --project /app/SistemaGestionTareas/SistemaGestionTareas.csproj

# echo "Base de datos y datos de prueba cargados con éxito."

#!/bin/bash

# Esperar a que el contenedor de la base de datos esté disponible
/app/wait-for-it.sh api-db:5432 --timeout=30 --strict -- echo "Base de datos disponible!"

# Verificar si ya se ejecutaron las migraciones (usando un archivo de marca o similar)
if [ ! -f /app/.db_migrations_applied ]; then
    echo "Ejecutando migraciones..."
    dotnet ef database update --project /app/SistemaGestionTareas/SistemaGestionTareas.csproj

    # Marcar que las migraciones han sido aplicadas
    touch /app/.db_migrations_applied
else
    echo "Las migraciones ya fueron aplicadas previamente."
fi

echo "Base de datos y datos de prueba cargados con éxito."
