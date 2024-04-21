Backend del Portal de Empleo SGPAv2
Descripción
Este repositorio contiene el código del backend para el "Portal de Empleo SGPAv2". Este servicio está diseñado para gestionar todas las operaciones de backend relacionadas con la búsqueda y postulación a ofertas de trabajo. Está desarrollado en C#.NET y utiliza Entity Framework para interactuar con una base de datos SQL Server.

Tecnologías Utilizadas
C#.NET: Lenguaje de programación para el desarrollo del backend.
Entity Framework: ORM utilizado para la manipulación y consulta de la base de datos.
SQL Server: Sistema de gestión de base de datos relacional.
Características
Autenticación y Autorización: Implementado mediante JWT para asegurar y gestionar el acceso a las funcionalidades del sistema.
Patrón DTO: Utilizado para la transferencia de datos entre el frontend y el backend.
Patrón Singleton: Aplicado para gestionar las conexiones a la base de datos.
MVC: Utilizado para organizar el código en modelos, vistas y controladores.
Patrones de Diseño y Buenas Prácticas
Clean Code: Enfoque en escribir código claro y comprensible para mejorar la mantenibilidad.
Pruebas Unitarias: Para verificar la lógica de los componentes de forma aislada.
Control de Versiones con Git: Empleando estrategias para una colaboración eficaz y segura.
Estructura del Proyecto
bash
Copy code
/backend
|-- /Controllers      # Controladores para manejar las solicitudes API
|-- /Models           # Modelos que representan las entidades de la base de datos
|-- /Data             # Contexto de Entity Framework y clases de acceso a datos
|-- /Services         # Servicios para lógica de negocio
|-- /DTOs             # Objetos de Transferencia de Datos
|-- appsettings.json  # Configuraciones del proyecto
|-- Program.cs        # Punto de entrada del backend
|-- Startup.cs        # Configuración de la aplicación
Instalación y Configuración
Requisitos Previos
.NET SDK
SQL Server
Pasos para la Instalación
Clonar el repositorio:
bash
Copy code
https://github.com/Santyzz0311/backend-portal-empleos.git
Instalar dependencias:
bash
Copy code
cd PortalDeEmpleoSGPAv2-backend
dotnet restore
Configurar la base de datos:
Ejecutar los scripts SQL en su instancia de SQL Server.
Configurar la cadena de conexión en appsettings.json.
Ejecutar el Backend
arduino
Copy code
dotnet run
El servidor backend se iniciará y estará disponible en http://localhost:5000/.

Documentación de API
La documentación de la API está disponible en http://localhost:5000/swagger, proporcionando una interfaz para interactuar con la API y ver todos los endpoints disponibles.