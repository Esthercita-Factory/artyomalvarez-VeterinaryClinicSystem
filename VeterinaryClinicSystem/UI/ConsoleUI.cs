using System;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinicSystem.Exceptions;
using VeterinaryClinicSystem.Models;
using VeterinaryClinicSystem.Services;

namespace VeterinaryClinicSystem.UI;

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
        Console.WriteLine("  ---------    SISTEMA VETERINARIO ------------- ");
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
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Nombre no válido. Por favor, escriba un nombre válido: ");
            Console.ResetColor();
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

        Console.Write("Ingrese el número de teléfono: ");
        string telefono = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(telefono))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Error: El teléfono no puede estar vacío. Ingrese un teléfono: ");
            Console.ResetColor();
            telefono = Console.ReadLine()!;
        }

        Console.Write("Ingrese motivo de consulta / síntoma principal: ");
        string sintoma = Console.ReadLine()!;

        try
        {
            Console.WriteLine("[UI] Guardando paciente de forma asíncrona...");
            var nuevoPaciente = await _patientService.RegistrarPacienteAsync(nombre, edad, sintoma, telefono);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[UI] ¡Éxito! Paciente registrado de forma asíncrona con ID {nuevoPaciente.Id}");
            Console.ResetColor();

            // Opción para registrar 0, 1 o varias mascotas para el paciente
            Console.Write("\n¿Desea registrar mascotas para este paciente? (s/n): ");
            string respuesta = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "n";

            while (respuesta == "s" || respuesta == "si" || respuesta == "sí")
            {
                await RegistrarMascotaFormularioAsync(nuevoPaciente.Id);

                Console.Write("\n¿Desea registrar otra mascota para este paciente? (s/n): ");
                respuesta = Console.ReadLine()?.Trim().ToLowerInvariant() ?? "n";
            }
        }
        catch (Exception ex)
        {
            LoggerService.LogError("Error al registrar paciente desde UI Asíncrona", ex);
            Console.WriteLine($"Error de registro: {ex.Message}");
        }
    }

    private async Task RegistrarMascotaFormularioAsync(Guid? clienteId)
    {
        Console.WriteLine("\n  --- REGISTRAR MASCOTA ---");
        Console.Write("  Ingrese el nombre de la mascota: ");
        string nombreMascota = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(nombreMascota))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  Error: El nombre no puede estar vacío: ");
            Console.ResetColor();
            nombreMascota = Console.ReadLine()!;
        }

        Console.Write("  Ingrese la especie (ej. Perro, Gato, Ave): ");
        string especie = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(especie))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  Error: La especie no puede estar vacía: ");
            Console.ResetColor();
            especie = Console.ReadLine()!;
        }

        Console.Write("  Ingrese la raza (opcional, presione Enter para 'Sin Raza'): ");
        string raza = Console.ReadLine()!;
        if (string.IsNullOrWhiteSpace(raza)) raza = "Sin Raza";

        Console.Write("  Ingrese la edad en meses: ");
        int edadMeses;
        while (!int.TryParse(Console.ReadLine(), out edadMeses) || edadMeses < 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  Error: Ingrese un número entero válido (>= 0): ");
            Console.ResetColor();
        }

        Console.Write("  Ingrese el peso en kg (ej. 12.5): ");
        double peso;
        while (!double.TryParse(Console.ReadLine(), out peso) || peso <= 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("  Error: Ingrese un peso válido mayor a 0: ");
            Console.ResetColor();
        }

        Console.Write("  Ingrese motivo de consulta / síntoma de la mascota: ");
        string motivoMascota = Console.ReadLine()!;

        Console.WriteLine("  [UI] Guardando mascota de forma asíncrona...");
        var mascota = await _patientService.RegistrarMascotaAsync(clienteId, nombreMascota, edadMeses, peso, motivoMascota, especie, raza);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"  [UI] ¡Éxito! Mascota '{mascota.Nombre}' ({mascota.Especie}) registrada con ID {mascota.Id} ({mascota.EmitirSonido()})");
        Console.ResetColor();
    }

    private void ListarPacientes()
    {
        Console.WriteLine("\n--- LISTADO DE PACIENTES ---");
        var pacientes = _patientService.ObtenerTodosLosPacientes();

        if (!pacientes.Any())
        {
            Console.WriteLine("No hay pacientes registrados.");
        }
        else
        {
            foreach (var p in pacientes)
            {
                Console.WriteLine(p.ObtenerInformacion());
                if (p.Mascotas.Any())
                {
                    Console.WriteLine("  Mascotas:");
                    foreach (var m in p.Mascotas)
                    {
                        Console.WriteLine($"    - {m.Nombre} ({m.Especie}, Raza: {m.Raza}, Edad: {m.EdadEnMeses} meses, Peso: {m.Peso}kg, Motivo: {m.MotivoConsulta}) | Sonido: {m.EmitirSonido()}");
                    }
                }
                else
                {
                    Console.WriteLine("  (Sin mascotas registradas)");
                }
            }
        }

        var sinDueno = _patientService.ObtenerMascotasSinDueno();
        if (sinDueno.Any())
        {
            Console.WriteLine("\n--- MASCOTAS SIN DUEÑO / EN RESCATE O ADOPCIÓN ---");
            foreach (var m in sinDueno)
            {
                Console.WriteLine($"  - {m.Nombre} ({m.Especie}, Raza: {m.Raza}, Edad: {m.EdadEnMeses} meses, Peso: {m.Peso}kg, Motivo: {m.MotivoConsulta}) | Sonido: {m.EmitirSonido()}");
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

            if (paciente.Mascotas.Any())
            {
                Console.WriteLine("  Mascotas asociadas:");
                foreach (var m in paciente.Mascotas)
                {
                    Console.WriteLine($"    - {m.Nombre} ({m.Especie}, Raza: {m.Raza}, Edad: {m.EdadEnMeses} meses, Peso: {m.Peso}kg, Motivo: {m.MotivoConsulta}) | Sonido: {m.EmitirSonido()}");
                }
            }
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
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No hay pacientes con mascotas registradas para demostrar la atención del servicio.");
            Console.ResetColor();
        }
    }

    private void DemostrarInterfacesYNotificaciones()
    {
        Console.WriteLine("\n=== DEMOSTRACIÓN DE INTERFACES MÚLTIPLES ===");
        var paciente = _patientService.ObtenerTodosLosPacientes().FirstOrDefault();
        if (paciente != null)
        {
            // 1. Contrato IRegistrable (Heredado de Persona)
            IRegistrable registrable = paciente;
            Console.WriteLine("1. Demostración de contrato IRegistrable:");
            Console.WriteLine($"   -> Información obtenida vía interfaz: {registrable.ObtenerInformacion()}");

            // 2. Contrato INotificable (Implementado directamente por Patient)
            INotificable notificable = paciente;
            Console.WriteLine("2. Demostración de contrato INotificable:");
            Console.Write("   -> ");
            notificable.EnviarNotificacion("Recordatorio: Su mascota tiene cita médica programada para mañana.");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("No hay pacientes registrados para enviar notificaciones.");
            Console.ResetColor();
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
