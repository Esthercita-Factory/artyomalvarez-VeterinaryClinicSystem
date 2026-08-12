namespace VenterinaryClinicSystem.Models;

public class Pet
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public int EdadEnMeses{ get; set; }
    public double Peso { get; set; }
    public string Sintoma { get; set; }
    public string Especie { get; set; }

    public Pet(int id, string nombre, int edadEnMeses, double peso, string sintoma, string specie)
    {
        Id = id;
        Nombre = nombre;
        EdadEnMeses = edadEnMeses;
        Peso = peso;
        Sintoma = sintoma;
        Especie = specie;
    }


    public void Prensentacion(int dia)
    {
        Console.WriteLine($"el animal {Id} se esta presentando");
    }
     
}