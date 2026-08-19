using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VenterinaryClinicSystem.Models;

namespace VenterinaryClinicSystem.Repository;

/// <summary>
/// Implementación concreta de IClienteRepository.
/// Bóveda pura de acceso a datos para clientes (Thread-Safe con ConcurrentDictionary).
/// </summary>
public class ClienteRepository : IClienteRepository
{
    private readonly ConcurrentDictionary<Guid, Patient> _clientesPorId = new ConcurrentDictionary<Guid, Patient>();

    public void Agregar(Patient cliente)
    {
        if (cliente == null) throw new ArgumentNullException(nameof(cliente));
        _clientesPorId[cliente.Id] = cliente;
    }

    public async Task AgregarAsync(Patient cliente)
    {
        if (cliente == null) throw new ArgumentNullException(nameof(cliente));
        await Task.Delay(500); // Simula latencia I/O de persistencia
        _clientesPorId[cliente.Id] = cliente;
    }

    public IReadOnlyList<Patient> ObtenerTodos()
    {
        return _clientesPorId.Values.ToList().AsReadOnly();
    }

    public async Task<IReadOnlyList<Patient>> ObtenerTodosAsync()
    {
        await Task.Delay(300);
        return ObtenerTodos();
    }

    public Patient? ObtenerPorId(Guid id)
    {
        _clientesPorId.TryGetValue(id, out var cliente);
        return cliente;
    }

    public Patient? ObtenerPorNombre(string nombre)
    {
        return _clientesPorId.Values.FirstOrDefault(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
    }

    public bool Actualizar(Patient cliente)
    {
        if (cliente == null || !_clientesPorId.ContainsKey(cliente.Id))
        {
            return false;
        }

        _clientesPorId[cliente.Id] = cliente;
        return true;
    }

    public bool Eliminar(Guid id)
    {
        return _clientesPorId.TryRemove(id, out _);
    }
}
