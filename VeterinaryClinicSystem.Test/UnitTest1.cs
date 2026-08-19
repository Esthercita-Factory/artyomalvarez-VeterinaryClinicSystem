using System;
using Xunit;
using VenterinaryClinicSystem.Models;
using VenterinaryClinicSystem.Services;
using VenterinaryClinicSystem.Exceptions;

namespace VeterinaryClinicSystem.Test;

public class ClinicSystemM53S4Tests
{
    [Fact]
    public void Patient_DeberiaImplementar_IRegistrableYINotificable()
    {
        // Arrange
        var paciente = new Patient(Guid.NewGuid(), "Prueba Test", 30, "Tos", "555-0000");

        // Assert
        Assert.IsAssignableFrom<IRegistrable>(paciente);
        Assert.IsAssignableFrom<INotificable>(paciente);
    }

    [Fact]
    public void ServicioVeterinario_DeberiaImplementar_IAtendible()
    {
        // Arrange
        var vet = new Trabajador(Guid.NewGuid(), "Dr. Test", 40, "555-1111", "Veterinario", "General");
        var servicio = new ConsultaGeneral(vet);

        // Assert
        Assert.IsAssignableFrom<IAtendible>(servicio);
    }

    [Fact]
    public void BuscarMascotaDePaciente_DeberiaLanzar_MascotaNoEncontradaException()
    {
        // Arrange
        var service = new PatientService();

        // Act & Assert
        Assert.Throws<MascotaNoEncontradaException>(() => 
            service.BuscarMascotaDePaciente("Carlos Pérez", "Inexistente"));
    }
}