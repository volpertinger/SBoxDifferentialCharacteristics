using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

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
            var settings = new Settings();

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

            // Запись результатов в выходной файл
            watch.mainAlgorithm.Start();
            var results = new string[settings.possibleInputsCount];

            for (int i = 0; i < settings.possibleInputsCount; i++)
            {
                var ss = sbox.getVectorResult(i);
                results[i] = sbox.getStringResult(i);
            }

            File.WriteAllLines(settings.outputPath, results);

            watch.StopAll();
            logger.LogInfo($"\nExecution time [allProgram]: {watch.allProgram.Elapsed.TotalSeconds} second\n" +
            $"Execution time [inputCheck]: {watch.inputCheck.Elapsed.TotalSeconds} seconds\n" +
            $"Execution time [inputPreparation]: {watch.inputPreparation.Elapsed.TotalSeconds} seconds\n" +
            $"Execution time [mainAlgorithm]: {watch.mainAlgorithm.Elapsed.TotalSeconds} seconds\n"
            );

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
