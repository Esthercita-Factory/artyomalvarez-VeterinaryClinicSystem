using System;
using System.Linq;
using System.Threading.Tasks;
using VenterinaryClinicSystem.Exceptions;
using VenterinaryClinicSystem.Models;
using VenterinaryClinicSystem.Services;

namespace VenterinaryClinicSystem.UI;

/// <summary>
/// Capa UI (Presentación):
/// Incorpora el menú interactivo para ejecutar métodos asíncronos y procesos paralelos sin bloquear el hilo principal.
/// Convenciones: Métodos asíncronos sufijados con "Async", PascalCase en métodos/clases, camelCase en variables.
/// </summary>
public class ConsoleUI
{
    private readonly IPatientService _patientService;

    public ConsoleUI(IPatientService patientService)
    {
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
    }

    public async Task IniciarAsync()
    {
        bool salir = false;

        while (!salir)
        {
            MostrarMenu();
            string opcion = Console.ReadLine()!;

            switch (opcion)
            {
                case "1":
                    await RegistrarPacienteAsync();
                    break;
                case "2":
                    ListarPacientes();
                    break;
                case "3":
                    BuscarPaciente();
                    break;
                case "4":
                    DemostrarLinq();
                    break;
                case "5":
                    DemostrarPooServicios();
                    break;
                case "6":
                    DemostrarInterfacesYNotificaciones();
                    break;
                case "7":
                    DemostrarExcepcionesYLogging();
                    break;
                case "8":
                    await DemostrarAsincroniaYTareasParalelasAsync();
                    break;
                case "9":
                    salir = true;
                    Console.WriteLine("¡Gracias por usar el sistema de la clínica veterinaria!");
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    Console.ResetColor();
                    break;
            }
        }
    }

    private void MostrarMenu()
    {
        Console.WriteLine("\n==============================================");
        Console.WriteLine("    SISTEMA VETERINARIO - ASINCRONÍA Y TAREAS");
        Console.WriteLine("==============================================");
        Console.WriteLine("1. Registrar nuevo paciente (Asíncrono - Task.Delay)");
        Console.WriteLine("2. Listar pacientes y sus mascotas");
        Console.WriteLine("3. Buscar paciente por nombre");
        Console.WriteLine("4. Demostrar consultas LINQ");
        Console.WriteLine("5. Demostrar POO (Herencia y Polimorfismo)");
        Console.WriteLine("6. Demostrar Interfaces Múltiples (IRegistrable, INotificable)");
        Console.WriteLine("7. Demostrar Manejo de Excepciones y Logging");
        Console.WriteLine("8. Demostrar Programación Asíncrona (Task.WhenAll vs Task.WhenAny)");
        Console.WriteLine("9. Salir");
        Console.Write("Seleccione una opción: ");
    }

    // TASK 2 y 5: Invocación de método asíncrono usando await sin bloquear el hilo principal
    private async Task RegistrarPacienteAsync()
    {
        Console.WriteLine("\n--- REGISTRAR PACIENTE ASÍNCRONO (Task.Delay) ---");
        
        Console.Write("Ingrese el nombre del paciente: ");
        string nombre = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(nombre) || !nombre.Any(char.IsLetter))
        {
            Console.Write("Nombre no válido. Por favor, escriba un nombre válido: ");
            nombre = Console.ReadLine()!;
        }

        Console.Write("Ingrese la edad: ");
        int edad;
        while (!int.TryParse(Console.ReadLine(), out edad) || edad <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error: Debe ingresar un número entero positivo para la edad: ");
            Console.ResetColor();
        }

        Console.Write("Ingrese síntoma principal: ");
        string sintoma = Console.ReadLine()!;

        try
        {
            Console.WriteLine("[UI] Guardando paciente de forma asíncrona...");
            var nuevoPaciente = await _patientService.RegistrarPacienteAsync(nombre, edad, sintoma);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[UI] ¡Éxito! Paciente registrado de forma asíncrona con ID {nuevoPaciente.Id}");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            LoggerService.LogError("Error al registrar paciente desde UI Asíncrona", ex);
            Console.WriteLine($"Error de registro: {ex.Message}");
        }
    }

    private void ListarPacientes()
    {
        Console.WriteLine("\n--- LISTADO DE PACIENTES ---");
        var pacientes = _patientService.ObtenerTodosLosPacientes();

        if (!pacientes.Any())
        {
            Console.WriteLine("No hay pacientes registrados.");
            return;
        }

        foreach (var p in pacientes)
        {
            Console.WriteLine(p.ObtenerInformacion());
            if (p.Mascotas.Any())
            {
                Console.WriteLine("  Mascotas:");
                foreach (var m in p.Mascotas)
                {
                    Console.WriteLine($"    - {m.Nombre} ({m.Especie}, Raza: {m.Raza}) | Sonido: {m.EmitirSonido()}");
                }
            }
        }
    }

    private void BuscarPaciente()
    {
        Console.Write("\nIngrese el nombre del paciente a buscar: ");
        string nombre = Console.ReadLine()!;
        var paciente = _patientService.BuscarPorNombre(nombre);

        if (paciente != null)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(paciente.ObtenerInformacion());
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"No se encontró al paciente '{nombre}'.");
            Console.ResetColor();
        }
    }

    private void DemostrarLinq()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DE CONSULTAS LINQ ===");
        Console.WriteLine("1. Nombres de pacientes:");
        foreach (var n in _patientService.ObtenerSoloNombres())
        {
            Console.WriteLine($"   - {n}");
        }

        Console.WriteLine("\n2. Mascotas agrupadas por especie:");
        foreach (var grupo in _patientService.ObtenerMascotasAgrupadasPorEspecie())
        {
            Console.WriteLine($"   Especie: {grupo.Key}");
            foreach (var m in grupo)
            {
                Console.WriteLine($"     * {m.Nombre}");
            }
        }
    }

    private void DemostrarPooServicios()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DE POO Y SERVICIOS ===");
        var vet = new Trabajador(Guid.NewGuid(), "Dr. Roberto", 45, "555-4321", "Veterinario", "Medicina Interna");
        var paciente = _patientService.ObtenerTodosLosPacientes().FirstOrDefault();

        if (paciente != null && paciente.Mascotas.Any())
        {
            ServicioVeterinario consulta = new ConsultaGeneral(vet);
            consulta.Atender(paciente, paciente.Mascotas.First());
        }
    }

    private void DemostrarInterfacesYNotificaciones()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DE INTERFACES Y NOTIFICACIONES ===");
        var paciente = _patientService.ObtenerTodosLosPacientes().FirstOrDefault();
        if (paciente != null)
        {
            INotificable notificable = paciente;
            notificable.EnviarNotificacion("Recordatorio: Su mascota tiene cita programada mañana.");
        }
    }

    private void DemostrarExcepcionesYLogging()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DE EXCEPCIONES Y LOGGING ===");
        try
        {
            _patientService.BuscarMascotaDePaciente("PacienteInexistente", "Firulais");
        }
        catch (PacienteNoEncontradoException ex)
        {
            LoggerService.LogError("Excepción personalizada de paciente capturada", ex);
        }

        try
        {
            int cero = 0;
            int resultado = 10 / cero;
        }
        catch (DivideByZeroException ex)
        {
            LoggerService.LogError("Excepción de división por cero capturada", ex);
        }
    }

    // TASK 3 y 4: Demostración de Programación Asíncrona en Paralelo
    private async Task DemostrarAsincroniaYTareasParalelasAsync()
    {
        Console.WriteLine("\n=== HISTORIA M5.3S5: DEMOSTRACIÓN PROGRAMACIÓN ASÍNCRONA ===");
        Console.WriteLine("1. Procesos paralelos de la clínica (Task.WhenAny vs Task.WhenAll):");
        await _patientService.SimularProcesosParalelosClinicaAsync("Carlos Pérez");

        Console.WriteLine("\n2. Atención concurrente simulada para múltiples mascotas (Task.WhenAll):");
        await _patientService.SimularAtencionConcurrentesMascotasAsync();
    }
}
