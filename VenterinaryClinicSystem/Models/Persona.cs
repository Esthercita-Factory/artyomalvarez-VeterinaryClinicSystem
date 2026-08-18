namespace VenterinaryClinicSystem.Models;

public abstract class Persona : IRegistrable
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    
    // Encapsulación de dato sensible: Teléfono
    private string _telefono = string.Empty;
    public string Telefono 
    { 
        get => _telefono; 
        set => _telefono = value; 
    }

    public Persona(Guid id, string nombre, int edad, string telefono)
    {
        Id = id;
        Nombre = nombre;
        Edad = edad;
        Telefono = telefono;
    }

    public virtual void Registrar()
    {
        Console.WriteLine($"Registrando a la persona: {Nombre}");
    }

    public abstract void MostrarInformacion();
}
