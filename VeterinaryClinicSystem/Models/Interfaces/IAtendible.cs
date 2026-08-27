using VeterinaryClinicSystem.Models;

namespace VeterinaryClinicSystem.Models;

/// <summary>
/// TASK 2: Interfaz IAtendible.
/// Define el contrato que cualquier servicio u operador debe cumplir para atender a un paciente y su mascota.
/// A diferencia de una clase abstracta, permite aplicar este comportamiento a cualquier entidad sin forzar una jerarquía de herencia.
/// </summary>
public interface IAtendible
{
    void Atender(Patient paciente, Pet mascota);
}
