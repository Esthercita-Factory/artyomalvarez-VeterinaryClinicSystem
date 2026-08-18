using VenterinaryClinicSystem.Models;

namespace VenterinaryClinicSystem.Services;

// Clase abstracta ServicioVeterinario (Abstracción)
public abstract class ServicioVeterinario
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

    // Método abstracto que debe ser implementado por cada subclase (Consulta, Vacunación)
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
        Console.WriteLine($"\n--- ATENDIENDO CONSULTA GENERAL ---");
        Console.WriteLine($"Atendido por: Dr/a. {AtendidoPor.Nombre} ({AtendidoPor.Cargo})");
        Console.WriteLine($"Paciente: {paciente.Nombre} | Mascota: {mascota.Nombre} ({mascota.Especie})");
        Console.WriteLine($"Sonido de la mascota: {mascota.EmitirSonido()}");
        Console.WriteLine($"Diagnóstico preliminar basado en síntoma: {mascota.Sintoma}");
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
        Console.WriteLine($"\n--- ATENDIENDO SERVICIO DE VACUNACIÓN ---");
        Console.WriteLine($"Atendido por: {AtendidoPor.Nombre} ({AtendidoPor.Especialidad})");
        Console.WriteLine($"Aplicando vacuna '{TipoVacuna}' a la mascota {mascota.Nombre} del paciente {paciente.Nombre}.");
        Console.WriteLine($"Reacción de {mascota.Nombre}: {mascota.EmitirSonido()}");
        Console.WriteLine($"Costo del servicio: ${Costo}");
    }
}
