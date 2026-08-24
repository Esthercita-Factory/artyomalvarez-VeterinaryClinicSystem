using System;
using System.Collections.Generic;

namespace VeterinaryClinicSystem.Models;

public class Patient : Persona, INotificable
{
    private readonly List<Pet> _mascotas = new List<Pet>();

    public string MotivoConsulta { get; set; }
    
    // Encapsulación de Colección: Exposición de lectura IReadOnlyCollection
    public IReadOnlyCollection<Pet> Mascotas => _mascotas.AsReadOnly();

    public Patient(Guid id, string nombre, int edad, string motivoConsulta, string telefono = "000-000-0000") 
        : base(id, nombre, edad, telefono)
    {
        MotivoConsulta = motivoConsulta;
    }

    public void AgregarMascota(Pet mascota)
    {
        if (mascota == null) throw new ArgumentNullException(nameof(mascota));
        _mascotas.Add(mascota);
    }

    public bool RemoverMascota(Guid mascotaId)
    {
        var mascota = _mascotas.Find(m => m.Id == mascotaId);
        if (mascota != null)
        {
            _mascotas.Remove(mascota);
            return true;
        }
        return false;
    }

    public override string ObtenerInformacion()
    {
        return $"[PACIENTE] ID: {Id} | Nombre: {Nombre} | Edad: {Edad} | Teléfono: {Telefono} | Motivo Consulta: {MotivoConsulta}";
    }

    public void EnviarNotificacion(string mensaje)
    {
        // Genera la representación de notificación sin acoplarse a UI directas
    }
}