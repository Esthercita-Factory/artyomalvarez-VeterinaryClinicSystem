using VenterinaryClinicSystem.Models;

List<Patient> pacientes = new List<Patient>();
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n--- MENU DE PACIENTES ---");
    Console.WriteLine("1. Registrar paciente");
    Console.WriteLine("2. Listar pacientes");
    Console.WriteLine("3. Buscar paciente");
    Console.WriteLine("4. Salir");
    Console.Write("Seleccione una opción: ");

    string opcion = Console.ReadLine()!;

    switch (opcion)
    {
        case "1":
            Patient.RegistrarPaciente(pacientes);
            break;
        case "2":
            Patient.ListarPacientes(pacientes);
            break;
        case "3":
            Console.Write("Ingrese el nombre del paciente a buscar: ");
            string nombre = Console.ReadLine()!;
            Patient.BuscarPacientes(pacientes, nombre);
            break;
        case "4":
            salir = true;
            Console.WriteLine("¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}