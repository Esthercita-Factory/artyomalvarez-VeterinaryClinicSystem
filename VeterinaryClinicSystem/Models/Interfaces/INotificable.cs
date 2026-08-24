namespace VeterinaryClinicSystem.Models;

/// <summary>
/// TASK 3: Interfaz INotificable.
/// Permite enviar notificaciones a los clientes o entidades asociadas (ej. recordatorios de cita).
/// Demuestra el uso de múltiples interfaces al ser implementada junto con IRegistrable en la clase Patient.
/// </summary>
public interface INotificable
{
    void EnviarNotificacion(string mensaje);
}
