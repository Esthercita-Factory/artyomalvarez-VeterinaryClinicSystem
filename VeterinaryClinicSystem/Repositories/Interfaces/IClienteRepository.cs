using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VeterinaryClinicSystem.Models;

namespace VeterinaryClinicSystem.Repositories;

/// <summary>
/// Contrato del Repositorio de Clientes (Pacientes).
/// Responsabilidad única: Abstraer el almacenamiento y acceso a datos de los clientes.
/// </summary>
public interface IClienteRepository
{
    void Agregar(Patient cliente);
    Task AgregarAsync(Patient cliente);
    IReadOnlyList<Patient> ObtenerTodos();
    Task<IReadOnlyList<Patient>> ObtenerTodosAsync();
    Patient? ObtenerPorId(Guid id);
    Patient? ObtenerPorNombre(string nombre);
    bool Actualizar(Patient cliente);
    bool Eliminar(Guid id);
}
