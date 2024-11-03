/// <summary>
/// Just a keylogger written for the needs of the main program
/// </summary>
public class Logger
{
    /// <summary>
    /// The logging flag to the console
    /// </summary>
    private readonly bool _logToConsole;

    /// <summary>
    /// The flag for logging to a file
    /// </summary>
    private readonly bool _logToFile;

    /// <summary>
    /// The path for writing logs to a file
    /// </summary>
    private readonly string _filePath;

    /// <summary>
    /// Basic Constructor
    /// </summary>

    public Logger(bool logToConsole, bool logToFile, string filePath = "log.txt")
    {
        _logToConsole = logToConsole;
        _logToFile = logToFile;
        _filePath = filePath;
    }

    /// <summary>
    /// The basic function for recording the log
    /// </summary>
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
                Console.WriteLine($"Error writing to the file: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// ERROR level logging
    /// </summary>
    public void LogError(string message)
    {
        Log($"ERROR: {message}");
    }

    /// <summary>
    /// INFO level logging
    /// </summary>
    public void LogInfo(string message)
    {
        Log($"INFO: {message}");
    }

    /// <summary>
    /// WARNING level logging
    /// </summary>
    public void LogWarning(string message)
    {
        Log($"WARNING: {message}");
    }
}
