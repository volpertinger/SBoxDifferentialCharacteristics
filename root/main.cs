/// <summary>
/// Суть программы: подсчет разностных характеристик S блока последовательным и параллельным алгоритмами
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Замер времени выполнения
            var watch = new ExecutionWatch();
            watch.allProgram.Start();

            // Чтение конфигурации
            var settings = new Settings("root/settings.conf");

            // Создание логгера
            Logger logger = new Logger(settings.logToConsole, settings.logToFile, settings.logPath);
            logger.LogInfo("Start");

            // Чтение входных данных
            watch.inputPreparation.Start();
            string[] inputs = File.ReadAllLines(settings.inputPath);
            watch.inputPreparation.Stop();

            // Проверка входных данных на корректность
            watch.inputCheck.Start();
            if (!Validator.isInputCorrect(inputs, settings, logger)) { return; }
            watch.inputCheck.Stop();

            // Создание SBox
            watch.inputPreparation.Start();
            SBox sbox = new SBox(inputs, settings.variableCount);
            watch.inputPreparation.Stop();

            // Вычисление разностных характеристик

            var results = new string[2];

            if (settings.calculateDifferentialCharacteristicsSequential)
            {
                sbox.calculateDifferentialCharacteristicsSequential();
                results[0] = sbox.GetDifferentialCharacteristic().ToString();
            }

            if (settings.calculateDifferentialCharacteristicsParallel)
            {
                sbox.calculateDifferentialCharacteristicsParallel(settings.threadsCount);
                results[1] = sbox.GetDifferentialCharacteristic().ToString();
            }

            // Запись результатов в выходной файл
            File.WriteAllLines(settings.outputPath, results);

            watch.StopAll();
            logger.LogInfo(watch.ToString());

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
