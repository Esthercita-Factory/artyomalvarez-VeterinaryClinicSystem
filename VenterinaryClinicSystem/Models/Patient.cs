using System.Collections.Generic;

namespace VenterinaryClinicSystem.Models;

public class Patient
{
    // Propiedades automáticas básicas
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public string Sintoma { get; set; }

    // Relación con su lista de mascotas
    public List<Pet> Mascotas { get; set; }

    public Patient(Guid id, string nombre, int edad, string sintoma)
    {
        Id = id;
        Nombre = nombre;
        Edad = edad;
        Sintoma = sintoma;
        Mascotas = new List<Pet>();
    }
}