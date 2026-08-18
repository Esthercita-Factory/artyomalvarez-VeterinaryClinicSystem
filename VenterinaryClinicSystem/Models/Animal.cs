namespace VenterinaryClinicSystem.Models;

public abstract class Animal : IRegistrable
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int EdadEnMeses { get; set; }
    public string Especie { get; set; }

    public Animal(int id, string nombre, int edadEnMeses, string especie)
    {
        Id = id;
        Nombre = nombre;
        EdadEnMeses = edadEnMeses;
        Especie = especie;
    }

    // Polimorfismo: método virtual para que las subclases lo sobrescriban (override)
    public virtual string EmitirSonido()
    {
        return "El animal hace un sonido indeterminado.";
    }

    public virtual void Registrar()
    {
        Console.WriteLine($"Registrando al animal: {Nombre} ({Especie})");
    }

    public abstract void MostrarInformacion();
}
