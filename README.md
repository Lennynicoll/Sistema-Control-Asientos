# Seat Management System (Sistema de Control de Asientos)

Este es un sistema de gestión de asientos desarrollado en C# .NET 10.0, diseñado para registrar estudiantes y gestionar su distribución en un aula o salón mediante una matriz dinámica.

## Key Features (Funcionalidades Clave)

- **Gestión de Estudiantes**: Registro y consulta de estudiantes con matrícula, nombre y carrera.
- **Matriz de Asientos Dinámica**: Configuración personalizada de la cantidad de filas y columnas del salón.
- **Asignación Inteligente**: Sistema unificado para asignar o cambiar asientos, liberando automáticamente el lugar anterior si el estudiante ya tenía uno asignado.
- **Mapa Visual**: Visualización en consola de la distribución de los asientos (disponibles y ocupados).
- **Indicadores de Carga**: Mensajes visuales de progreso para mejorar la experiencia del usuario.
- **Navegación por Niveles**: Menús organizados por categorías con opciones para volver atrás.

## Architecture (Arquitectura)

El proyecto sigue el patrón **MVC (Model-View-Controller)** con una capa adicional de **Servicios**:

- **Models**: Clases abstractas y entidades base (POO Avanzada).
- **Views**: Interfaz de consola con textos totalmente en español.
- **Controllers**: Orquestadores del flujo de la aplicación.
- **Services**: Lógica de negocio y gestión de transacciones.
- **Data**: Gestión del contexto de base de datos mediante **Entity Framework Core**.

## Technologies (Tecnologías)

- **C# .NET 10.0**
- **Entity Framework Core** (ORM)
- **SQL Server** (Base de Datos)
- **LINQ** (Consultas de datos)

## Requirements & Rules (Reglas del Proyecto)

1. **Clean Code**: Código fuente íntegramente en inglés, interfaz de usuario en español.
2. **Zero Comments**: El código es autodocumentado, sin comentarios internos.
3. **OOP Strict**: Uso de clases abstractas, herencia, constructores y sobrecarga de métodos.
4. **No Static**: Lógica basada 100% en instanciación de objetos.

## Setup (Instalación)

1. **Base de Datos**:
   - Asegúrese de tener una instancia de SQL Server llamada `SQLEXPRESS01`.
   - Ejecute el script localizado en `CodeDB/DatabaseSchema.sql`.

2. **Migraciones**:
   - El proyecto ya incluye las migraciones necesarias. Puede aplicar cambios con:
     ```bash
     dotnet ef database update
     ```

3. **Ejecución**:
   - Ejecute el proyecto desde la carpeta raíz:
     ```bash
     dotnet run --project ControlDeAsientos
     ```

## Database Schema (Esquema DB)

El esquema se encuentra respaldado en la carpeta `CodeDB/`, conteniendo las tablas:
- `Estudiantes`
- `Asientos`
- `Asignaciones`
