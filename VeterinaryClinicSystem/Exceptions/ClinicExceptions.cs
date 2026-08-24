using System;

namespace VeterinaryClinicSystem.Exceptions;

/// <summary>
/// TASK 5: Excepción personalizada de dominio para cuando no se encuentra una mascota buscada.
/// </summary>
public class MascotaNoEncontradaException : Exception
{
    public MascotaNoEncontradaException() : base("No se encontró la mascota especificada en el sistema.")
    {
    }

    public MascotaNoEncontradaException(string mensaje) : base(mensaje)
    {
    }

    public MascotaNoEncontradaException(string mensaje, Exception innerException) : base(mensaje, innerException)
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
    public PacienteNoEncontradoException() : base("No se encontró el paciente especificado en el sistema.")
    {
    }

    public PacienteNoEncontradoException(string mensaje) : base(mensaje)
    {
    }

    public PacienteNoEncontradoException(string mensaje, Exception innerException) : base(mensaje, innerException)
    {
    }

    public PacienteNoEncontradoException(Guid id) 
        : base($"No se encontró ningún paciente registrado con el ID: {id}")
    {
    }
}
