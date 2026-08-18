using System;
using System.Collections.Generic;

namespace VenterinaryClinicSystem.Models;

// Patient hereda de Persona e implementa la abstracción
public class Patient : Persona
{
    public string Sintoma { get; set; }
    public List<Pet> Mascotas { get; set; }

    public Patient(Guid id, string nombre, int edad, string sintoma, string telefono = "000-000-0000") 
        : base(id, nombre, edad, telefono)
    {
        Sintoma = sintoma;
        Mascotas = new List<Pet>();
    }

    public override void MostrarInformacion()
    {
        Console.WriteLine($"[PACIENTE] ID: {Id} | Nombre: {Nombre} | Edad: {Edad} | Teléfono: {Telefono} | Síntoma: {Sintoma}");
    }
}