<!-- Para visualizar el diagrama UML, presionar Ctrl + Shift + V para abrir la vista previa de Markdown. -->

# UNAJBOOKS — Diagramas de la capa Application

Estos diagramas muestran los casos de uso de la aplicación, las interfaces que implementan y los repositorios de los que dependen.


## 1. Casos de uso de libros

```mermaid
classDiagram
direction TB

class IRegistrarLibro {
    <<interface>>
    +Ejecutar(titulo, autor, editorial, stock) Libro
}

class ILibroRepository {
    <<interface>>
    +Agregar(libro) Libro
    +BuscarPorCodigo(codigo) Libro
    +ObtenerTodos() List~Libro~
    +Eliminar(libro) void
    +ExisteCodigo(codigo) bool
    +TienePrestamosActivos(libroId) bool
}

class RegistrarLibro {
    -ILibroRepository _libroRepository
    +Ejecutar(titulo, autor, editorial, stock) Libro
}

class BuscarLibro {
    -ILibroRepository _libroRepository
    +Ejecutar(codigo) Libro
}

class ConsultarLibros {
    -ILibroRepository _libroRepository
    +Ejecutar() List~Libro~
}

class EliminarLibro {
    -ILibroRepository _libroRepository
    +Ejecutar(codigo) void
}

RegistrarLibro ..|> IRegistrarLibro
RegistrarLibro --> ILibroRepository
BuscarLibro --> ILibroRepository
ConsultarLibros --> ILibroRepository
EliminarLibro --> ILibroRepository
```

## 2. Casos de uso de socios

```mermaid
classDiagram
direction TB

class IBuscarSocio {
    <<interface>>
    +Ejecutar(dni) Socio
}

class ISocioRepository {
    <<interface>>
    +Agregar(socio) Socio
    +BuscarPorDNI(dni) Socio
    +ObtenerTodos() List~Socio~
    +Eliminar(socio) void
    +ExisteDNI(dni) bool
    +Actualizar(socio, esPremium) void
}

class IPrestamoRepository {
    <<interface>>
    +TienePrestamosActivos(socioId) bool
}

class RegistrarSocio {
    -ISocioRepository _socioRepository
    +Ejecutar(nombre, apellido, dni, telefono, direccion, esPremium) Socio
}

class BuscarSocio {
    -ISocioRepository _socioRepository
    +Ejecutar(dni) Socio
}

class ConsultarSocios {
    -ISocioRepository _socioRepository
    +Ejecutar() List~Socio~
}

class ModificarSocio {
    -ISocioRepository _socioRepository
    +Ejecutar(dni, nuevoTelefono, nuevaDireccion, nuevoEsPremium) void
}

class EliminarSocio {
    -ISocioRepository _socioRepository
    -IPrestamoRepository _prestamoRepository
    +Ejecutar(dni) void
}

BuscarSocio ..|> IBuscarSocio

RegistrarSocio --> ISocioRepository
BuscarSocio --> ISocioRepository
ConsultarSocios --> ISocioRepository
ModificarSocio --> ISocioRepository
EliminarSocio --> ISocioRepository
EliminarSocio --> IPrestamoRepository : verifica prestamos activos
```

## 3. Casos de uso de préstamos

```mermaid
classDiagram
direction TB

class IPrestamoRepository {
    <<interface>>
    +Agregar(prestamo) Prestamo
    +BuscarPorId(id) Prestamo
    +ObtenerTodos() List~Prestamo~
    +ObtenerPorSocio(socioId) List~Prestamo~
    +ObtenerActivos() List~Prestamo~
    +TienePrestamosActivos(socioId) bool
    +TienePrestamoActivo(socioId, libroId) bool
    +ContarPrestamosActivos(socioId) int
    +Actualizar(prestamo) void
}

class IBusquedaSocioRepository {
    <<interface>>
    +BuscarPorDni(dni) Socio
    +ObtenerPrestamosPorSocio(socioId) List~Prestamo~
}

class IConsultarPrestamosSocio {
    <<interface>>
    +BuscarSocio(dni) Socio
    +Ejecutar(socio) List~Prestamo~
}

class ISocioRepository {
    <<interface>>
    +BuscarPorDNI(dni) Socio
}

class ILibroRepository {
    <<interface>>
    +BuscarPorCodigo(codigo) Libro
}

class RegistrarPrestamo {
    -ISocioRepository _socioRepository
    -ILibroRepository _libroRepository
    -IPrestamoRepository _prestamoRepository
    +Ejecutar(dni, codigoLibro) Prestamo
}

class RegistrarDevolucion {
    -IPrestamoRepository _prestamoRepository
    +Ejecutar(prestamoId) Prestamo
}

class ConsultarPrestamosActivos {
    -IPrestamoRepository _prestamoRepository
    +Ejecutar() List~Prestamo~
}

class ConsultarPrestamosSocio {
    -IBusquedaSocioRepository _busquedaSocioRepository
    +BuscarSocio(dni) Socio
    +Ejecutar(socio) List~Prestamo~
}

ConsultarPrestamosSocio ..|> IConsultarPrestamosSocio

RegistrarPrestamo --> ISocioRepository
RegistrarPrestamo --> ILibroRepository
RegistrarPrestamo --> IPrestamoRepository

RegistrarDevolucion --> IPrestamoRepository
ConsultarPrestamosActivos --> IPrestamoRepository
ConsultarPrestamosSocio --> IBusquedaSocioRepository
```