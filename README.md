# Documentación de la Prueba

## 1. Backend

### Tecnologías Utilizadas:
- **IDE**: Visual Studio Code 2022
- **Framework**: C# .Net Core 8
- **Paquetes NuGet**:
  - **Entity Framework Core, EntityFrameworkCore.Tools, EntityFrameworkCore.Design**: Para interactuar con la base de datos PostgreSQL.
  - **JWTBearer**: Para la autenticación de usuarios mediante JWT.
  - **Npgsql.EntityFrameworkCore.PostgreSQL**: Para la integración de PostgreSQL con Entity Framework Core.
- **Formato de Datos**: Envió de datos y respuestas en formato JSON.
- **Consultas**: LINQ para la manipulación de datos.

### Arquitectura:
Está estructurado en varias capas que separan la lógica de negocio, la comunicación con la base de datos y las funcionalidades de la API:
- **Domain**: Capa de negocio, donde residen las reglas y lógica del dominio de la aplicación.
- **Aplicación**: Capa intermedia que actúa como puente entre el controlador y la capa de datos **DataAccess**. Esta capa gestiona los casos de uso y orquesta las interacciones.
- **DataAccess**: Capa encargada de las operaciones CRUD y la interacción directa con la base de datos utilizando **Entity Framework Core**.
- **API**: Capa que expone los métodos para interactuar con el sistema, permitiendo acceso a los datos a través de endpoints HTTP.

## 2. Frontend (React + Vite)

### Tecnologías Utilizadas:
- **IDE**: Visual Studio Code
- **Framework**: React + Vite
- **CSS Framework**: TailwindCSS 4
- **Bibliotecas**:
  - **Axios**: Para realizar peticiones HTTP.
  - **React Icons**: Para iconos SVG.
  - **React Router DOM**: Para la gestión de rutas en el frontend.
  - **JWTDecode**: Para decodificar y leer el contenido de los tokens JWT.
  - **i18n**: Permite que la aplicación esté disponible tanto en **inglés** como en **español**.

### Desarrollo del Frontend:
Fue desarrollado utilizando **React** con **Vite**, lo que permite una carga rápida y eficiente. El diseño fue implementado con **TailwindCSS**, un framework que permite crear interfaces rápidas. La navegación entre diferentes vistas se gestiona con **React Router DOM**, lo que proporciona una navegación fluida y controlada dentro de la aplicación.

## 3. Base de Datos

- **PostgreSQL**: Sistema de gestión de bases de datos utilizado para almacenar la información.
- **Npgsql.EntityFrameworkCore.PostgreSQL**: Proveedor de **Entity Framework Core** que permite la integración de PostgreSQL en la api en .NET.

## 4. Docker

### Backend:
1. **Dockerfile**: Se encuentra containerizado utilizando un **Dockerfile** el cual define el entorno de ejecución para la API.
2. **Script `wait-for-it.sh`**: Utilizado para garantizar que los servicios dependientes (como la base de datos) estén completamente disponibles antes de que la api intente ejecutarse.
3. **Script `init-db.sh`**: Este script utiliza `wait-for-it.sh` el cual espera que la base de datos esté conectada correctamente y luego ejecuta las migraciones necesarias, para crear las tablas y cargar datos de prueba.

### Frontend Docker:
1. **Dockerfile**: Se encuentra containerizado utilizando un **Dockerfile** el cual define el entorno de ejecución de la web.

### Base de Datos Docker:
1. **Dockerfile**: La base de datos también está containerizada utilizando **Docker**.  El contenedor de PostgreSQL se configura y crea dentro del archivo **docker-compose.yml**.

## 5. Test

### Herramientas de Prueba:
- **xUnit**: Framework utilizado para pruebas unitarias.
- **Pendiente**: 
	- Pruebas de integración.
	- Pruebas automatizadas. 

## 6. Pasos para Ejecutar el Proyecto

Para ejecutar el proyecto localmente, sigue estos pasos:

 - **Clonar el repositorio**:
   Clona el repositorio usando Git:

   ```bash
   git clone <URL del repositorio>

 - **Acceder a la carpeta del proyecto**:
Después de clonar el repositorio, navega a la carpeta raíz donde se encuentra el archivo docker-compose.yml
Este archivo docker-compose.yml es el encargado de orquestar los contenedores de Docker para el backend, frontend y base de datos.

 - **Construir y ejecutar los contenedores con Docker Compose**
 Ejecutando este comando se crearan los contenedores de **api**, **web** y **api-db**.
      ```bash
   docker-compose up --build

 - **Acceder a la aplicación**
	- **Backend API**: La API del backend debería estar disponible en **http://localhost:5133**
	- **Frontend**: El frontend debería estar disponible en **http://localhost:5173**
	- **Base de datos**: PostgreSQL corre en el pueto **5432**

 - **Usuario de prueba:**
     - **Email**: prueba@prueba.com
     - **Contraseña**: Abcd1234_

 - **Para probar solo la api**
	 - **Hacer login ya que necesita autorización**
		   - **Login**: http://localhost:5133/api/login
	- **Endpoints disponibles**
		- **Añadir tarea**: http://localhost:5133/api/task/add-new
		- **Obtener todas las tareas**: http://localhost:5133/api/task/get-all
		- **Obtener tarea por ID**: http://localhost:5133/api/task/get-by-id/{id}
		- **Eliminar tarea**: http://localhost:5133/api/task/delete/{id}
		- **Actualizar tarea**: http://localhost:5133/api/task/update/{id}
		- **Cambiar estado de tarea**: http://localhost:5133/api/task/change-status/{id}
