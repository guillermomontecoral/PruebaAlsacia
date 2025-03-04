# FROM node:alpine
# WORKDIR /app
# COPY package*.json .
# RUN npm install
# COPY . .

# # Expone el puerto de Vite
# EXPOSE 5173
# # Comando para iniciar el servidor en desarrollo
# CMD ["npm", "run", "dev", "--", "--host"]

FROM node:alpine

# Establece el directorio de trabajo dentro del contenedor
WORKDIR /app

# Copia los archivos package.json y package-lock.json primero
COPY package*.json ./

# Instala las dependencias
RUN npm install

# Copia el resto del código fuente de la aplicación
COPY . .

# Exponer el puerto en el que Vite se ejecuta (por defecto 5173)
EXPOSE 5173

# Inicia el servidor de desarrollo de Vite
CMD ["npm", "run", "dev", "--", "--host"]
