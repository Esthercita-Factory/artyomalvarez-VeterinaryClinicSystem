using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using VeterinaryClinicSystem.Models;
using VeterinaryClinicSystem.Services;
using VeterinaryClinicSystem.Repositories;
using VeterinaryClinicSystem.Exceptions;

namespace VeterinaryClinicSystem.Test;

public class ClinicSystemUseCaseTests
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IMascotaRepository _mascotaRepository;
    private readonly IPatientService _service;

    public ClinicSystemUseCaseTests()
    {
        _clienteRepository = new ClienteRepository();
        _mascotaRepository = new MascotaRepository();
        _service = new PatientService(_clienteRepository, _mascotaRepository);
    }

    [Fact]
    public void CasoUso1_RegistrarCliente_DeberiaGuardarEnClienteRepository()
    {
        // Act
        var cliente = _service.RegistrarPaciente("Juan Delgado", 35, "Dolor de oído");

        // Assert
        Assert.NotNull(cliente);
        Assert.NotEqual(Guid.Empty, cliente.Id);
        var guardado = _service.BuscarPorNombre("Juan Delgado");
        Assert.NotNull(guardado);
        Assert.Equal(35, guardado.Edad);
    }

    [Fact]
    public async Task CasoUso2_RegistrarClienteAsincrono_DeberiaGuardarSinBloquear()
    {
        // Act
        var cliente = await _service.RegistrarPacienteAsync("Valeria Rios", 29, "Control anual");

        // Assert
        Assert.NotNull(cliente);
        var guardado = await _clienteRepository.ObtenerTodosAsync();
        Assert.Contains(guardado, c => c.Nombre == "Valeria Rios");
    }

    [Theory]
    [InlineData("", 25, "Fiebre")]
    [InlineData("   ", 30, "Tos")]
    [InlineData(null, 40, "Revisión")]
    public void CasoUso3_RegistrarCliente_NombreInvalido_DeberiaLanzarArgumentException(string? nombreInvalido, int edad, string sintoma)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => _service.RegistrarPaciente(nombreInvalido, edad, sintoma));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void CasoUso4_RegistrarCliente_EdadInvalida_DeberiaLanzarArgumentOutOfRangeException(int edadInvalida)
    {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => _service.RegistrarPaciente("Maria Santos", edadInvalida, "Control"));
    }

    [Fact]
    public void CasoUso5_BuscarMascotaEnMascotaRepository_DeberiaEncontrarPorClienteId()
    {
        // Arrange
        var cliente = _service.BuscarPorNombre("Carlos Pérez");
        Assert.NotNull(cliente);

        // Act
        var mascota = _service.BuscarMascotaDePaciente("Carlos Pérez", "Firulais");

        // Assert
        Assert.NotNull(mascota);
        Assert.Equal("Firulais", mascota.Nombre);
    }

    [Fact]
    public void CasoUso6_BuscarMascotaInexistente_DeberiaLanzarMascotaNoEncontradaException()
    {
        // Act & Assert
        Assert.Throws<MascotaNoEncontradaException>(() => 
            _service.BuscarMascotaDePaciente("Carlos Pérez", "MascotaFantasma"));
    }

    [Fact]
    public void CasoUso7_PolimorfismoMascotas_DeberiaEmitirSonidoCorrecto()
    {
        // Arrange
        var perro = new Pet(Guid.NewGuid(), "PerroTest", 12, 10, "Sintoma", "Perro");
        var gato = new Pet(Guid.NewGuid(), "GatoTest", 12, 4, "Sintoma", "Gato");

        // Assert
        Assert.Equal("¡Guau Guau!", perro.EmitirSonido());
        Assert.Equal("¡Miau Miau!", gato.EmitirSonido());
    }
}
