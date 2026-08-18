namespace VenterinaryClinicSystem.Models;

public class Trabajador : Persona
{
    public string Cargo { get; set; }
    public string Especialidad { get; set; }

    public Trabajador(Guid id, string nombre, int edad, string telefono, string cargo, string especialidad) 
        : base(id, nombre, edad, telefono)
    {
        Cargo = cargo;
        Especialidad = especialidad;
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"[TRABAJADOR] {Nombre} | Cargo: {Cargo} | Especialidad: {Especialidad} | Teléfono: {Telefono}");
    }
}
