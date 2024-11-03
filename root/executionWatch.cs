using System.Diagnostics;

/// <summary>
/// The class is necessary for calculating the execution time of the program as a whole and its individual parts
/// </summary>
public class ExecutionWatch
{

    /// <summary>
    /// Calculation of the execution time of the entire program
    /// </summary>
    public Stopwatch allProgram { get; private set; }

    /// <summary>
    /// Calculation of the execution time of the sequential algorithm
    /// </summary>
    public Stopwatch sequentialAlgorithm { get; private set; }

    /// <summary>
    /// Calculation of the execution time of the parallel algorithm
    /// </summary>
    public Stopwatch parallelAlgorithm { get; private set; }

    /// <summary>
    /// Calculation of the time for checking the correctness of the input data
    /// </summary>
    public Stopwatch inputCheck { get; private set; }

    /// <summary>
    /// Calculation of the input data preparation time to start executing the main algorithm
    /// </summary>
    public Stopwatch inputPreparation { get; private set; }

    /// <summary>
    /// Stops all timers
    /// </summary>
    public void StopAll()
    {
        allProgram.Stop();
        inputCheck.Stop();
        inputPreparation.Stop();
        sequentialAlgorithm.Stop();
        parallelAlgorithm.Stop();
    }

    public override string ToString()
    {

        return $"\nExecution time [allProgram]: {allProgram.Elapsed.TotalSeconds} second\n" +
        $"Execution time [inputCheck]: {inputCheck.Elapsed.TotalSeconds} seconds\n" +
        $"Execution time [inputPreparation]: {inputPreparation.Elapsed.TotalSeconds} seconds\n" +
        $"Execution time [sequentialAlgorithm]: {sequentialAlgorithm.Elapsed.TotalSeconds} seconds\n" +
        $"Execution time [parallelAlgorithm]: {parallelAlgorithm.Elapsed.TotalSeconds} seconds\n";
    }

    /// <summary>
    /// Initializes all timers
    /// </summary>
    public ExecutionWatch()
    {
        allProgram = new Stopwatch();
        inputCheck = new Stopwatch();
        inputPreparation = new Stopwatch();
        sequentialAlgorithm = new Stopwatch();
        parallelAlgorithm = new Stopwatch();
    }
}