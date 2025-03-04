#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
#WORKDIR /app
#COPY . ./
#RUN dotnet restore
#RUN dotnet publish -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0
#WORKDIR /app
#COPY --from=build-env /app/out .
#
## Copiar el script de espera
#COPY wait-for-it.sh /app/wait-for-it.sh
#RUN chmod +x /app/wait-for-it.sh
#
## Copiar el script de migraciones dentro del contenedor
#COPY init-db.sh /app/init-db.sh
#RUN chmod +x /app/init-db.sh
#
##ENTRYPOINT ["dotnet", "SistemaGestionTareas.dll"]
#
## Establecer el punto de entrada que ejecuta las migraciones antes de iniciar la API
#RUN apt-get update && apt-get install -y bash
#
#ENTRYPOINT ["bash", "-c", "/app/init-db.sh && dotnet SistemaGestionTareas.dll"]

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .

# Copiar el script de espera
COPY wait-for-it.sh /app/wait-for-it.sh
RUN chmod +x /app/wait-for-it.sh

# Copiar el script de migraciones dentro del contenedor
COPY init-db.sh /app/init-db.sh
RUN chmod +x /app/init-db.sh

#ENTRYPOINT ["dotnet", "SistemaGestionTareas.dll"]

# Establecer el punto de entrada que ejecuta las migraciones antes de iniciar la API
RUN apt-get update && apt-get install -y bash

ENTRYPOINT ["bash", "-c", "/app/init-db.sh && dotnet SistemaGestionTareas.dll"]