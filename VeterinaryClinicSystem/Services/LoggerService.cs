using System;
using System.IO;

namespace VeterinaryClinicSystem.Services;

/// <summary>
/// TASK 6: Sistema de registro de errores (Logging básico con rotación/límite de tamaño).
/// </summary>
public static class LoggerService
{
    private static readonly string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.log");
    private const long MaxLogFileSizeBytes = 1 * 1024 * 1024; // Límite de 1MB para prevenir consumo excesivo de disco
    private static readonly object _fileLock = new object();

    public static void LogInfo(string mensaje)
    {
        string registro = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [INFO] {mensaje}";
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(registro);
        Console.ResetColor();
        EscribirEnArchivoConRotacion(registro);
    }

    public static void LogWarning(string mensaje)
    {
        string registro = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [WARNING] {mensaje}";
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(registro);
        Console.ResetColor();
        EscribirEnArchivoConRotacion(registro);
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
        EscribirEnArchivoConRotacion(registro);
    }

    private static void EscribirEnArchivoConRotacion(string contenido)
    {
        try
        {
            lock (_fileLock)
            {
                FileInfo fileInfo = new FileInfo(LogFilePath);
                // Rotación básica: si supera el límite de 1MB, se sobrescribe para evitar saturación de disco (DoS)
                if (fileInfo.Exists && fileInfo.Length > MaxLogFileSizeBytes)
                {
                    File.WriteAllText(LogFilePath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] --- LOG ROTADO POR TAMAÑO LÍMITE ---" + Environment.NewLine);
                }

                File.AppendAllText(LogFilePath, contenido + Environment.NewLine);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
        }
    }
}
