using System.Diagnostics;

public class ExecutionWatch
{

    /// <summary>
    /// Расчет времени выполнения всей программы
    /// </summary>
    public Stopwatch allProgram { get; private set; }

    /// <summary>
    /// Расчет времени выполнения основного алгоритма
    /// </summary>
    public Stopwatch mainAlgorithm { get; private set; }

    public ExecutionWatch()
    {
        allProgram = new Stopwatch();
        mainAlgorithm = new Stopwatch();
    }
}