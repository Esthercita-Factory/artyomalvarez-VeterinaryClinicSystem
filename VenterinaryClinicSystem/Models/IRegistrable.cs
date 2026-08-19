namespace VenterinaryClinicSystem.Models;

/// <summary>
/// Contrato IRegistrable (Capa de Dominio).
/// </summary>
public interface IRegistrable
{
    void Registrar();
    string ObtenerInformacion();
}
