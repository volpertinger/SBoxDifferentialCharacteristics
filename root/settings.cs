using System.IO;
using System.Configuration;
public class Settings
{

    /// <summary>
    /// Путь к файлу входа
    /// </summary>
    public string inputPath { get; private set; }

    /// <summary>
    /// Путь к файлу выхода
    /// </summary>
    public string outputPath { get; private set; }

    /// <summary>
    /// Путь к файлу лога
    /// </summary>
    public string logPath { get; private set; }

    /// <summary>
    /// Количество переменных
    /// </summary>
    public int variableCount { get; private set; }

    /// <summary>
    /// Количество возможных входных векторов
    /// </summary>
    public int possibleInputsCount { get; private set; }

    /// <summary>
    /// Флаг для записи логов в консоль
    /// </summary>
    public bool logToConsole { get; private set; }

    /// <summary>
    /// Флаг для записи логов в файл
    /// </summary>
    public bool logToFile { get; private set; }

    public Settings()
    {
        var settings = File.ReadAllLines("root/settings.conf")
                                .Select(line => line.Split('='))
                                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

        inputPath = settings["input_file"];
        outputPath = settings["output_file"];
        logPath = settings["log_file"];
        variableCount = int.Parse(settings["variable_count"]);
        possibleInputsCount = (int)Math.Pow(2, variableCount);
        logToConsole = bool.Parse(settings["log_to_console"]);
        logToFile = bool.Parse(settings["log_to_file"]);
    }
}
