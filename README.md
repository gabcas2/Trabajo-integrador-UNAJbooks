# UNAJBOOKS

Sistema de gestión para una biblioteca universitaria.

El proyecto forma parte del Trabajo Integrador 2026 de la materia
Metodología de Programación II.

## Tecnologías utilizadas

- C#
- .NET 9
- Entity Framework Core 9
- PostgreSQL
- Npgsql
- Git / GitHub

## Estado actual del proyecto

Actualmente se encuentra implementada y probada la funcionalidad de gestión
de libros correspondiente a:

- Registrar un libro
- Buscar un libro por código
- Consultar todos los libros
- Eliminar un libro
- Consultar stock de un libro
- Consultar que libros pidio prestado un socio
- Consultar el historial de prestamos

La aplicación utiliza PostgreSQL como sistema de persistencia.

La arquitectura actual separa:

- Domain: entidades y reglas propias del dominio.
- Application: interfaces y casos de uso.
- Infrastructure: persistencia y repositorios.
- Presentacion: Metodos y excepciones definidas por el usuario

## Requisitos previos

Para ejecutar el proyecto es necesario tener instalado:

- .NET SDK 9
- PostgreSQL
- Git

Se puede comprobar la instalación de .NET ejecutando:

```bash
dotnet --version
