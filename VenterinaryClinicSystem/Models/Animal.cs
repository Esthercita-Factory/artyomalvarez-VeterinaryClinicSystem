using System;

namespace VenterinaryClinicSystem.Models;

/// <summary>
/// Clase abstracta Animal (Capa de Dominio).
/// </summary>
public abstract class Animal : IRegistrable
{
    public Guid Id { get; set; }
    public string Nombre { get; set; }
    public int EdadEnMeses { get; set; }
    public string Especie { get; set; }

    public Animal(Guid id, string nombre, int edadEnMeses, string especie)
    {
        Id = id;
        Nombre = nombre;
        EdadEnMeses = edadEnMeses;
        Especie = especie;
    }

    public virtual string EmitirSonido()
    {
        return "El animal hace un sonido indeterminado.";
    }

    public virtual void Registrar()
    {
        // Operación de negocio al registrar animal
    }

    public abstract string ObtenerInformacion();
}
