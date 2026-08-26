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
    void Agregar(Pet mascota, Guid? clienteId = null);
    Task AgregarAsync(Pet mascota, Guid? clienteId = null);
    IReadOnlyList<Pet> ObtenerTodas();
    IReadOnlyList<Pet> ObtenerPorClienteId(Guid clienteId);
    IReadOnlyList<Pet> ObtenerSinDueno();
    Pet? ObtenerPorId(Guid id);
    bool DesvincularMascotasDeCliente(Guid clienteId);
    bool Eliminar(Guid id);
}
