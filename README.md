# TodoApp - API de Gestión de Tareas

## Descripción
TodoApp es una API RESTful desarrollada en ASP.NET Core que permite la gestión de tareas, incluyendo autenticación de usuarios, categorización de tareas y seguimiento de estados.

## Diagramas
Los diagramas del proyecto se encuentran en la carpeta `docs/`:
- `project-structure.mmd`: Diagrama de la estructura del proyecto
- `database-er.mmd`: Diagrama Entidad-Relación de la base de datos

## Estructura del Proyecto

La aplicación está organizada en una arquitectura limpia con las siguientes carpetas principales:

- **Controllers/**: Contiene los controladores que manejan las peticiones HTTP
- **Models/**: Define las entidades principales del sistema
- **DTOs/**: Objetos de transferencia de datos para las operaciones API
- **Services/**: Servicios que implementan la lógica de negocio
- **Data/**: Contiene el contexto de la base de datos y configuraciones
- **Migrations/**: Archivos de migración de Entity Framework Core

## Base de Datos

El sistema utiliza las siguientes entidades principales:

### User (Usuario)
- Id (int)
- Username (string)
- Password (string)
- Email (string)
- Role (string)

### Task (Tarea)
- Id (int)
- Title (string)
- Description (string)
- CreatedDate (DateTime)
- UserId (int)
- CategoryId (int)
- StateId (int)

### Category (Categoría)
- Id (int)
- Name (string)
- Description (string)

### State (Estado)
- Id (int)
- Name (string)
- Description (string)

## Endpoints y Uso

### Autenticación
\`\`\`
POST /api/User/register
- Registro de nuevo usuario
Body: {
    "username": "string",
    "email": "string",
    "password": "string"
}

POST /api/User/login
- Inicio de sesión
Body: {
    "email": "string",
    "password": "string"
}
\`\`\`

### Tareas
\`\`\`
GET /api/Task
- Obtener todas las tareas (requiere autenticación)

POST /api/Task
- Crear nueva tarea
Body: {
    "title": "string",
    "description": "string",
    "categoryId": int,
    "stateId": int
}

PUT /api/Task/{id}
- Actualizar tarea existente
Body: {
    "title": "string",
    "description": "string",
    "categoryId": int,
    "stateId": int
}

DELETE /api/Task/{id}
- Eliminar tarea
\`\`\`

### Categorías
\`\`\`
GET /api/Category
- Obtener todas las categorías

POST /api/Category
- Crear nueva categoría (requiere rol Admin)
Body: {
    "name": "string",
    "description": "string"
}
\`\`\`

### Estados
\`\`\`
GET /api/State
- Obtener todos los estados

POST /api/State
- Crear nuevo estado (requiere rol Admin)
Body: {
    "name": "string",
    "description": "string"
}
\`\`\`

## Instrucciones de Uso

1. **Autenticación**:
   - Primero debes registrarte usando el endpoint `/api/User/register`
   - Luego inicia sesión con `/api/User/login` para obtener el token JWT
   - Incluye el token en el header de las siguientes peticiones: `Authorization: Bearer {token}`

2. **Gestión de Tareas**:
   - Para crear una tarea, necesitas estar autenticado
   - Debes especificar una categoría y estado válidos al crear/actualizar tareas
   - Solo puedes modificar/eliminar tus propias tareas

3. **Administración**:
   - Las operaciones de creación/modificación de Categorías y Estados requieren rol de Admin
   - Los usuarios normales solo pueden ver las categorías y estados disponibles

## Consideraciones Técnicas

- API desarrollada en ASP.NET Core
- Entity Framework Core para la gestión de la base de datos
- Autenticación JWT
- Validación de roles (Admin/User)
- API configurada para ejecutarse en `http://localhost:5119`
- Respuestas en formato JSON
- Implementación del patrón DTO
- Manejo de errores centralizado

## Pruebas

Para probar la API puedes utilizar:
1. Swagger UI (disponible en `/swagger`)
2. El archivo `TodoApp.http` incluido en el proyecto
3. Cualquier cliente HTTP como Postman o cURL

## Requisitos del Sistema

- .NET 6.0 o superior
- SQL Server (LocalDB o instancia completa)

## Configuración Inicial

1. Clona el repositorio
2. Actualiza la cadena de conexión en `appsettings.json` si es necesario
3. Ejecuta las migraciones:
   ```
   dotnet ef database update
   ```
4. Ejecuta el proyecto:
   ```
   dotnet run
   ```

## Contacto y Soporte

Para reportar problemas o sugerir mejoras, por favor crea un issue en el repositorio. 