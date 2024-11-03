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

    /// <summary>
    /// Останавливает все таймеры
    /// </summary>
    public void StopAll()
    {
        allProgram.Stop();
        mainAlgorithm.Stop();
        inputCheck.Stop();
        inputPreparation.Stop();
    }

    /// <summary>
    /// Инициализирует все таймеры
    /// </summary>
    public ExecutionWatch()
    {
        allProgram = new Stopwatch();
        mainAlgorithm = new Stopwatch();
        inputCheck = new Stopwatch();
        inputPreparation = new Stopwatch();
    }
}