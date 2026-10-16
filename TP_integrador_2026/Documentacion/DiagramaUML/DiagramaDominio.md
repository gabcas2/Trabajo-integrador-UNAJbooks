<!-- Para visualizar el diagrama UML, presionar Ctrl + Shift + V para abrir la vista previa de Markdown. -->

# UNAJBOOKS — Diagrama de dominio

Este diagrama muestra las entidades del dominio, sus atributos, sus operaciones y sus relaciones.

```mermaid
classDiagram
direction TB

class Libro {
    +int Id
    +string Codigo
    +string Titulo
    +string Autor
    +string Editorial
    +int Stock
    +Libro(titulo, autor, editorial, stock)
    +AumentarStock(cantidad)
    +DisminuirStock()
    +ActualizarStock(nuevoStock)
    -GenerarCodigo() string
}

class Socio {
    +int Id
    +string Nombre
    +string Apellido
    +int DNI
    +int NumTelefono
    +string Direccion
    +MaxPrestamosActivos int
    +DiasPrestamo int
    +Socio(nombre, apellido, dni, numTelefono, direccion)
    +ActualizarTelefono(nuevoTelefono)
    +ActualizarDireccion(nuevaDireccion)
}

class SocioPremium {
    +MaxPrestamosActivos int
    +DiasPrestamo int
    +SocioPremium(nombre, apellido, dni, numTelefono, direccion)
}

class Prestamo {
    +int Id
    +int LibroId
    +int SocioId
    +DateTime FechaPrestamo
    +DateTime FechaDevolucion
    +DateTime FechaVencimiento
    +Libro Libro
    +Socio Socio
    +Prestamo(libro, socio)
    +RegistrarDevolucion()
    +EstaActivo() bool
}

class NotFoundException {
    +NotFoundException(message)
}

SocioPremium --|> Socio : hereda

Prestamo "0..*" --> "1" Libro : referencia
Prestamo "0..*" --> "1" Socio : pertenece a

NotFoundException --|> Exception
```