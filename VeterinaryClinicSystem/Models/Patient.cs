using System;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryClinicSystem.Models;

public class Patient : Persona, INotificable
{
    private readonly List<Pet> _mascotas = new List<Pet>();

    public string MotivoConsulta { get; set; }
    
    // Propiedad compatible con el Diagrama UML (Historia M5.3S3)
    public string Sintoma 
    { 
        get => MotivoConsulta; 
        set => MotivoConsulta = value; 
    }
    
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
        if (!_mascotas.Any(m => m.Id == mascota.Id))
        {
            mascota.Dueno = this;
            _mascotas.Add(mascota);
        }
    }

    public bool RemoverMascota(Guid mascotaId)
    {
        var mascota = _mascotas.Find(m => m.Id == mascotaId);
        if (mascota != null)
        {
            mascota.Dueno = null;
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
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[NOTIFICACIÓN ENVIADA] Para: {Nombre} (Tel: {Telefono}) -> \"{mensaje}\"");
        Console.ResetColor();
    }
}