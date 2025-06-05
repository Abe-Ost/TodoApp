# Aplicación de Gestión de Tareas (Todo App)

Esta es una aplicación de gestión de tareas desarrollada con React y Tailwind CSS que permite a los usuarios gestionar sus tareas diarias. La aplicación ofrece dos modos de funcionamiento: uno con datos mockeados (para pruebas) y otro conectado a una API real.

## Características

-  Autenticación de usuarios
-  Interfaz moderna y responsive con Tailwind CSS
-  Gestión completa de tareas (CRUD)
-  Modo dual: datos mockeados o API real
-  Estadísticas de tareas
-  Prioridades con códigos de colores
-  Estado de completado para tareas

## Funcionalidades Principales

### Gestión de Tareas
- Crear nuevas tareas con título, descripción, fecha de vencimiento y prioridad
- Editar tareas existentes
- Eliminar tareas
- Marcar tareas como completadas
- Filtrar y visualizar tareas por prioridad

### Autenticación
- Sistema de login
- Persistencia de sesión
- Gestión de tokens JWT
- Cierre de sesión

### Estadísticas
- Total de tareas
- Tareas completadas vs pendientes
- Distribución por prioridad
- Porcentaje de progreso

## Requisitos Previos

- Node.js (v14 o superior)
- npm (incluido con Node.js)

## Instalación

1. Clonar el repositorio:
\`\`\`bash
git clone [URL_DEL_REPOSITORIO]
cd TodoApp
\`\`\`

2. Instalar dependencias:
\`\`\`bash
npm install
\`\`\`

## Configuración

La aplicación puede funcionar en dos modos:

### Modo Mock (Datos de Prueba)
- No requiere configuración adicional
- Usa datos de ejemplo almacenados localmente
- Perfecto para pruebas y desarrollo

### Modo API Real
- Requiere una API backend corriendo en http://localhost:5119
- Necesita configuración de endpoints en src/services/apiService.js

## Ejecución

1. Iniciar el servidor de desarrollo:
\`\`\`bash
npm run dev
\`\`\`

2. Abrir el navegador en:
\`\`\`
http://localhost:5173
\`\`\`

## Uso

### Credenciales de Prueba (Modo Mock)
- Email: test@example.com
- Password: password

### Operaciones CRUD

1. **Crear Tarea**
   - Click en el formulario superior
   - Completar título (obligatorio)
   - Agregar descripción (opcional)
   - Seleccionar fecha de vencimiento
   - Elegir prioridad (baja/media/alta)
   - Click en "Agregar Tarea"

2. **Editar Tarea**
   - Click en "Editar" en la tarea deseada
   - Modificar los campos necesarios
   - Click en "Guardar" para confirmar
   - Click en "Cancelar" para descartar cambios

3. **Eliminar Tarea**
   - Click en "Eliminar" en la tarea
   - Confirmar la eliminación en el diálogo

4. **Marcar como Completada**
   - Click en el checkbox junto al título
   - La tarea se mostrará tachada

### Cambiar entre Modos (Mock/API)

Para cambiar entre el modo mock y la API real:

1. Abrir src/App.jsx
2. Modificar la línea:
\`\`\`javascript
const service = mockService; // Para datos mock
// o
const service = apiService; // Para API real
\`\`\`

## Estructura del Proyecto

\`\`\`
src/
 ├── components/
 │   ├── Login.jsx       # Componente de autenticación
 │   ├── TaskForm.jsx    # Formulario de creación/edición
 │   ├── TaskItem.jsx    # Componente individual de tarea
 │   └── TaskStats.jsx   # Estadísticas de tareas
 │
 ├── services/
 │   ├── apiService.js   # Servicios para API real
 │   └── mockService.js  # Servicios mockeados
 │
 └── App.jsx            # Componente principal
\`\`\`

## Tecnologías Utilizadas

- React 18
- Tailwind CSS
- Vite
- JWT para autenticación

## Contribución

Si deseas contribuir al proyecto:

1. Fork el repositorio
2. Crea una rama para tu feature
3. Commit tus cambios
4. Push a la rama
5. Crea un Pull Request

## Licencia

MIT 