using System;

namespace VenterinaryClinicSystem.Exceptions;

/// <summary>
/// TASK 5: Excepción personalizada de dominio para cuando no se encuentra una mascota buscada.
/// </summary>
public class MascotaNoEncontradaException : Exception
{
    public MascotaNoEncontradaException(string mensaje) : base(mensaje)
    {
    }

    public MascotaNoEncontradaException(string nombreMascota, string nombrePaciente) 
        : base($"No se encontró la mascota '{nombreMascota}' perteneciente al paciente '{nombrePaciente}'.")
    {
    }
}

/// <summary>
/// TASK 5: Excepción personalizada de dominio para cuando no se encuentra un paciente en el sistema.
/// </summary>
public class PacienteNoEncontradoException : Exception
{
    public PacienteNoEncontradoException(string mensaje) : base(mensaje)
    {
    }

    public PacienteNoEncontradoException(Guid id) 
        : base($"No se encontró ningún paciente registrado con el ID: {id}")
    {
    }
}
