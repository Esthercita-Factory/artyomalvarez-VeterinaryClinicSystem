using System;

namespace VenterinaryClinicSystem.Models;

public class Pet : Animal
{
    public double Peso { get; set; }
    public string Sintoma { get; set; }
    public string Raza { get; set; }

    public Pet(Guid id, string nombre, int edadEnMeses, double peso, string sintoma, string especie, string raza = "Sin Raza")
        : base(id, nombre, edadEnMeses, especie)
    {
        Peso = peso;
        Sintoma = sintoma;
        Raza = raza;
    }

    public override string EmitirSonido()
    {
        if (Especie.Equals("Perro", StringComparison.OrdinalIgnoreCase))
            return "¡Guau Guau!";
        if (Especie.Equals("Gato", StringComparison.OrdinalIgnoreCase))
            return "¡Miau Miau!";
        
        return "¡Sonido animal!";
    }

    public override string ObtenerInformacion()
    {
        return $"[MASCOTA] ID: {Id} | Nombre: {Nombre} | Especie: {Especie} | Raza: {Raza} | Edad: {EdadEnMeses} meses | Peso: {Peso}kg";
    }
}