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

    /// <summary>
    /// Расчет времени проверки корректности входных данных
    /// </summary>
    public Stopwatch inputCheck { get; private set; }

    /// <summary>
    /// Расчет времени подготовки входных данных для начала выполнения основного алгоритма
    /// </summary>
    public Stopwatch inputPreparation { get; private set; }

    public void StopAll()
    {
        allProgram.Stop();
        mainAlgorithm.Stop();
        inputCheck.Stop();
        inputPreparation.Stop();
    }

    public ExecutionWatch()
    {
        allProgram = new Stopwatch();
        mainAlgorithm = new Stopwatch();
        inputCheck = new Stopwatch();
        inputPreparation = new Stopwatch();
    }
}