<!-- Para visualizar el diagrama UML, presionar Ctrl + Shift + V para abrir la vista previa de Markdown. -->

# UNAJBOOKS — Diagrama de infraestructura

Este diagrama muestra los repositorios concretos, las interfaces que implementan y su relación con el contexto de persistencia.

## Repositorios y persistencia

```mermaid
classDiagram
direction TB

class ILibroRepository {
    <<interface>>
}

class ISocioRepository {
    <<interface>>
}

class IPrestamoRepository {
    <<interface>>
}

class IBusquedaSocioRepository {
    <<interface>>
}

class LibroRepository {
    -AppDbContext _context
}

class SocioRepository {
    -AppDbContext _context
}

class PrestamoRepository {
    -AppDbContext _context
}

class BusquedaSocioRepository {
    -AppDbContext _context
}

class AppDbContext {
    +DbSet~Libro~ Libros
    +DbSet~Socio~ Socios
    +DbSet~Prestamo~ Prestamos
}

LibroRepository ..|> ILibroRepository
SocioRepository ..|> ISocioRepository
PrestamoRepository ..|> IPrestamoRepository
BusquedaSocioRepository ..|> IBusquedaSocioRepository

LibroRepository --> AppDbContext : utiliza
SocioRepository --> AppDbContext : utiliza
PrestamoRepository --> AppDbContext : utiliza
BusquedaSocioRepository --> AppDbContext : utiliza

AppDbContext --> Libro : persiste
AppDbContext --> Socio : persiste
AppDbContext --> Prestamo : persiste
```