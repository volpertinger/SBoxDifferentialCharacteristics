using System;
using System.IO;

public class Logger
{
    private readonly bool _logToConsole;
    private readonly bool _logToFile;
    private readonly string _filePath;

    public Logger(bool logToConsole, bool logToFile, string filePath = "log.txt")
    {
        _logToConsole = logToConsole;
        _logToFile = logToFile;
        _filePath = filePath;
    }

    public void Log(string message)
    {
        string logMessage = $"{DateTime.Now}: {message}";

        if (_logToConsole)
        {
            Console.WriteLine(logMessage);
        }

        if (_logToFile)
        {
            try
            {
                File.AppendAllText(_filePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в файл: {ex.Message}");
            }
        }
    }

    public void LogError(string message)
    {
        Log($"ERROR: {message}");
    }

    public void LogInfo(string message)
    {
        Log($"INFO: {message}");
    }

    public void LogWarning(string message)
    {
        Log($"WARNING: {message}");
    }
}
