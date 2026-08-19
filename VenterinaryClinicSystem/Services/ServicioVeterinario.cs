using VenterinaryClinicSystem.Models;

namespace VenterinaryClinicSystem.Services;

/// <summary>
/// TASK 1 y TASK 2: Justificación e implementación de IAtendible.
/// - ServicioVeterinario es una clase abstracta porque comparte propiedades del servicio (NombreServicio, Costo, AtendidoPor).
/// - Implementa la interfaz IAtendible para garantizar que cualquier subclase de servicio pueda ser tratada de forma polimórfica mediante el contrato Atender().
/// </summary>
public abstract class ServicioVeterinario : IAtendible
{
    public string NombreServicio { get; set; }
    public decimal Costo { get; set; }
    public Trabajador AtendidoPor { get; set; }

    public ServicioVeterinario(string nombreServicio, decimal costo, Trabajador atendidoPor)
    {
        NombreServicio = nombreServicio;
        Costo = costo;
        AtendidoPor = atendidoPor;
    }

    // Método abstracto de ServicioVeterinario e implementación del contrato IAtendible
    public abstract void Atender(Patient paciente, Pet mascota);
}

// Subclase ConsultaGeneral
public class ConsultaGeneral : ServicioVeterinario
{
    public ConsultaGeneral(Trabajador veterinario) 
        : base("Consulta General", 50.00m, veterinario)
    {
    }

    public override void Atender(Patient paciente, Pet mascota)
    {
        Console.WriteLine($"\n--- ATENDIENDO CONSULTA GENERAL (Vía IAtendible) ---");
        Console.WriteLine($"Atendido por: Dr/a. {AtendidoPor.Nombre} ({AtendidoPor.Cargo})");
        Console.WriteLine($"Paciente: {paciente.Nombre} | Mascota: {mascota.Nombre} ({mascota.Especie})");
        Console.WriteLine($"Sonido de la mascota: {mascota.EmitirSonido()}");
        Console.WriteLine($"Diagnóstico preliminar basado en motivo de consulta: {mascota.MotivoConsulta}");
        Console.WriteLine($"Costo del servicio: ${Costo}");
    }
}

// Subclase Vacunacion
public class Vacunacion : ServicioVeterinario
{
    public string TipoVacuna { get; set; }

    public Vacunacion(Trabajador veterinario, string tipoVacuna) 
        : base("Vacunación", 35.00m, veterinario)
    {
        TipoVacuna = tipoVacuna;
    }

    public override void Atender(Patient paciente, Pet mascota)
    {
        Console.WriteLine($"\n--- ATENDIENDO SERVICIO DE VACUNACIÓN (Vía IAtendible) ---");
        Console.WriteLine($"Atendido por: {AtendidoPor.Nombre} ({AtendidoPor.Especialidad})");
        Console.WriteLine($"Aplicando vacuna '{TipoVacuna}' a la mascota {mascota.Nombre} del paciente {paciente.Nombre}.");
        Console.WriteLine($"Reacción de {mascota.Nombre}: {mascota.EmitirSonido()}");
        Console.WriteLine($"Costo del servicio: ${Costo}");
    }
}
