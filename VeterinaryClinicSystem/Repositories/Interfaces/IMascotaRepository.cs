using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinicSystem.Models;

namespace VeterinaryClinicSystem.Repositories;

/// <summary>
/// Contrato del Repositorio de Mascotas.
/// Responsabilidad única: Abstraer el almacenamiento y acceso a datos de las mascotas de la clínica.
/// </summary>
public interface IMascotaRepository
{
    void Agregar(Pet mascota, Guid clienteId);
    Task AgregarAsync(Pet mascota, Guid clienteId);
    IReadOnlyList<Pet> ObtenerTodas();
    IReadOnlyList<Pet> ObtenerPorClienteId(Guid clienteId);
    Pet? ObtenerPorId(Guid id);
    bool Eliminar(Guid id);
}
