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

    [Fact]
    public async Task CasoUso8_RegistrarClienteConTelefono_DeberiaGuardarTelefonoCorrectamente()
    {
        // Act
        var cliente = await _service.RegistrarPacienteAsync("Roberto Gomez", 40, "Vacunación", "555-987-6543");

        // Assert
        Assert.NotNull(cliente);
        Assert.Equal("555-987-6543", cliente.Telefono);
    }

    [Fact]
    public async Task CasoUso9_RegistrarMascotaParaCliente_DeberiaAsociarMascotaAlClienteYRepositorio()
    {
        // Arrange
        var cliente = await _service.RegistrarPacienteAsync("Laura Pausini", 33, "Consulta general", "555-112-2334");

        // Act
        var mascota = await _service.RegistrarMascotaAsync(cliente.Id, "Rocky", 18, 14.2, "Revisión dental", "Perro", "Beagle");

        // Assert
        Assert.NotNull(mascota);
        Assert.NotNull(mascota.Dueno);
        Assert.Equal("Laura Pausini", mascota.Dueno.Nombre);
        Assert.False(mascota.EsCallejera);
        Assert.Contains(cliente.Mascotas, m => m.Nombre == "Rocky");
        var mascotasDelCliente = _mascotaRepository.ObtenerPorClienteId(cliente.Id);
        Assert.Contains(mascotasDelCliente, m => m.Nombre == "Rocky");
    }

    [Fact]
    public async Task CasoUso10_RegistrarMascotaSinDueno_DeberiaGuardarseEnMascotaRepositorySinDueno()
    {
        // Act
        var mascotaSinDueno = await _service.RegistrarMascotaAsync(null, "Callejerito", 6, 3.5, "Rescate", "Gato", "Criollo");

        // Assert
        Assert.NotNull(mascotaSinDueno);
        Assert.Null(mascotaSinDueno.Dueno);
        Assert.True(mascotaSinDueno.EsCallejera);
        var sinDueno = _mascotaRepository.ObtenerSinDueno();
        Assert.Contains(sinDueno, m => m.Nombre == "Callejerito");
    }

    [Fact]
    public async Task CasoUso11_EliminarCliente_DeberiaDesvincularMascotasYDejarlasSinDueno()
    {
        // Arrange
        var cliente = await _service.RegistrarPacienteAsync("Pedro Infante", 50, "Chequeo", "555-777-8899");
        var mascota = await _service.RegistrarMascotaAsync(cliente.Id, "Torito", 24, 25.0, "Vacunación", "Perro", "Bulldog");

        // Act
        bool eliminado = _service.EliminarPaciente(cliente.Id);

        // Assert
        Assert.True(eliminado);
        Assert.Null(mascota.Dueno);
        Assert.True(mascota.EsCallejera);
        var mascotasDelCliente = _mascotaRepository.ObtenerPorClienteId(cliente.Id);
        Assert.Empty(mascotasDelCliente);
        var sinDueno = _service.ObtenerMascotasSinDueno();
        Assert.Contains(sinDueno, m => m.Nombre == "Torito");
    }

    [Fact]
    public async Task CasoUso12_ObtenerMascotasSinDueno_DeberiaRetornarListaCorrecta()
    {
        // Act
        await _service.RegistrarMascotaAsync(null, "Pelusa", 10, 3.0, "Revisión general", "Gato", "Angora");
        var listaSinDueno = _service.ObtenerMascotasSinDueno();

        // Assert
        Assert.Contains(listaSinDueno, m => m.Nombre == "Pelusa");
    }
}
