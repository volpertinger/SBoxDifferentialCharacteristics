using System.Diagnostics;

/// <summary>
/// Класс необходим для подсчета времени выполнения программы в целом и отдельных ее частей
/// </summary>
public class ExecutionWatch
{

    /// <summary>
    /// Расчет времени выполнения всей программы
    /// </summary>
    public Stopwatch allProgram { get; private set; }

    /// <summary>
    /// Расчет времени выполнения последовательного алгоритма
    /// </summary>
    public Stopwatch sequentialAlgorithm { get; private set; }

    /// <summary>
    /// Расчет времени выполнения параллельного алгоритма
    /// </summary>
    public Stopwatch parallelAlgorithm { get; private set; }

    /// <summary>
    /// Расчет времени проверки корректности входных данных
    /// </summary>
    public Stopwatch inputCheck { get; private set; }

    /// <summary>
    /// Расчет времени подготовки входных данных для начала выполнения основного алгоритма
    /// </summary>
    public Stopwatch inputPreparation { get; private set; }

    /// <summary>
    /// Останавливает все таймеры
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
    /// Инициализирует все таймеры
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