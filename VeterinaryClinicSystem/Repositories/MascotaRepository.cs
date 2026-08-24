using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinicSystem.Models;

namespace VeterinaryClinicSystem.Repositories;

/// <summary>
/// Implementación concreta de IMascotaRepository.
/// Bóveda pura de acceso a datos para mascotas (Thread-Safe con ConcurrentDictionary).
/// </summary>
public class MascotaRepository : IMascotaRepository
{
    private readonly ConcurrentDictionary<Guid, (Pet Mascota, Guid ClienteId)> _mascotasPorId = new ConcurrentDictionary<Guid, (Pet, Guid)>();

    public void Agregar(Pet mascota, Guid clienteId)
    {
        if (mascota == null) throw new ArgumentNullException(nameof(mascota));
        _mascotasPorId[mascota.Id] = (mascota, clienteId);
    }

    public async Task AgregarAsync(Pet mascota, Guid clienteId)
    {
        if (mascota == null) throw new ArgumentNullException(nameof(mascota));
        await Task.Delay(500); // Simula persistencia I/O
        _mascotasPorId[mascota.Id] = (mascota, clienteId);
    }

    public IReadOnlyList<Pet> ObtenerTodas()
    {
        return _mascotasPorId.Values.Select(tuple => tuple.Mascota).ToList().AsReadOnly();
    }

    public IReadOnlyList<Pet> ObtenerPorClienteId(Guid clienteId)
    {
        return _mascotasPorId.Values
            .Where(tuple => tuple.ClienteId == clienteId)
            .Select(tuple => tuple.Mascota)
            .ToList()
            .AsReadOnly();
    }

    public Pet? ObtenerPorId(Guid id)
    {
        if (_mascotasPorId.TryGetValue(id, out var tuple))
        {
            return tuple.Mascota;
        }
        return null;
    }

    public bool Eliminar(Guid id)
    {
        return _mascotasPorId.TryRemove(id, out _);
    }
}
