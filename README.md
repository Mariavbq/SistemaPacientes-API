# SistemaPacientesAPI — Tarea 2 de Programación II

Web API sencilla con **ASP.NET Core + Controllers**, **Entity Framework Core** y **SQL Server**. Incluye dos entidades (`Paciente` y `Doctor`) y CRUD completo para ambas, DTOs, contexto y appsettings. Desarrollado como una práctica introductoria, sin capas de repositorios/servicios adicionales.

## Estructura

```text
SistemaPacientesAPI/
├── Controllers/
│   ├── PacientesController.cs
│   └── DoctoresController.cs
├── Models/
│   ├── Paciente.cs
│   └── Doctor.cs
├── DTOs/
│   ├── PacienteDto.cs
│   ├── PacienteCrearDto.cs
│   ├── DoctorDto.cs
│   └── DoctorCrearDto.cs
├── Context/
│   └── AppDbContext.cs
├── Database/
│   └── consulta-verificacion.sql
├── Properties/
│   └── launchSettings.json
├── appsettings.json
├── Program.cs
└── SistemaPacientesAPI.csproj
```

## Antes de ejecutar

1. Tener instalado el **.NET SDK 9** (o compatible) y **SQL Server Express** (el motor de base de datos, no solo SSMS).
2. Descomprimir, abrir en VS Code la carpeta donde está `SistemaPacientesAPI.csproj`.
3. Revisar en `appsettings.json` la conexión `ConnectionStrings:DefaultConnection`:

```json
"DefaultConnection": "Server=.\\SQLEXPRESS;Database=SistemaPacientesAPI;Trusted_Connection=True;TrustServerCertificate=True;"
```

**IMPORTANTE:** dentro del archivo JSON real el separador se representa `\`. Cambia el servidor por el que aparece en la ventana de conexión de SSMS. Ejemplos conocidos de otros equipos: `MARIVI\SQLEXPRESS` o `CCJ-COR\SQLEXPRESS01`. *El SQL Server que usarás debe encontrarse en la misma computadora o ser accesible.*

4. Abrir terminal en la carpeta del proyecto:

```powershell
dotnet restore
dotnet build
dotnet run --launch-profile http
```

5. Abrir `http://localhost:5180/swagger`. En el arranque se usa `EnsureCreated()` para crear **SistemaPacientesAPI** y sus dos tablas si todavía no existen. Tu usuario de Windows debe tener permisos para crear la base. Si ya existe una base con estructura diferente, no se actualizarán las tablas; usa una base nueva para esta tarea.

## Endpoints (10)

| Método | Ruta | Función |
|---|---|---|
| GET | `/api/pacientes` | Listar pacientes |
| GET | `/api/pacientes/1` | Consultar un paciente |
| POST | `/api/pacientes` | Registrar paciente |
| PUT | `/api/pacientes/1` | Actualizar paciente |
| DELETE | `/api/pacientes/1` | Eliminar paciente |
| GET | `/api/doctores` | Listar doctores |
| GET | `/api/doctores/1` | Consultar doctor |
| POST | `/api/doctores` | Registrar doctor |
| PUT | `/api/doctores/1` | Actualizar doctor |
| DELETE | `/api/doctores/1` | Eliminar doctor |

### JSON de ejemplo para crear paciente

```json
{
  "nombre": "Paciente de prueba",
  "cedula": "000-0000000-0",
  "edad": 24,
  "telefono": "809-000-0000"
}
```

### JSON de ejemplo para crear doctor

```json
{
  "nombre": "Doctor de prueba",
  "especialidad": "Medicina general",
  "telefono": "809-000-0001"
}
```

## Prueba mínima para mostrar al profesor

1. `POST /api/doctores`: crear doctor.
2. `GET /api/doctores`: verlo en la lista.
3. `POST /api/pacientes`: crear paciente.
4. `GET /api/pacientes`: verlo en la lista.
5. Editar uno con `PUT`, comprobar con `GET`.
6. Eliminar uno con `DELETE`, comprobar `404` con `GET /{id}`.
7. Consultar SQL Server con `Database/consulta-verificacion.sql`.

## Subir a GitHub

> La tarea exige **entregar el enlace público**, no el archivo ZIP.

Crea un repositorio público (por ejemplo `SistemaPacientes-API`), ejecuta en esta carpeta:

```powershell
git init
git add .
git commit -m "Tarea 2 CRUD Web API de pacientes y doctores"
git branch -M main
git remote add origin https://github.com/TU_USUARIO/SistemaPacientes-API.git
git push -u origin main
```

Si es el **mismo repositorio** que creaste para la Tarea 1, usa su URL; no crees otro si vas a trabajar con un repositorio único. Si ya existe `origin`, usa `git remote set-url origin URL`.

## Nota académica

La estructura cumple los elementos **explícitos** del enunciado (Controllers, Context, DTOs, Models, appsettings). Como no contamos con los ejemplos exactos de las sesiones en vivo, verifica el nombre de los folders y el formato de los DTOs frente a lo que demostró el profesor.

Este es un proyecto didáctico: no utiliza datos clínicos reales ni implementa autenticación y autorización. Emplea solo información de prueba.
