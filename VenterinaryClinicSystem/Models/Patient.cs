namespace VenterinaryClinicSystem.Models;

public class Patient
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public byte Edad { get; set; }
    public string Sintomas { get; set; }

    public Patient(Guid id, string nombre, byte edad, string sintomas)
    {
        Id = id;
        Nombre = nombre;
        Edad = edad;
        Sintomas = sintomas;
    }

    public static void RegistrarPaciente(List<Patient> patients)
    {
        // 1. Validar nombre vacío
        Console.Write("Ingrese el nombre: ");
        string nombre = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(nombre))
        {
            Console.Write("El nombre no puede estar vacío. Ingrese el nombre: ");
            nombre = Console.ReadLine()!;
        }

        // 2. Validar edad inválida o fuera de rango
        byte edad = 0;
        bool edadValida = false;
        while (!edadValida)
        {
            Console.Write("Ingrese la edad: ");
            try
            {
                edad = byte.Parse(Console.ReadLine()!);
                if (edad == 0)
                {
                    Console.WriteLine("La edad debe ser mayor a 0.");
                }
                else
                {
                    edadValida = true;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Ingrese una edad válida (solo números).");
            }
            catch (OverflowException)
            {
                Console.WriteLine("La edad está fuera del rango (1 a 255).");
            }
        }
        
        // 3. Validar síntomas vacíos
        Console.Write("Ingrese los síntomas: ");
        string sintomas = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(sintomas))
        {
            Console.Write("Los síntomas no pueden estar vacíos. Ingrese los síntomas: ");
            sintomas = Console.ReadLine()!;
        }

        // Crear y guardar el paciente
        Patient nuevoPaciente = new Patient(Guid.NewGuid(), nombre, edad, sintomas);
        patients.Add(nuevoPaciente);
        Console.WriteLine("\n¡Paciente registrado con éxito!");
    }

    public static void ListarPacientes(List<Patient> patients)
    {
        if (patients.Count == 0)
        {
            Console.WriteLine("\nNo hay pacientes registrados.");
            return;
        }

        foreach (var patient in patients)
        {
            Console.WriteLine($"\nNombre: {patient.Nombre}");
            Console.WriteLine($"Edad: {patient.Edad}");
            Console.WriteLine($"Síntomas: {patient.Sintomas}");
        }
    }
    
    public static void BuscarPacientes(List<Patient> patients, string nombre)
    {
        bool encontrado = false;
        foreach (var patient in patients)
        {
            if (patient.Nombre.ToLower() == nombre.ToLower())
            {
                Console.WriteLine($"\nNombre: {patient.Nombre}");
                Console.WriteLine($"Edad: {patient.Edad}");
                Console.WriteLine($"Síntomas: {patient.Sintomas}");
                encontrado = true;
                break;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("Paciente No encontrado");
        }
    }   
}