using System;
using System.Collections.Generic;

namespace VenterinaryClinicSystem.Models;

/// <summary>
/// TASK 1: Justificación de diseño entre Clases Abstractas e Interfaces.
/// - Persona (Clase Abstracta): Se utiliza porque Patient comparte estado, propiedades base (Id, Nombre, Edad, Telefono) 
///   y lógica genérica con otras personas (ej. Trabajador). C# permite herencia única de clases.
/// - IRegistrable e INotificable (Interfaces): Se utilizan para dotar a Patient de capacidades polimórficas múltiples.
///   Patient implementa varias interfaces simultáneamente (TASK 3), logrando desacoplamiento y mayor flexibilidad.
/// </summary>
public class Patient : Persona, INotificable
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

    // TASK 3: Implementación de la interfaz INotificable
    public void EnviarNotificacion(string mensaje)
    {
        Console.WriteLine($"\n[NOTIFICACIÓN ENVIADA] Para Paciente '{Nombre}' (Tel: {Telefono}):");
        Console.WriteLine($"   \"{mensaje}\"");
    }
}