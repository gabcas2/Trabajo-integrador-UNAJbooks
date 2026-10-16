# UNAJBOOKS — Informe de avance H7

**Estado:** borrador pendiente de revisión y finalización.

## 1. Datos generales

- **Proyecto:** UNAJBOOKS.
- **Asignatura:** Metodología de Programación II.
- **Trabajo:** Integrador 2026.
- **Integrantes:** [Completar].
- **Fecha:** [Completar].

## 2. Descripción del proyecto

UNAJBOOKS es un sistema de gestión para una biblioteca universitaria. Contempla la administración de libros, socios y préstamos, así como el control de la disponibilidad de los ejemplares.

El proyecto está desarrollado en C# con .NET 9, Entity Framework Core 9 y PostgreSQL.

## 3. Objetivos

El proyecto busca aplicar los conceptos de programación orientada a objetos mediante un sistema que permita gestionar las principales operaciones de una biblioteca.

Entre sus objetivos se encuentran modelar las entidades del dominio, aplicar encapsulamiento, herencia y polimorfismo cuando corresponda, separar responsabilidades mediante capas, implementar casos de uso y probar el comportamiento del sistema.

## 4. Arquitectura

La solución se organiza en las siguientes capas:

- **Domain:** entidades y reglas del dominio.
- **Application:** interfaces y casos de uso.
- **Infrastructure:** repositorios y persistencia.
- **Presentación:** menús e interacción con el usuario mediante consola.

Esta organización separa las reglas del negocio de la presentación y los detalles de persistencia.

## 5. Diseño y documentación UML

El repositorio contiene los diagramas de dominio, aplicación, infraestructura y presentación, además de un diagrama de secuencia relacionado con el préstamo.

Los archivos están en `TP_integrador_2026/Documentacion/DiagramaUML/` y `TP_integrador_2026/Documentacion/DiagramaSecuencia/`.

Antes de entregar, se debe comprobar que los diagramas coincidan con la implementación actual y completar la especificación de clases y mensajes solicitada por la cátedra.

## 6. Funcionalidades

### Libros

- Registrar y buscar libros.
- Consultar el catálogo y el stock.
- Eliminar libros respetando las reglas del sistema.
- Filtrar libros con stock y sin stock.
- Obtener títulos y buscar el primer libro disponible.

### Socios

- Registrar y buscar socios.
- Consultar, modificar y eliminar socios.
- Gestionar socios comunes y premium.

### Préstamos

- Registrar préstamos y controlar la disponibilidad.
- Registrar devoluciones y actualizar el stock.
- Consultar préstamos activos, préstamos por socio e historial.
- Contabilizar préstamos activos por socio.

## 7. Recorridos de colecciones

En `ConsultarLibros` se implementaron cuatro operaciones:

1. Filtrar libros con stock disponible.
2. Filtrar libros que no cumplen la condición de tener stock disponible.
3. Transformar una colección de libros en una colección de títulos.
4. Buscar el primer libro con stock y devolver `null` si no existe.

En `ConsultarPrestamosActivos` se incorporó un método que cuenta los préstamos activos por DNI mediante un diccionario.

Estas operaciones responden a los requisitos de recorridos, filtrado, transformación, búsqueda y conteo de colecciones.

## 8. Tecnologías

- C# y .NET 9.
- Entity Framework Core 9.
- Npgsql y PostgreSQL.
- xUnit.
- Git y GitHub.

Las dependencias se declaran en los archivos de proyecto.

## 9. Pruebas automatizadas

La solución incluye el proyecto `TP_integrador_2026.Tests`.

En el commit `bb5c1bc`, la ejecución de `dotnet test` informó:

- Total de pruebas: 105.
- Aprobadas: 105.
- Errores: 0.
- Omitidas: 0.

Las pruebas abarcan entidades, casos de uso, repositorios y configuración de persistencia. Algunas utilizan el proveedor InMemory de Entity Framework Core; por eso, el resultado no equivale a una prueba completa contra una instancia real de PostgreSQL.

Para repetir las pruebas desde la raíz del repositorio, ejecutar `dotnet test .\TP_integrador_2026\TP_integrador_2026.sln`.

## 10. Refactorización

El historial de Git contiene el commit `3831623`, cuyo mensaje es `Refactor Biblioteca.cs for improved readability`.

El cambio afectó a `Biblioteca.cs` e incluyó ajustes en la organización del código y modificaciones relacionadas con los métodos de presentación.

El detalle se documenta en `TP_integrador_2026/Documentacion/Refactor/RefactorBiblioteca.md`.

Se distingue entre los cambios orientados a la legibilidad y los cambios funcionales que aparecen en esa versión.

## 11. Patrones de diseño

### Repository

El proyecto utiliza interfaces de repositorio para separar los casos de uso de la persistencia. Esto permite que la capa Application trabaje con contratos sin depender directamente de las consultas concretas a la base de datos.

### Segundo patrón

[Pendiente: elegir y justificar un segundo patrón con un caso de uso concreto. Distinguir entre un patrón implementado y uno cuya aplicación se propone.]

## 12. Metodología y organización del equipo

La documentación debe indicar la metodología utilizada, la organización de las tareas, la distribución de responsabilidades y las evidencias de colaboración.

Pendiente de completar:

- Metodología adoptada.
- Integrantes y responsabilidades reales.
- Evidencias del tablero de Trello.
- Actas de seguimiento exigidas por la cátedra.
- Aportes realizados por cada integrante.

## 13. Ejecución

Requisitos previos: .NET SDK 9, PostgreSQL y una base de datos configurada con las migraciones necesarias.

Desde la raíz del repositorio, ejecutar `dotnet run --project .\TP_integrador_2026\TP_integrador_2026.csproj`.

La configuración de la conexión y el procedimiento de inicialización de la base de datos deben quedar documentados con instrucciones que puedan seguirse en otra computadora.

## 14. Control de versiones

El proyecto utiliza Git y GitHub para conservar el historial de cambios y trabajar mediante ramas.

En el commit `bb5c1bc`, las ramas `main` y `feature/Agustin` quedaron sincronizadas.

Antes de entregar, se deben añadir evidencias de la participación real de los integrantes y de la organización del trabajo.

## 15. Pendientes antes de la entrega

- Completar los datos del equipo y la metodología.
- Incorporar las evidencias de Trello y las actas requeridas.
- Contrastar los diagramas UML con el código.
- Completar la especificación de clases y mensajes.
- Justificar un segundo patrón de diseño.
- Documentar la configuración de la base de datos desde cero.
- Incorporar evidencia de ejecución de la aplicación.
- Revisar la cobertura de pruebas según los requisitos de la cátedra.
- Describir con ejemplos concretos los cambios anteriores y posteriores al refactor.
- Revisar el informe completo contra las pautas de la asignatura.

---

**Nota:** este documento es un borrador de avance. Los puntos pendientes deben resolverse antes de presentarlo como informe final.
