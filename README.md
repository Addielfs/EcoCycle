# EcoCycle - Sistema de Recolección y Reciclaje de Materiales

Sistema web desarrollado con ASP.NET Core 10 que permite gestionar materiales reciclables, publicaciones, centros de reciclaje y recompensas para usuarios. Incluye roles (Admin/Usuario), mapa interactivo con Leaflet y carga de imágenes.

## Tecnologías

- **Backend API:** ASP.NET Core 10 Web API
- **Frontend:** ASP.NET Core 10 MVC con Razor Pages y Bootstrap 5
- **Base de datos:** SQL Server
- **ORM:** Entity Framework Core 10 (Code First)
- **Autenticación:** JWT (JSON Web Tokens) con roles
- **Validaciones:** FluentValidation
- **Mapas:** Leaflet + OpenStreetMap (sin API key)
- **Imágenes:** Almacenamiento local en `wwwroot/uploads/`
- **Documentación API:** Swagger / OpenAPI
- **Arquitectura:** Clean Architecture simplificada (Repository + Service Pattern)

## Estructura del Proyecto

```
EcoCycle/
├── EcoCycle.Domain/              # Entidades, interfaces de repositorio
├── EcoCycle.Application/         # DTOs, servicios, validaciones
├── EcoCycle.Infrastructure/      # DbContext, repositorios, configuraciones EF
├── EcoCycle.API/                 # Controladores REST, JWT, Middleware
└── EcoCycle.Web/                 # MVC Frontend con Bootstrap
```

## Requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB o Express)
- Visual Studio 2022+ / VS Code

## Configuración

### 1. Base de datos

Edita el archivo `EcoCycle.API/appsettings.json` con tu cadena de conexión:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=EcoCycleDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### 2. JWT

Por defecto viene configurada una clave. Puedes cambiarla en `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "EcoCycleSuperSecretKey2024!@#$%^&*()AtLeast32Chars",
    "Issuer": "EcoCycleAPI",
    "Audience": "EcoCycleApp"
  }
}
```

### 3. Imágenes

Configuración opcional en `EcoCycle.API/appsettings.json`:

```json
{
  "ImageStorage": {
    "MaxSizeMb": 5,
    "AllowedExtensions": [".jpg", ".jpeg", ".png", ".gif", ".webp"]
  }
}
```

### 4. API URL (Frontend)

Edita `EcoCycle.Web/appsettings.json`:

```json
{
  "ApiSettings": {
    "BaseUrl": "http://localhost:5049"
  }
}
```

## Ejecución

### Migraciones

```bash
dotnet ef database update --project EcoCycle.Infrastructure --startup-project EcoCycle.API
```

### Iniciar la API

```bash
cd EcoCycle.API
dotnet run
```

La API se inicia en `http://localhost:5049`.

### Iniciar el Frontend

```bash
cd EcoCycle.Web
dotnet run
```

El frontend se inicia en `http://localhost:5068`.

> **Importante:** Asegúrate de que la API esté corriendo antes de usar el frontend.

## Seed Data

Al iniciar la API por primera vez, se siembran automáticamente:

- **Materiales:** Papel, Cartón, Plástico, Vidrio, Metal, Electrónicos
- **Usuario Admin:** `admin@ecocycle.com` / `Admin123!` (rol "Admin")
- **Factor de Conversión:** `ConversionFactor` = 10 (puntos por kg)

## Roles y Permisos

| Acción | Admin | Usuario |
|--------|-------|---------|
| CRUD Materiales | ✅ | ❌ |
| CRUD Centros Reciclaje | ✅ | ❌ |
| CRUD Recompensas | ✅ | ❌ |
| Crear/Eliminar Publicaciones | ✅ (todas) | ✅ (propias) |
| Ver Reportes | ✅ | ❌ |
| Editar Configuración | ✅ | ❌ |
| Ver Dashboard | ✅ | ✅ |

Los usuarios solo pueden eliminar/editar sus propias publicaciones. El campo `IdUsuario` se asigna desde el JWT, ignorando el valor enviado por el cliente.

## Endpoints de la API

### Autenticación

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/auth/register` | Registrar un nuevo usuario |
| POST | `/api/auth/login` | Iniciar sesión (retorna rol) |

### Materiales (Admin para POST/PUT/DELETE)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/materiales` | Obtener todos los materiales |
| GET | `/api/materiales/{id}` | Obtener material por ID |
| POST | `/api/materiales` | Crear un material |
| PUT | `/api/materiales/{id}` | Actualizar un material |
| DELETE | `/api/materiales/{id}` | Eliminar un material |

### Publicaciones

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/publicaciones` | Obtener todas las publicaciones |
| GET | `/api/publicaciones/{id}` | Obtener publicación por ID |
| GET | `/api/publicaciones/material/{idMaterial}` | Filtrar por material |
| GET | `/api/publicaciones/usuario/{idUsuario}` | Filtrar por usuario |
| POST | `/api/publicaciones` | Crear una publicación |
| PUT | `/api/publicaciones/{id}` | Actualizar una publicación (owner/admin) |
| DELETE | `/api/publicaciones/{id}` | Eliminar una publicación (owner/admin) |
| POST | `/api/publicaciones/{id}/imagen` | Subir imagen a publicación |
| DELETE | `/api/publicaciones/{id}/imagen` | Eliminar imagen de publicación |

### Centros de Reciclaje (Admin para POST/PUT/DELETE)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/centrosreciclaje` | Obtener todos los centros |
| GET | `/api/centrosreciclaje/{id}` | Obtener centro por ID |
| POST | `/api/centrosreciclaje` | Crear un centro |
| PUT | `/api/centrosreciclaje/{id}` | Actualizar un centro |
| DELETE | `/api/centrosreciclaje/{id}` | Eliminar un centro |

### Recompensas (Admin para POST/PUT/DELETE)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/recompensas` | Obtener todas las recompensas |
| GET | `/api/recompensas/usuario/{idUsuario}` | Obtener puntos del usuario |
| POST | `/api/recompensas` | Registrar puntos |
| PUT | `/api/recompensas/{id}` | Actualizar recompensa |
| DELETE | `/api/recompensas/{id}` | Eliminar recompensa |

### Configuración (Admin)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/configuracion` | Obtener todas las configuraciones |
| PUT | `/api/configuracion/{clave}` | Actualizar valor de configuración |

### Reportes (Admin)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/reportes/materiales-mas-publicados` | Materiales más publicados |
| GET | `/api/reportes/puntos-por-usuario` | Puntos acumulados por usuario |

## Funcionalidades Principales

### Mapa Interactivo
- Las publicaciones y centros de reciclaje muestran su ubicación en un mapa Leaflet.
- Al crear/editar, se puede seleccionar la ubicación haciendo clic en el mapa.
- Los marcadores tienen popups con información del elemento.

### Carga de Imágenes
- Las publicaciones aceptan imágenes (JPG, PNG, GIF, WebP, máximo 5 MB).
- Las imágenes se almacenan en `wwwroot/uploads/publicaciones/` con nombre único GUID.
- Se muestran en las tarjetas de publicaciones con `object-fit: cover`.

### Cálculo Automático de Puntos
- Al crear una publicación, se calculan puntos de recompensa: `cantidad × ConversionFactor`.
- Al actualizar la cantidad, se recalcula automáticamente la recompensa asociada.
- El factor de conversión es configurable por el admin desde la interfaz.

## Licencia


Este proyecto cuenta con la licencia de dios.
Proyecto de uso educativo y demostrativo 
