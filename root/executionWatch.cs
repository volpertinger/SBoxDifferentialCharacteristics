using System.Diagnostics;

/// <summary>
/// The class is necessary for calculating the execution time of the program as a whole and its individual parts
/// </summary>
public class ExecutionWatch
{

    /// <summary>
    /// Calculation of the execution time of the entire program
    /// </summary>
    public Stopwatch AllProgram { get; private set; }

    /// <summary>
    /// Calculation of the execution time of the sequential algorithm
    /// </summary>
    public Stopwatch SequentialAlgorithm { get; private set; }

    /// <summary>
    /// Calculation of the execution time of the parallel algorithm
    /// </summary>
    public Stopwatch ParallelAlgorithm { get; private set; }

    /// <summary>
    /// Calculation of the time for checking the correctness of the input data
    /// </summary>
    public Stopwatch InputCheck { get; private set; }

    /// <summary>
    /// Calculation of the input data preparation time to start executing the main algorithm
    /// </summary>
    public Stopwatch InputPreparation { get; private set; }

    /// <summary>
    /// Stops all timers
    /// </summary>
    public void StopAll()
    {
        AllProgram.Stop();
        InputCheck.Stop();
        InputPreparation.Stop();
        SequentialAlgorithm.Stop();
        ParallelAlgorithm.Stop();
    }

    public override string ToString()
    {

        return $"\nExecution time [AllProgram]: {AllProgram.Elapsed.TotalSeconds} second\n" +
        $"Execution time [InputCheck]: {InputCheck.Elapsed.TotalSeconds} seconds\n" +
        $"Execution time [InputPreparation]: {InputPreparation.Elapsed.TotalSeconds} seconds\n" +
        $"Execution time [SequentialAlgorithm]: {SequentialAlgorithm.Elapsed.TotalSeconds} seconds\n" +
        $"Execution time [ParallelAlgorithm]: {ParallelAlgorithm.Elapsed.TotalSeconds} seconds\n";
    }

    /// <summary>
    /// Initializes all timers
    /// </summary>
    public ExecutionWatch()
    {
        AllProgram = new Stopwatch();
        InputCheck = new Stopwatch();
        InputPreparation = new Stopwatch();
        SequentialAlgorithm = new Stopwatch();
        ParallelAlgorithm = new Stopwatch();
    }
}