# artyomalvarez-VeterinaryClinicSystem
Sistema de gestión de clínicas veterinarias desarrollado con C# y .NET.

## 📐 Diagrama de Clases UML (Historia M5.3S3)

```mermaid
classDiagram
    class IClienteRepository {
        <<interface>>
        +Agregar(Patient cliente) void
        +AgregarAsync(Patient cliente) Task
        +ObtenerTodos() IReadOnlyList~Patient~
        +ObtenerPorNombre(string nombre) Patient
    }

    class IMascotaRepository {
        <<interface>>
        +Agregar(Pet mascota, Guid clienteId) void
        +ObtenerPorClienteId(Guid clienteId) IReadOnlyList~Pet~
    }

    class ClienteRepository {
        -ConcurrentDictionary~Guid, Patient~ _clientesPorId
        +Agregar(Patient cliente) void
    }

    class MascotaRepository {
        -ConcurrentDictionary~Guid, Tuple~ _mascotasPorId
        +Agregar(Pet mascota, Guid clienteId) void
    }

    class IPatientService {
        <<interface>>
        +RegistrarPaciente(string nombre, int edad, string sintoma) Patient
        +RegistrarPacienteAsync(string nombre, int edad, string sintoma) Task~Patient~
    }

    class PatientService {
        -IClienteRepository _clienteRepository
        -IMascotaRepository _mascotaRepository
    }

    class Persona {
        <<abstract>>
        +Guid Id
        +string Nombre
        +int Edad
        +string Telefono
    }

    class Animal {
        <<abstract>>
        +Guid Id
        +string Nombre
        +string Especie
        +EmitirSonido()* string
    }

    class Patient {
        +string Sintoma
        +IReadOnlyCollection~Pet~ Mascotas
    }

    class Pet {
        +double Peso
        +string Raza
        +EmitirSonido() string
    }

    IClienteRepository <|.. ClienteRepository
    IMascotaRepository <|.. MascotaRepository
    IPatientService <|.. PatientService
    PatientService --> IClienteRepository : Inyección de Dependencias
    PatientService --> IMascotaRepository : Inyección de Dependencias
    Persona <|-- Patient
    Animal <|-- Pet
```
