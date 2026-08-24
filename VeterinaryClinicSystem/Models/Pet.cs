using System;

namespace VeterinaryClinicSystem.Models;

public class Pet : Animal
{
    public double Peso { get; set; }
    public string MotivoConsulta { get; set; }
    public string Raza { get; set; }

    public Pet(Guid id, string nombre, int edadEnMeses, double peso, string motivoConsulta, string especie, string raza = "Sin Raza")
        : base(id, nombre, edadEnMeses, especie)
    {
        Peso = peso;
        MotivoConsulta = motivoConsulta;
        Raza = raza;
    }

    public override string EmitirSonido()
    {
        if (Especie.Equals("Perro", StringComparison.OrdinalIgnoreCase))
            return "¡Guau Guau!";
        if (Especie.Equals("Gato", StringComparison.OrdinalIgnoreCase))
            return "¡Miau Miau!";
        if (Especie.Equals("Canario", StringComparison.OrdinalIgnoreCase) || Especie.Equals("Ave", StringComparison.OrdinalIgnoreCase))
            return "¡Pío Pío!";
        
        return "¡Sonido animal!";
    }

    public override string ObtenerInformacion()
    {
        return $"[MASCOTA] ID: {Id} | Nombre: {Nombre} | Especie: {Especie} | Raza: {Raza} | Edad: {EdadEnMeses} meses | Peso: {Peso}kg";
    }
}