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
            logger.LogInfo($"Start\n{settings.ToString()}\n");

            // Чтение входных данных
            watch.inputPreparation.Start();
            if (settings.generatePermutations)
            {
                PermutationsGenerator.generate(settings.inputPath, settings.variableCount);
            }
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


            if (settings.calculateDifferentialCharacteristicsSequential)
            {
                watch.sequentialAlgorithm.Start();
                sbox.calculateDifferentialCharacteristicsSequential();
                watch.sequentialAlgorithm.Stop();
                sbox.GetDifferentialCharacteristic().WriteToFile(settings.outputPath, settings.writeBuffer);
            }

            if (settings.calculateDifferentialCharacteristicsParallel)
            {
                watch.parallelAlgorithm.Start();
                sbox.calculateDifferentialCharacteristicsParallel(settings.threadsCount);
                watch.parallelAlgorithm.Stop();
            }

            // Запись результатов в выходной файл
            //File.WriteAllLines(settings.outputPath, results);

            watch.StopAll();
            logger.LogInfo(watch.ToString());

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
