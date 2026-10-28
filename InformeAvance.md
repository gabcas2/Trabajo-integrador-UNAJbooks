# UNAJBOOKS — Informe de avance H7

**Estado:** borrador pendiente de revisión y finalización.

## 1. Datos generales

- **Proyecto:** UNAJBOOKS.
- **Asignatura:** Metodología de Programación II.
- **Trabajo:** Integrador 2026.
- **Integrantes:** Agustin Lozada, Nabergoj Alan, Castege Gabriel.
- **Fecha:** .

## 2. Descripción del proyecto

UNAJBOOKS es un sistema de gestión para una biblioteca universitaria. Contempla la administración de libros, socios y préstamos, así como el control de la disponibilidad de los ejemplares.

El proyecto está desarrollado en C# con .NET 9, Entity Framework Core 9 y PostgreSQL.

## 3. Objetivos

El proyecto busca aplicar los conceptos de programación orientada a objetos mediante un sistema que permita gestionar las principales operaciones de una biblioteca.

Entre sus objetivos se encuentran modelar las entidades del dominio, aplicar encapsulamiento, herencia y polimorfismo cuando corresponda, separar responsabilidades mediante capas, implementar casos de uso y probar el comportamiento del sistema.

## 4. Arquitectura

La solución se organiza en las siguientes capas(patron por capas):

- **Domain:** entidades y reglas del dominio.
- **Application:** interfaces y casos de uso.
- **Infrastructure:** repositorios y persistencia.
- **Presentación:** menús e interacción con el usuario mediante consola.

Esta organización separa las reglas del negocio de la presentación y los detalles de persistencia.

## 5. Diseño y documentación UML

El repositorio contiene los diagramas de dominio, aplicación, infraestructura y presentación, además de un diagrama de secuencia relacionado con el préstamo.

Los archivos están en `TP_integrador_2026/Documentacion/DiagramaUML/` y `TP_integrador_2026/Documentacion/DiagramaSecuencia/`.


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
- Mermaid Preview 

Las dependencias se declaran en los archivos de proyecto.

## 9. Pruebas automatizadas

La solución incluye el proyecto `TP_integrador_2026.Tests`.

En el commit `bb5c1bc`, la ejecución de `dotnet test` informó:

- Total de pruebas: 105.
- Aprobadas: 105.
- Errores: 0.
- Omitidas: 0.

Las pruebas abarcan entidades, casos de uso, repositorios y configuración de persistencia. Algunas utilizan el proveedor InMemory de Entity Framework Core; por eso, el resultado no equivale a una prueba completa contra una instancia real de PostgreSQL.

Para repetir las pruebas desde la raíz del repositorio, ejecutar `dotnet test .\TP_integrador_2026\TP_integrador_2026.sln`.//pedir captura de las pruebas

## 10. Refactorización

El historial de Git contiene el commit `3831623`, cuyo mensaje es `Refactor Biblioteca.cs for improved readability`.

El cambio afectó a `Biblioteca.cs` e incluyó ajustes en la organización del código y modificaciones relacionadas con los métodos de presentación.

El detalle se documenta en `TP_integrador_2026/Documentacion/Refactor/RefactorBiblioteca.md`.

Se distingue entre los cambios orientados a la legibilidad y los cambios funcionales que aparecen en esa versión.

## 11. Patrones de diseño

11.1. Repository (implementado)

El proyecto utiliza el patrón Repository para separar el acceso a los datos de la lógica de los casos de uso. La capa Application trabaja con interfaces de repositorio, mientras que Infrastructure contiene las implementaciones concretas que acceden a la persistencia mediante Entity Framework Core.

Por ejemplo, los casos de uso pueden utilizar interfaces como ILibroRepository e IPrestamoRepository sin depender directamente de las consultas realizadas sobre la base de datos.

Esta separación facilita las pruebas automatizadas, ya que permite sustituir los repositorios concretos por implementaciones simuladas cuando se prueban los casos de uso.

11.2. Inyección de dependencias (implementada)

El proyecto utiliza inyección de dependencias para proporcionar a las clases los objetos que necesitan, en lugar de obligarlas a construir sus propias dependencias.

La configuración de los servicios se realiza en Program.cs, mientras que los casos de uso y los repositorios reciben sus dependencias mediante sus constructores.

Por ejemplo, un caso de uso puede recibir una implementación de ILibroRepository a través de su constructor. Esto reduce el acoplamiento entre las clases y permite utilizar distintas implementaciones de una misma interfaz, según la configuración de la aplicación o las necesidades de las pruebas.

11.3. Strategy (propuesta de aplicación)

El patrón Strategy podría utilizarse para encapsular distintas políticas de préstamo según el tipo de socio. Por ejemplo, las reglas relativas al plazo de préstamo o a la cantidad máxima de préstamos activos podrían representarse mediante estrategias intercambiables.

De esta manera, si las políticas cambiasen o se incorporaran nuevos tipos de socios, sería posible incorporar nuevas estrategias sin concentrar todas las variantes en una misma clase mediante múltiples condiciones.

Esta aplicación se presenta como una propuesta de diseño y no como una funcionalidad implementada actualmente. Antes de incorporarla al código, sería necesario analizar las reglas existentes y determinar si una estrategia independiente aporta una mejora real respecto del comportamiento que ya se encuentra modelado mediante las clases de socios.



## 12. Metodología y organización del equipo

La documentación debe indicar la metodología utilizada, la organización de las tareas, la distribución de responsabilidades y las evidencias de colaboración.

Pendiente de completar:

- Metodología adoptada: Para el desarrollo de este trabajo practico la metodologia utilizada fue Scrum, las tareas estuvieron divididas en tres sprints.
  
- Integrantes y responsabilidades reales: El equipo esta integrado por Agustín Lozada, Alan Nabergoj y Gabriel Castege. Cada integrante podia decidir que tarea del sprint iban a realizar y organizar el tiempo que le dedicaban.

- Organizacion de tareas: Las tareas se organizan en distintos tableros de Trello dependiendo de los tres sprints, su estado: "Haciendo", "En revision" y "Hecho" y el Backlog donde se almacenan antes de organizarlas en los sprints.
   
- Evidencias del tablero de Trello.
  ![image alt](https://github.com/gabcas2/Trabajo-integrador-UNAJbooks/blob/d6e455844f0a73ddc2f81f6fb31c6716ed3c9946/Trello.PNG)
- Actas de seguimiento exigidas por la cátedra.
- Aportes realizados por cada integrante.//aca iria una captura de los commits de cada integrante

## 13. Ejecución

Requisitos previos: .NET SDK 9, PostgreSQL y una base de datos configurada con las migraciones necesarias.

Desde la raíz del repositorio, ejecutar `dotnet run --project .\TP_integrador_2026\TP_integrador_2026.csproj`.

La configuración de la conexión y el procedimiento de inicialización de la base de datos deben quedar documentados con instrucciones que puedan seguirse en otra computadora.//pedir la configuracion de postgreSQL

Configuracion para usar credenciales locales:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=UNAJBooks;Username=postgres;Password=TU_CONTRASEÑA"
  }
}
```

## 14. Control de versiones

El proyecto utiliza Git y GitHub para conservar el historial de cambios y trabajar mediante ramas, cada miembro del equipo tenia su propia rama desde donde subia sus respectivas modificaciones correspondientes al Main, por lo general se consultaba a todo el equipo antes de integrar los Pull Request mediante un Merge.


## 15. Pendientes antes de la entrega

- Revisar la cobertura de pruebas según los requisitos de la materia.
- Revisar el informe completo contra las pautas de la asignatura.

---

