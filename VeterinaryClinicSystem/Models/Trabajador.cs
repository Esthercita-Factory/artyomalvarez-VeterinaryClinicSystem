using System;
using System.Collections.Generic;

namespace VeterinaryClinicSystem.Models;

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

    public override string ObtenerInformacion()
    {
        return $"[TRABAJADOR] ID: {Id} | Nombre: {Nombre} | Edad: {Edad} | Teléfono: {Telefono} | Cargo: {Cargo} | Especialidad: {Especialidad}";
    }
}
