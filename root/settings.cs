/// <summary>
/// Представление необходимых параметров в виде класса
/// </summary>
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

    /// <summary>
    /// Флаг для помледовательного вычисления разностных характеристик S Box
    /// </summary>
    public bool calculateDifferentialCharacteristicsSequential { get; private set; }

    /// <summary>
    /// Флаг для параллельного вычисления разностных характеристик S Box
    /// </summary>
    public bool calculateDifferentialCharacteristicsParallel { get; private set; }

    /// <summary>
    /// Количество потоков для параллельной работы алгоритма
    /// </summary>
    public int threadsCount { get; private set; }

    /// <summary>
    /// Базовый конструктор, который берет данные из файла, расположенного по пути path
    /// </summary>
    /// <param name="path">Путь файла с настройками</param>
    public Settings(string path)
    {
        var settings = File.ReadAllLines(path)
                                .Select(line => line.Split('='))
                                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

        inputPath = settings["input_file"];
        outputPath = settings["output_file"];
        logPath = settings["log_file"];
        variableCount = int.Parse(settings["variable_count"]);
        possibleInputsCount = (int)Math.Pow(2, variableCount);
        logToConsole = bool.Parse(settings["log_to_console"]);
        logToFile = bool.Parse(settings["log_to_file"]);
        calculateDifferentialCharacteristicsSequential = bool.Parse(settings["calculate_differential_characteristics_sequential"]);
        calculateDifferentialCharacteristicsParallel = bool.Parse(settings["calculate_differential_characteristics_parallel"]);
        threadsCount = int.Parse(settings["threads_count"]);
    }
}
