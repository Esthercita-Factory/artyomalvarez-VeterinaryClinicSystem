using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenterinaryClinicSystem.Exceptions;
using VenterinaryClinicSystem.Models;
using VenterinaryClinicSystem.Repository;

namespace VenterinaryClinicSystem.Services;

/// <summary>
/// Capa SERVICES (Cerebro / Lógica de Negocio):
/// Recibe IClienteRepository e IMascotaRepository por Inyección de Dependencias (Constructor Injection).
/// </summary>
public class PatientService : IPatientService
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMascotaRepository _mascotaRepository;

    // Inyección de Dependencias a través del constructor
    public PatientService(IClienteRepository clienteRepository, IMascotaRepository mascotaRepository)
    {
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        _mascotaRepository = mascotaRepository ?? throw new ArgumentNullException(nameof(mascotaRepository));
        CargarDatosIniciales();
    }

    private void CargarDatosIniciales()
    {
        var p1 = new Patient(Guid.NewGuid(), "Carlos Pérez", 35, "Chequeo de rutina");
        var m1 = new Pet(Guid.NewGuid(), "Firulais", 24, 12.5, "Vacunación", "Perro", "Labrador");
        var m2 = new Pet(Guid.NewGuid(), "Michi", 12, 4.0, "Fiebre", "Gato", "Siames");
        p1.AgregarMascota(m1);
        p1.AgregarMascota(m2);

        var p2 = new Patient(Guid.NewGuid(), "Ana Gómez", 28, "Consulta general");
        var m3 = new Pet(Guid.NewGuid(), "Rex", 36, 20.0, "Cojera", "Perro", "Pastor Alemán");
        p2.AgregarMascota(m3);

        var p3 = new Patient(Guid.NewGuid(), "Beatriz López", 42, "Control de peso");
        var m4 = new Pet(Guid.NewGuid(), "Garfield", 48, 6.5, "Sobrepeso", "Gato", "Persa");
        p3.AgregarMascota(m4);

        _clienteRepository.Agregar(p1);
        _mascotaRepository.Agregar(m1, p1.Id);
        _mascotaRepository.Agregar(m2, p1.Id);

        _clienteRepository.Agregar(p2);
        _mascotaRepository.Agregar(m3, p2.Id);

        _clienteRepository.Agregar(p3);
        _mascotaRepository.Agregar(m4, p3.Id);
    }

    public IReadOnlyList<Patient> ObtenerTodosLosPacientes()
    {
        return _clienteRepository.ObtenerTodos();
    }

    public Patient RegistrarPaciente(string? nombre, int edad, string? sintoma, string? telefono = "000-000-0000")
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre del paciente no puede estar vacío o ser nulo.");
        }

        if (edad <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(edad), "La edad debe ser un número positivo.");
        }

        string sintomaLimpio = string.IsNullOrWhiteSpace(sintoma) ? "Sin síntoma especificado" : sintoma.Trim();
        string telefonoLimpio = string.IsNullOrWhiteSpace(telefono) ? "000-000-0000" : telefono.Trim();

        var nuevoPaciente = new Patient(Guid.NewGuid(), nombre.Trim(), edad, sintomaLimpio, telefonoLimpio);
        _clienteRepository.Agregar(nuevoPaciente);
        LoggerService.LogInfo($"[SÍNCRONO] Cliente '{nuevoPaciente.Nombre}' registrado en IClienteRepository con ID {nuevoPaciente.Id}");
        return nuevoPaciente;
    }

    public async Task<Patient> RegistrarPacienteAsync(string? nombre, int edad, string? sintoma, string? telefono = "000-000-0000")
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            throw new ArgumentException("El nombre del paciente no puede estar vacío o ser nulo.");
        }

        if (edad <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(edad), "La edad debe ser un número positivo.");
        }

        string sintomaLimpio = string.IsNullOrWhiteSpace(sintoma) ? "Sin síntoma especificado" : sintoma.Trim();
        string telefonoLimpio = string.IsNullOrWhiteSpace(telefono) ? "000-000-0000" : telefono.Trim();

        var nuevoPaciente = new Patient(Guid.NewGuid(), nombre.Trim(), edad, sintomaLimpio, telefonoLimpio);

        await _clienteRepository.AgregarAsync(nuevoPaciente);
        LoggerService.LogInfo($"[ASÍNCRONO] Cliente '{nuevoPaciente.Nombre}' registrado exitosamente con ID {nuevoPaciente.Id}");
        return nuevoPaciente;
    }

    public Patient? BuscarPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre)) return null;
        return _clienteRepository.ObtenerPorNombre(nombre);
    }

    public Pet BuscarMascotaDePaciente(string nombrePaciente, string nombreMascota)
    {
        var paciente = BuscarPorNombre(nombrePaciente);
        if (paciente == null)
        {
            throw new PacienteNoEncontradoException($"No se encontró al paciente '{nombrePaciente}'.");
        }

        var mascota = _mascotaRepository.ObtenerPorClienteId(paciente.Id)
            .FirstOrDefault(m => m.Nombre.Equals(nombreMascota, StringComparison.OrdinalIgnoreCase));

        if (mascota == null)
        {
            throw new MascotaNoEncontradaException(nombreMascota, nombrePaciente);
        }

        return mascota;
    }

    public bool ModificarNombrePaciente(Guid id, string nuevoNombre)
    {
        var paciente = _clienteRepository.ObtenerPorId(id);
        if (paciente != null)
        {
            paciente.Nombre = nuevoNombre;
            return _clienteRepository.Actualizar(paciente);
        }
        return false;
    }

    public bool EliminarPaciente(Guid id)
    {
        return _clienteRepository.Eliminar(id);
    }

    public IEnumerable<Patient> ObtenerPacientesMayoresDe(int edad)
    {
        return _clienteRepository.ObtenerTodos().Where(p => p.Edad > edad);
    }

    public IEnumerable<string> ObtenerSoloNombres()
    {
        return _clienteRepository.ObtenerTodos().Select(p => p.Nombre);
    }

    public IEnumerable<Patient> ObtenerPacientesOrdenadosPorEdad(bool ascendente = true)
    {
        var pacientes = _clienteRepository.ObtenerTodos();
        return ascendente ? pacientes.OrderBy(p => p.Edad) : pacientes.OrderByDescending(p => p.Edad);
    }

    public IEnumerable<IGrouping<string, Pet>> ObtenerMascotasAgrupadasPorEspecie()
    {
        return _mascotaRepository.ObtenerTodas()
            .GroupBy(m => m.Especie);
    }

    public async Task SimularProcesosParalelosClinicaAsync(string nombrePaciente)
    {
        LoggerService.LogInfo($"Iniciando procesos paralelos para cliente '{nombrePaciente}'...");

        Task<string> tareaHistorial = Task.Run(async () =>
        {
            await Task.Delay(1500);
            return "Historial médico cargado desde el repositorio.";
        });

        Task<string> tareaCita = Task.Run(async () =>
        {
            await Task.Delay(800);
            return "Cita agendada en el sistema.";
        });

        Task<string> tareaNotificacion = Task.Run(async () =>
        {
            await Task.Delay(2000);
            return "Notificación enviada al cliente.";
        });

        Task<string> primeraFinalizada = await Task.WhenAny(tareaHistorial, tareaCita, tareaNotificacion);
        string resultadoPrimera = await primeraFinalizada;
        LoggerService.LogInfo($"[Task.WhenAny] Primera tarea completada: {resultadoPrimera}");

        string[] todosLosResultados = await Task.WhenAll(tareaHistorial, tareaCita, tareaNotificacion);
        LoggerService.LogInfo("[Task.WhenAll] Todas las tareas del proceso han finalizado:");
        foreach (var res in todosLosResultados)
        {
            LoggerService.LogInfo($"  - {res}");
        }
    }

    public async Task SimularAtencionConcurrentesMascotasAsync()
    {
        LoggerService.LogInfo("Simulando atención médica simultánea para mascotas guardadas en IMascotaRepository...");

        var mascotas = _mascotaRepository.ObtenerTodas();
        if (!mascotas.Any())
        {
            LoggerService.LogWarning("No hay mascotas registradas para atender.");
            return;
        }

        List<Task<string>> tareasAtencion = mascotas.Select(m => Task.Run(async () =>
        {
            LoggerService.LogInfo($"  -> Atendiendo a {m.Nombre} ({m.Especie})...");
            await Task.Delay(1000);
            return $"Atención de {m.Nombre} finalizada.";
        })).ToList();

        string[] resultadosAtencion = await Task.WhenAll(tareasAtencion);

        LoggerService.LogInfo("Resumen de atención simultánea de mascotas:");
        foreach (var item in resultadosAtencion)
        {
            LoggerService.LogInfo($"  ✓ {item}");
        }
    }
}