using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinicSystem.Models;

namespace VeterinaryClinicSystem.Services;

/// <summary>
/// Interfaz IPatientService con soporte asíncrono (Historia M5.3S5).
/// </summary>
public interface IPatientService
{
    IReadOnlyList<Patient> ObtenerTodosLosPacientes();
    Patient RegistrarPaciente(string? nombre, int edad, string? sintoma, string? telefono = "000-000-0000");
    Task<Patient> RegistrarPacienteAsync(string? nombre, int edad, string? sintoma, string? telefono = "000-000-0000");
    Patient? BuscarPorNombre(string nombre);
    Pet BuscarMascotaDePaciente(string nombrePaciente, string nombreMascota);
    bool ModificarNombrePaciente(Guid id, string nuevoNombre);
    bool EliminarPaciente(Guid id);
    IEnumerable<Patient> ObtenerPacientesMayoresDe(int edad);
    IEnumerable<string> ObtenerSoloNombres();
    IEnumerable<Patient> ObtenerPacientesOrdenadosPorEdad(bool ascendente = true);
    IEnumerable<IGrouping<string, Pet>> ObtenerMascotasAgrupadasPorEspecie();

    // TASK 3 y TASK 4: Gestión de tareas paralelas con Task
    Task SimularProcesosParalelosClinicaAsync(string nombrePaciente);
    Task SimularAtencionConcurrentesMascotasAsync();
}
