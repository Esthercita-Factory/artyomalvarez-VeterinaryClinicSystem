using VenterinaryClinicSystem.Models;

List<Patient> pacientes = new List<Patient>();
Patient gestorPacientes = new Patient(Guid.NewGuid(), "", 0, "");
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
            gestorPacientes.RegistrarPaciente(pacientes);
            break;
        case "2":
            gestorPacientes.ListarPacientes(pacientes);
            break;
        case "3":
            Console.Write("Ingrese el nombre del paciente a buscar: ");
            string nombre = Console.ReadLine()!;
            gestorPacientes.BuscarPacientes(pacientes, nombre);
            break;
        case "4":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}