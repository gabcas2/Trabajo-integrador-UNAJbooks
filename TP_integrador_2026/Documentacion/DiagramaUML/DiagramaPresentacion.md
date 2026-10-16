<!-- Para visualizar el diagrama UML, presionar Ctrl + Shift + V para abrir la vista previa de Markdown. -->

# UNAJBOOKS — Diagrama de presentación

Este diagrama representa el punto de entrada del programa, el menú principal y la relación de los menús con los casos de uso.

## 1. Entrada del programa

```mermaid
classDiagram
direction TB

class Program {
    +Main(args)
}

class Biblioteca {
    <<static>>
    +RegistrarLibroMenu(registrarLibro)
    +BuscarLibroMenu(buscarLibro)
    +ConsultarLibrosMenu(consultarLibros)
    +RecorridosLibrosMenu(consultarLibros)
    +EliminarLibroMenu(eliminarLibro)
    +RegistrarSocioMenu(registrarSocio)
    +BuscarSocioMenu(buscarSocio)
    +ConsultarSociosMenu(consultarSocios)
    +ModificarSocioMenu(modificarSocio)
    +EliminarSocioMenu(eliminarSocio)
    +RegistrarPrestamoMenu(registrarPrestamo)
    +RegistrarDevolucionMenu(registrarDevolucion)
    +VerPrestamosActivosMenu(consultarPrestamos)
    +ConsultarPrestamosSocioMenu(consultarPrestamosSocio)
}

class ServiceCollection {
    <<Microsoft.Extensions.DependencyInjection>>
}

class AppDbContext {
    <<Infrastructure.Persistence>>
}

Program ..> ServiceCollection : configura dependencias
Program ..> AppDbContext : configura PostgreSQL
Program ..> Biblioteca : invoca menus
```

## 2. Menús y casos de uso

```mermaid
classDiagram
direction TB

class Biblioteca {
    <<static>>
}

class RegistrarLibro
class BuscarLibro
class ConsultarLibros
class EliminarLibro

class RegistrarSocio
class BuscarSocio
class ConsultarSocios
class ModificarSocio
class EliminarSocio

class RegistrarPrestamo
class RegistrarDevolucion
class ConsultarPrestamosActivos
class ConsultarPrestamosSocio

Biblioteca ..> RegistrarLibro : registrar libro
Biblioteca ..> BuscarLibro : buscar libro
Biblioteca ..> ConsultarLibros : consultar libros y demostrar recorridos
Biblioteca ..> EliminarLibro : eliminar libro

Biblioteca ..> RegistrarSocio : registrar socio
Biblioteca ..> BuscarSocio : buscar socio
Biblioteca ..> ConsultarSocios : consultar socios
Biblioteca ..> ModificarSocio : modificar socio
Biblioteca ..> EliminarSocio : eliminar socio

Biblioteca ..> RegistrarPrestamo : registrar prestamo
Biblioteca ..> RegistrarDevolucion : registrar devolucion
Biblioteca ..> ConsultarPrestamosActivos : ver prestamos activos y conteo por socio
Biblioteca ..> ConsultarPrestamosSocio : consultar prestamos de socio
```