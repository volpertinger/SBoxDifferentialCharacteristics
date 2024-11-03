/// <summary>
/// Representation of necessary parameters in the form of a class
/// </summary>
public class Settings
{

    /// <summary>
    /// The path to the login file
    /// </summary>
    public string inputPath { get; private set; }

    /// <summary>
    /// The path to the output file
    /// </summary>
    public string outputPath { get; private set; }

    /// <summary>
    /// Flag of the need to record the result
    /// </summary>
    public bool isNeedToWriteResult { get; private set; }

    /// <summary>
    /// The path to the log file
    /// </summary>
    public string logPath { get; private set; }

    /// <summary>
    /// Number of variables
    /// </summary>
    public int variableCount { get; private set; }

    /// <summary>
    /// The number of possible input vectors
    /// </summary>
    public int possibleInputsCount { get; private set; }

    /// <summary>
    /// Flag for writing logs to the console
    /// </summary>
    public bool logToConsole { get; private set; }

    /// <summary>
    /// Flag for writing logs to a file
    /// </summary>
    public bool logToFile { get; private set; }

    /// <summary>
    /// Flag for sequential calculation of the difference characteristics of the S Box
    /// </summary>
    public bool calculateDifferentialCharacteristicsSequential { get; private set; }

    /// <summary>
    /// Flag for parallel calculation of the difference characteristics of the S Box
    /// </summary>
    public bool calculateDifferentialCharacteristicsParallel { get; private set; }

    /// <summary>
    /// The number of threads for parallel operation of the algorithm
    /// </summary>
    public int threadsCount { get; private set; }

    /// <summary>
    /// A flag for generating the S box and writing to the output Path instead of reading from there
    /// </summary>
    public bool generatePermutations { get; private set; }

    /// <summary>
    /// The size of the buffer to write to the file
    /// </summary>
    public int writeBuffer { get; private set; }

    /// <summary>
    /// A basic constructor that takes data from a file located on the path
    /// </summary>
    /// <param name="path">The path of the settings file</param>
    public Settings(string path)
    {
        var settings = File.ReadAllLines(path)
                                .Select(line => line.Split('='))
                                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

        inputPath = settings["input_file"];
        outputPath = settings["output_file"];
        isNeedToWriteResult = logToFile = bool.Parse(settings["is_need_to_write_result"]);
        logPath = settings["log_file"];
        variableCount = int.Parse(settings["variable_count"]);
        possibleInputsCount = (int)Math.Pow(2, variableCount);
        logToConsole = bool.Parse(settings["log_to_console"]);
        logToFile = bool.Parse(settings["log_to_file"]);
        calculateDifferentialCharacteristicsSequential = bool.Parse(settings["calculate_differential_characteristics_sequential"]);
        calculateDifferentialCharacteristicsParallel = bool.Parse(settings["calculate_differential_characteristics_parallel"]);
        threadsCount = int.Parse(settings["threads_count"]);
        generatePermutations = bool.Parse(settings["generate_permutations"]);
        writeBuffer = int.Parse(settings["write_buffer"]);
    }

    public override string ToString()
    {
        return $"variables: {variableCount}; inputs: {possibleInputsCount};\n" +
        $"calculate sequential: {calculateDifferentialCharacteristicsSequential};\n" +
        $"calculate parallel: {calculateDifferentialCharacteristicsParallel}; threads: {threadsCount};\n" +
        $"permutations generation: {generatePermutations};";
    }
}
