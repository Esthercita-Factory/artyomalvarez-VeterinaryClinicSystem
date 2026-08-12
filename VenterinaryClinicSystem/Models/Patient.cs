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

    public void RegistrarPaciente(List<Patient> patients)
    {

    }

    public void ListarPacientes(List<Patient> patients)
    {
        foreach (var patient in patients)
        {
            Console.WriteLine(patient.Nombre);
            Console.WriteLine(patient.Edad);
            Console.WriteLine(patient.Sintomas);
        }
    }
    
    public void BuscarPacientes(List<Patient> patients, string nombre)
    {
        bool encontrado = false;
        foreach (var patient in patients)
        {
            if (patient.Nombre == nombre)
            {
                Console.WriteLine(patient.Nombre);
                break;
            }
        }

        if (!encontrado)
        {
            Console.WriteLine("Paciente No encontrado");
        }
    }   
    
 
    
}