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
            watch.AllProgram.Start();

            // Reading the configuration
            var settings = new Settings("root/settings.conf");

            // Creating a logger
            Logger logger = new Logger(settings.LogToConsole, settings.LogToFile, settings.LogPath);
            logger.LogInfo($"Start\n{settings.ToString()}\n");

            // Reading the input data
            watch.InputPreparation.Start();
            if (settings.GenerateFunction)
            {
                PermutationsGenerator.Generate(settings.InputPath, settings.VariableCount);
                logger.LogInfo($"The permutations are written to a file: {settings.InputPath}");
            }
            string[] inputs = File.ReadAllLines(settings.InputPath);
            watch.InputPreparation.Stop();

            // Checking the input data for correctness
            watch.InputCheck.Start();
            if (!Validator.IsInputCorrect(inputs, settings, logger)) { return; }
            watch.InputCheck.Stop();

            // Creating an SBox
            watch.InputPreparation.Start();
            SBox sbox = new SBox(inputs, settings.VariableCount);
            watch.InputPreparation.Stop();

            // Calculation of difference characteristics

            if (settings.CalculateSequential)
            {
                watch.SequentialAlgorithm.Start();
                sbox.CalculateDifferentialCharacteristicsSequential();
                watch.SequentialAlgorithm.Stop();
                if (settings.IsNeedToWriteResult)
                    sbox.GetDifferentialCharacteristic().WriteToFile(settings.OutputPath, settings.WriteBuffer);
            }

            if (settings.CalculateParallel)
            {
                sbox = new SBox(inputs, settings.VariableCount);
                watch.ParallelAlgorithm.Start();
                sbox.CalculateDifferentialCharacteristicsParallel(settings.ThreadsCount);
                watch.ParallelAlgorithm.Stop();
                if (settings.IsNeedToWriteResult)
                    sbox.GetDifferentialCharacteristic().WriteToFile(settings.OutputPath, settings.WriteBuffer);
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
