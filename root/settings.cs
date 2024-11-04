/// <summary>
/// Representation of necessary parameters in the form of a class
/// </summary>
public class Settings
{

    /// <summary>
    /// The path to the input file
    /// </summary>
    public string InputPath { get; private set; }

    /// <summary>
    /// The path to the output file
    /// </summary>
    public string OutputPath { get; private set; }

    /// <summary>
    /// Flag of the need to write the result to output file
    /// </summary>
    public bool IsNeedToWriteResult { get; private set; }

    /// <summary>
    /// The path to the file with logs to write them
    /// </summary>
    public string LogPath { get; private set; }

    /// <summary>
    /// Number of boolean function variables
    /// </summary>
    public int VariableCount { get; private set; }

    /// <summary>
    /// The number of boolean function inputs
    /// </summary>
    public int InputsCount { get; private set; }

    /// <summary>
    /// Flag for writing logs to the console
    /// </summary>
    public bool LogToConsole { get; private set; }

    /// <summary>
    /// Flag for writing logs to a file
    /// </summary>
    public bool LogToFile { get; private set; }

    /// <summary>
    /// Flag for sequential calculation of the difference characteristics of the S Box
    /// </summary>
    public bool CalculateSequential { get; private set; }

    /// <summary>
    /// Flag for parallel calculation of the difference characteristics of the S Box
    /// </summary>
    public bool CalculateParallel { get; private set; }

    /// <summary>
    /// The number of threads for parallel operation of the algorithm
    /// </summary>
    public int ThreadsCount { get; private set; }

    /// <summary>
    /// A flag for generating the S box and writing to the output Path instead of reading from there
    /// </summary>
    public bool GenerateFunction { get; private set; }

    /// <summary>
    /// The size of the buffer to write to the output file
    /// </summary>
    public int WriteBuffer { get; private set; }

    /// <summary>
    /// A basic constructor that takes data from a file located on the path
    /// </summary>
    /// <param name="path">The path of the settings file</param>
    public Settings(string path)
    {
        var settings = File.ReadAllLines(path)
                                .Select(line => line.Split('='))
                                .ToDictionary(parts => parts[0].Trim(), parts => parts[1].Trim());

        InputPath = settings["input_file"];
        OutputPath = settings["output_file"];
        IsNeedToWriteResult = LogToFile = bool.Parse(settings["is_need_to_write_result"]);
        LogPath = settings["log_file"];
        VariableCount = int.Parse(settings["variable_count"]);
        InputsCount = (int)Math.Pow(2, VariableCount);
        LogToConsole = bool.Parse(settings["log_to_console"]);
        LogToFile = bool.Parse(settings["log_to_file"]);
        CalculateSequential = bool.Parse(settings["calculate_sequential"]);
        CalculateParallel = bool.Parse(settings["calculate_parallel"]);
        ThreadsCount = int.Parse(settings["threads_count"]);
        GenerateFunction = bool.Parse(settings["generate_function"]);
        WriteBuffer = int.Parse(settings["write_buffer"]);
    }

    public override string ToString()
    {
        return $"variables: {VariableCount}; inputs: {InputsCount};\n" +
        $"calculate sequential: {CalculateSequential};\n" +
        $"calculate parallel: {CalculateParallel}; threads: {ThreadsCount};\n" +
        $"function generation: {GenerateFunction};";
    }
}
