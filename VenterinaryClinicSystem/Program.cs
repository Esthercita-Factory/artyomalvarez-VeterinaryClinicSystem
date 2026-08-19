using VenterinaryClinicSystem.Services;

PatientService patientService = new PatientService();
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n--- MENU CLINICA VETERINARIA ---");
    Console.WriteLine("1. Registrar paciente");
    Console.WriteLine("2. Listar pacientes");
    Console.WriteLine("3. Buscar paciente (LINQ FirstOrDefault)");
    Console.WriteLine("4. Demostrar consultas LINQ básicas (Task 2)");
    Console.WriteLine("5. Demostrar consultas LINQ encadenadas (Task 4)");
    Console.WriteLine("6. Demostrar problemas prácticos LINQ (Task 5)");
    Console.WriteLine("7. Demostrar POO: Herencia, Polimorfismo y Servicios (M5.3S3)");
    Console.WriteLine("8. Demostrar Interfaces y Notificaciones (M5.3S4 - Tasks 1, 2, 3)");
    Console.WriteLine("9. Demostrar Excepciones, Depuración y Logging (M5.3S4 - Tasks 4, 5, 6)");
    Console.WriteLine("10. Salir");
    Console.Write("Seleccione una opción: ");

    string opcion = Console.ReadLine()!;

    switch (opcion)
    {
        case "1":
            patientService.RegistrarPaciente();
            break;
        case "2":
            patientService.ListarPacientes();
            break;
        case "3":
            patientService.BuscarPaciente();
            break;
        case "4":
            patientService.DemostrarSintaxisLinq();
            break;
        case "5":
            patientService.DemostrarConsultasEncadenadas();
            break;
        case "6":
            patientService.DemostrarProblemasPracticos();
            break;
        case "7":
            patientService.DemostrarPooHerenciaYPolimorfismo();
            break;
        case "8":
            patientService.DemostrarInterfacesYNotificaciones();
            break;
        case "9":
            patientService.DemostrarManejoExcepcionesYLogging();
            break;
        case "10":
            salir = true;
            Console.WriteLine("¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}