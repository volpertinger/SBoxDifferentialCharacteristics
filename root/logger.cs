/// <summary>
/// Простоей логгер, написанный для нужд основной программы
/// </summary>
public class Logger
{
    /// <summary>
    /// Флаг логирования в консоль
    /// </summary>
    private readonly bool _logToConsole;

    /// <summary>
    /// Флаг логирования в файл
    /// </summary>
    private readonly bool _logToFile;

    /// <summary>
    /// Путь для записи логов в файл
    /// </summary>
    private readonly string _filePath;

    /// <summary>
    /// Базовый конструктор
    /// </summary>
    /// <param name="logToConsole">Флаг логирования в консоль</param>
    /// <param name="logToFile">Флаг логирования в файл</param>
    /// <param name="filePath">Путь для записи логов в файл</param>

    public Logger(bool logToConsole, bool logToFile, string filePath = "log.txt")
    {
        _logToConsole = logToConsole;
        _logToFile = logToFile;
        _filePath = filePath;
    }

    /// <summary>
    /// Бащовая функция для записи лога
    /// </summary>
    /// <param name="message">Строка, которая будет залогирована</param>
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

    /// <summary>
    /// Логирование уровня ERROR
    /// </summary>
    /// <param name="message">Сообщение</param>
    public void LogError(string message)
    {
        Log($"ERROR: {message}");
    }

    /// <summary>
    /// Логирование уровня INFO
    /// </summary>
    /// <param name="message">Сообщение</param>
    public void LogInfo(string message)
    {
        Log($"INFO: {message}");
    }

    /// <summary>
    /// Логирование уровня WARNING
    /// </summary>
    /// <param name="message">Сообщение</param>
    public void LogWarning(string message)
    {
        Log($"WARNING: {message}");
    }
}
