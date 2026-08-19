using System;
using System.IO;

namespace VenterinaryClinicSystem.Services;

/// <summary>
/// TASK 6: Sistema de registro de errores (Logging básico).
/// Escribe logs formateados en consola y guarda trazas de auditoría/error en un archivo local (app.log).
/// </summary>
public static class LoggerService
{
    private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");

    public static void LogInfo(string mensaje)
    {
        string registro = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {mensaje}";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(registro);
        Console.ResetColor();
        EscribirEnArchivo(registro);
    }

    public static void LogWarning(string mensaje)
    {
        string registro = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [WARNING] {mensaje}";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(registro);
        Console.ResetColor();
        EscribirEnArchivo(registro);
    }

    public static void LogError(string mensaje, Exception? ex = null)
    {
        string registro = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [ERROR] {mensaje}";
        if (ex != null)
        {
            registro += $" | Detalle Excepción: {ex.GetType().Name} - {ex.Message}";
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(registro);
        Console.ResetColor();
        EscribirEnArchivo(registro);
    }

    private static void EscribirEnArchivo(string contenido)
    {
        try
        {
            File.AppendAllText(LogFilePath, contenido + Environment.NewLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
        }
    }
}
