/// <summary>
/// Calculation of the difference characteristics of the S box by sequential and parallel algorithms
/// </summary>
class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Measuring the execution time
            var watch = new ExecutionWatch();
            watch.allProgram.Start();

            // Reading the configuration
            var settings = new Settings("root/settings.conf");

            // Creating a logger
            Logger logger = new Logger(settings.logToConsole, settings.logToFile, settings.logPath);
            logger.LogInfo($"Start\n{settings.ToString()}\n");

            // Reading the input data
            watch.inputPreparation.Start();
            if (settings.generatePermutations)
            {
                PermutationsGenerator.generate(settings.inputPath, settings.variableCount);
            }
            string[] inputs = File.ReadAllLines(settings.inputPath);
            watch.inputPreparation.Stop();

            // Checking the input data for correctness
            watch.inputCheck.Start();
            if (!Validator.isInputCorrect(inputs, settings, logger)) { return; }
            watch.inputCheck.Stop();

            // Creating an SBox
            watch.inputPreparation.Start();
            SBox sbox = new SBox(inputs, settings.variableCount);
            watch.inputPreparation.Stop();

            // Calculation of difference characteristics

            if (settings.calculateDifferentialCharacteristicsSequential)
            {
                watch.sequentialAlgorithm.Start();
                sbox.calculateDifferentialCharacteristicsSequential();
                watch.sequentialAlgorithm.Stop();
                if (settings.isNeedToWriteResult)
                    sbox.GetDifferentialCharacteristic().WriteToFile(settings.outputPath, settings.writeBuffer);
            }

            if (settings.calculateDifferentialCharacteristicsParallel)
            {
                watch.parallelAlgorithm.Start();
                sbox.calculateDifferentialCharacteristicsParallel(settings.threadsCount);
                watch.parallelAlgorithm.Stop();
                if (settings.isNeedToWriteResult)
                    sbox.GetDifferentialCharacteristic().WriteToFile(settings.outputPath, settings.writeBuffer);
            }

            watch.StopAll();
            logger.LogInfo(watch.ToString());

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error has occurred: {ex.Message}");
        }
    }
}
