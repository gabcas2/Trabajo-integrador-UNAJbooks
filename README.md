# UNAJBOOKS

Sistema de gestión para una biblioteca universitaria, desarrollado como parte del Trabajo Integrador 2026 de Metodología de Programación II.

## Tecnologías utilizadas

- C#
- .NET 9
- Entity Framework Core 9
- PostgreSQL
- Npgsql
- xUnit
- Git y GitHub

## Funcionalidades implementadas

### Gestión de libros
- Registrar libros.
- Buscar libros por código.
- Consultar libros y su stock.
- Eliminar libros respetando las reglas del sistema.
- Filtrar libros con stock y sin stock.
- Obtener títulos y buscar el primer libro con stock.

### Gestión de socios
- Registrar y buscar socios.
- Consultar, modificar y eliminar socios.
- Gestionar socios comunes y socios premium.

### Gestión de préstamos
- Registrar préstamos y controlar la disponibilidad de libros.
- Registrar devoluciones y actualizar el stock.
- Consultar préstamos activos y préstamos de un socio.
- Consultar el historial de préstamos.
- Contabilizar préstamos activos por socio.

## Arquitectura

El proyecto organiza sus responsabilidades en las siguientes capas:

- **Domain:** entidades y reglas del dominio.
- **Application:** interfaces y casos de uso.
- **Infrastructure:** persistencia y repositorios.
- **Presentation:** interacción con el usuario.

## Requisitos previos

Para ejecutar el proyecto se necesita:

- .NET SDK 9.
- PostgreSQL.
- Una base de datos configurada para la aplicación.

Para comprobar la versión de .NET instalada:

```powershell
dotnet --version
```

## Configuración de la base de datos

El archivo `TP_integrador_2026/appsettings.json` contiene la configuración general de conexión.

Para usar credenciales locales, crear `TP_integrador_2026/appsettings.Development.json` con este formato y reemplazar los valores de ejemplo por los de la instalación local:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=UNAJBooks;Username=postgres;Password=TU_CONTRASEÑA"
  }
}
```

El archivo `appsettings.Development.json` es de configuración local y no debe subirse al repositorio.

Antes de ejecutar la aplicación, la base de datos debe estar disponible y tener aplicadas las migraciones del proyecto.

## Ejecución

Desde la carpeta raíz del repositorio:

```powershell
dotnet run --project .\TP_integrador_2026\TP_integrador_2026.csproj
```

## Pruebas

Para ejecutar la suite de pruebas:

```powershell
dotnet test .\TP_integrador_2026\TP_integrador_2026.sln
```

## Estado de las pruebas

Las pruebas automatizadas verifican reglas de las entidades, casos de uso, repositorios y configuración de persistencia. La cantidad de pruebas y sus resultados pueden consultarse ejecutando el comando anterior.

## Repositorio

Código fuente: https://github.com/gabcas2/Trabajo-integrador-UNAJbooks