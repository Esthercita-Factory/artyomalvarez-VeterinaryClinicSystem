using System;
using System.Collections.Generic;
using System.Linq;
using VenterinaryClinicSystem.Models;

namespace VenterinaryClinicSystem.Services;

public class PatientService
{
    // TASK 1: Lista para almacenar pacientes
    private List<Patient> _pacientes = new List<Patient>();

    // TASK 1: Diccionario para asociar el ID (Guid) del paciente con su información (Acceso rápido por ID)
    private Dictionary<Guid, Patient> _pacientesPorId = new Dictionary<Guid, Patient>();

    public PatientService()
    {
        // Cargar datos iniciales de prueba para probar LINQ fácilmente
        CargarDatosIniciales();
    }

    private void CargarDatosIniciales()
    {
        var p1 = new Patient(Guid.NewGuid(), "Carlos Pérez", 35, "Chequeo de rutina");
        p1.Mascotas.Add(new Pet(101, "Firulais", 24, 12.5, "Vacunación", "Perro"));
        p1.Mascotas.Add(new Pet(102, "Michi", 12, 4.0, "Fiebre", "Gato"));

        var p2 = new Patient(Guid.NewGuid(), "Ana Gómez", 28, "Consulta general");
        p2.Mascotas.Add(new Pet(103, "Rex", 36, 20.0, "Cojera", "Perro"));

        var p3 = new Patient(Guid.NewGuid(), "Beatriz López", 42, "Control de peso");
        p3.Mascotas.Add(new Pet(104, "Garfield", 48, 6.5, "Sobrepeso", "Gato"));

        // Agregar a la lista
        _pacientes.Add(p1);
        _pacientes.Add(p2);
        _pacientes.Add(p3);

        // Agregar al diccionario (Clave: Id Guid, Valor: Objeto Paciente)
        _pacientesPorId[p1.Id] = p1;
        _pacientesPorId[p2.Id] = p2;
        _pacientesPorId[p3.Id] = p3;
    }

    // TASK 1: Registrar paciente y agregar a las colecciones (List y Dictionary)
    public void RegistrarPaciente()
    {
        Console.Write("Ingrese el nombre: ");
        string nombre = Console.ReadLine()!;
        while (string.IsNullOrWhiteSpace(nombre) || !nombre.Any(char.IsLetter))
        {
            Console.Write("Nombre inválido. Ingrese nuevamente: ");
            nombre = Console.ReadLine()!;
        }

        Console.Write("Ingrese la edad: ");
        int edad;
        while (!int.TryParse(Console.ReadLine(), out edad) || edad <= 0)
        {
            Console.Write("Edad inválida. Ingrese un número mayor a 0: ");
        }

        Console.Write("Ingrese los síntomas: ");
        string sintoma = Console.ReadLine()!;

        // Generar un Guid único automáticamente
        Patient nuevoPaciente = new Patient(Guid.NewGuid(), nombre, edad, sintoma);

        // Agregar a la Lista y al Diccionario
        _pacientes.Add(nuevoPaciente);
        _pacientesPorId[nuevoPaciente.Id] = nuevoPaciente;

        Console.WriteLine($"\n¡Paciente registrado con éxito! (ID Asignado: {nuevoPaciente.Id})");
    }

    // TASK 1: Modificar y Eliminar elementos en colecciones
    public void ModificarPaciente(Guid id, string nuevoNombre)
    {
        // Acceso directo y rápido usando el Diccionario con Guid
        if (_pacientesPorId.TryGetValue(id, out Patient? paciente) && paciente != null)
        {
            paciente.Nombre = nuevoNombre;
            Console.WriteLine($"Paciente con ID {id} modificado correctamente.");
        }
        else
        {
            Console.WriteLine($"No se encontró paciente con ID {id}.");
        }
    }

    public void EliminarPaciente(Guid id)
    {
        if (_pacientesPorId.TryGetValue(id, out Patient? paciente) && paciente != null)
        {
            // Eliminar de ambas colecciones
            _pacientes.Remove(paciente);
            _pacientesPorId.Remove(id);
            Console.WriteLine($"Paciente con ID {id} eliminado correctamente.");
        }
    }

    // TASK 2: Practicar diferencias entre Sintaxis de Consulta y Sintaxis de Métodos en LINQ
    public void DemostrarSintaxisLinq()
    {
        Console.WriteLine("\n=== TASK 2: EJEMPLOS DE LINQ ===");

        // 1. Where: Filtrar pacientes por edad
        Console.WriteLine("\n1. Filtrar pacientes con edad mayor a 30:");
        
        // Sintaxis de Métodos (Expresiones Lambda):
        var mayoresMetodo = _pacientes.Where(p => p.Edad > 30);
        
        // Sintaxis de Consulta (Parecida a SQL):
        var mayoresConsulta = from p in _pacientes
                              where p.Edad > 30
                              select p;

        foreach (var p in mayoresMetodo)
        {
            Console.WriteLine($"- {p.Nombre} ({p.Edad} años)");
        }

        // 2. Select: Proyectar solo los nombres de los pacientes
        Console.WriteLine("\n2. Proyectar solo nombres de pacientes (Select):");
        var soloNombres = _pacientes.Select(p => p.Nombre);
        foreach (var nombre in soloNombres)
        {
            Console.WriteLine($"- {nombre}");
        }

        // 3. OrderBy / OrderByDescending: Ordenar por Nombre o Edad
        Console.WriteLine("\n3. Pacientes ordenados por edad:");
        var ordenadosAsc = _pacientes.OrderBy(p => p.Edad);
        var ordenadosDesc = _pacientes.OrderByDescending(p => p.Edad);

        Console.WriteLine("  Ascendente:");
        foreach (var p in ordenadosAsc) Console.WriteLine($"    {p.Nombre}: {p.Edad} años");

        Console.WriteLine("  Descendente:");
        foreach (var p in ordenadosDesc) Console.WriteLine($"    {p.Nombre}: {p.Edad} años");

        // 4. GroupBy: Agrupar mascotas por especie
        Console.WriteLine("\n4. Mascotas agrupadas por especie (GroupBy):");
        var todasLasMascotas = _pacientes.SelectMany(p => p.Mascotas);
        var agrupadasPorEspecie = todasLasMascotas.GroupBy(m => m.Especie);

        foreach (var grupo in agrupadasPorEspecie)
        {
            Console.WriteLine($"  Especie: {grupo.Key}");
            foreach (var mascota in grupo)
            {
                Console.WriteLine($"    - {mascota.Nombre}");
            }
        }

        // 5. Métodos concretos: First, FirstOrDefault, Any, All, Count
        Console.WriteLine("\n5. Métodos concretos de LINQ:");
        
        // Count: Total de pacientes
        int totalPacientes = _pacientes.Count();
        Console.WriteLine($"  Total de pacientes (Count): {totalPacientes}");

        // FirstOrDefault: Buscar primer paciente llamado 'Ana Gómez' o devolver null si no existe
        var ana = _pacientes.FirstOrDefault(p => p.Nombre == "Ana Gómez");
        Console.WriteLine($"  Búsqueda con FirstOrDefault: {(ana != null ? ana.Nombre : "No encontrado")}");

        // Any: Verificar si existe al menos un paciente mayor de 40 años
        bool hayMayoresDe40 = _pacientes.Any(p => p.Edad > 40);
        Console.WriteLine($"  ¿Hay algún paciente mayor de 40 años? (Any): {hayMayoresDe40}");

        // All: Verificar si todos los pacientes tienen al menos 18 años
        bool todosMayoresDeEdad = _pacientes.All(p => p.Edad >= 18);
        Console.WriteLine($"  ¿Todos los pacientes son mayores de edad? (All): {todosMayoresDeEdad}");
    }

    public void ListarPacientes()
    {
        if (_pacientes.Count == 0)
        {
            Console.WriteLine("\nNo hay pacientes registrados.");
            return;
        }

        foreach (var patient in _pacientes)
        {
            Console.WriteLine($"\n[ID: {patient.Id}] Nombre: {patient.Nombre} | Edad: {patient.Edad} | Síntoma: {patient.Sintoma}");
            if (patient.Mascotas.Any())
            {
                Console.WriteLine("  Mascotas:");
                foreach (var m in patient.Mascotas)
                {
                    Console.WriteLine($"    - {m.Nombre} ({m.Especie}, {m.EdadEnMeses} meses)");
                }
            }
        }
    }

    public void BuscarPaciente()
    {
        Console.Write("Ingrese el nombre del paciente a buscar: ");
        string nombre = Console.ReadLine()!;

        // Uso de LINQ FirstOrDefault en sustitución del foreach manual
        var paciente = _pacientes.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (paciente != null)
        {
            Console.WriteLine($"\n[ID: {paciente.Id}] Nombre: {paciente.Nombre} | Edad: {paciente.Edad} | Síntomas: {paciente.Sintoma}");
        }
        else
        {
            Console.WriteLine("Paciente no encontrado.");
        }
    }

    // TASK 4: Encadenar consultas LINQ (Filtro + Ordenamiento + Proyección)
    public void DemostrarConsultasEncadenadas()
    {
        Console.WriteLine("\n=== TASK 4: CONSULTAS LINQ ENCADENADAS ===");
        Console.WriteLine("Ejemplo: Pacientes con mascotas 'Perro', ordenados por edad del paciente, mostrando solo Nombre y Mascota.");

        // Encadenamos: Where (filtro) -> OrderBy (orden) -> Select (proyección objeto anónimo)
        var resultadoEncadenado = _pacientes
            .Where(p => p.Mascotas.Any(m => m.Especie.Equals("Perro", StringComparison.OrdinalIgnoreCase)))
            .OrderBy(p => p.Edad)
            .Select(p => new 
            {
                NombreDueño = p.Nombre,
                EdadDueño = p.Edad,
                Perros = p.Mascotas.Where(m => m.Especie.Equals("Perro", StringComparison.OrdinalIgnoreCase)).Select(m => m.Nombre)
            });

        foreach (var item in resultadoEncadenado)
        {
            string nombresPerros = string.Join(", ", item.Perros);
            Console.WriteLine($"- Dueño: {item.NombreDueño} ({item.EdadDueño} años) | Perro(s): {nombresPerros}");
        }
    }

    // TASK 5: Resolver problemas prácticos con LINQ
    public void DemostrarProblemasPracticos()
    {
        Console.WriteLine("\n=== TASK 5: PROBLEMAS PRÁCTICOS CON LINQ ===");

        // 1. Paciente más joven y paciente de mayor edad
        var pacienteMasJoven = _pacientes.OrderBy(p => p.Edad).FirstOrDefault();
        var pacienteMayorEdad = _pacientes.OrderByDescending(p => p.Edad).FirstOrDefault();

        Console.WriteLine("\n1. Extremos de edad:");
        if (pacienteMasJoven != null)
            Console.WriteLine($"   Paciente más joven: {pacienteMasJoven.Nombre} ({pacienteMasJoven.Edad} años)");
        if (pacienteMayorEdad != null)
            Console.WriteLine($"   Paciente de mayor edad: {pacienteMayorEdad.Nombre} ({pacienteMayorEdad.Edad} años)");

        // 2. Contar cuántas mascotas hay de cada especie
        Console.WriteLine("\n2. Conteo de mascotas por especie:");
        var conteoPorEspecie = _pacientes
            .SelectMany(p => p.Mascotas) // Aplana las listas de mascotas de todos los pacientes en una sola lista
            .GroupBy(m => m.Especie)
            .Select(g => new { Especie = g.Key, Total = g.Count() });

        foreach (var grupo in conteoPorEspecie)
        {
            Console.WriteLine($"   Especie '{grupo.Especie}': {grupo.Total} mascota(s)");
        }

        // 3. Verificar si existe al menos un paciente con determinada condición
        // Ejemplo: Mascota con síntoma 'Fiebre'
        bool existeMascotaConFiebre = _pacientes
            .SelectMany(p => p.Mascotas)
            .Any(m => m.Sintoma.Equals("Fiebre", StringComparison.OrdinalIgnoreCase));

        Console.WriteLine($"\n3. ¿Existe al menos una mascota con síntoma 'Fiebre'?: {existeMascotaConFiebre}");

        // 4. Listar nombres de pacientes en MAYÚSCULAS ordenados alfabéticamente
        Console.WriteLine("\n4. Nombres de pacientes en MAYÚSCULAS y ordenados:");
        var nombresMayusculas = _pacientes
            .Select(p => p.Nombre.ToUpper())
            .OrderBy(n => n);

        foreach (var nombre in nombresMayusculas)
        {
            Console.WriteLine($"   - {nombre}");
        }
    }
}