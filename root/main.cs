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

            // Чтение входных данных
            string[] inputs = File.ReadAllLines(settings.inputPath);

            // Проверка входных данных на корректность
            if (inputs.Length != settings.possibleInputsCount)
            {
                Console.WriteLine($"Ошибка: Входные данные должны содержать {settings.possibleInputsCount} строк, а не {inputs.Length}.");
                return;
            }

            HashSet<string> uniqueInputs = new HashSet<string>();

            foreach (var input in inputs)
            {
                if (!IsInputValid(input, settings.variableCount))
                {
                    Console.WriteLine($"Ошибка: Входные данные '{input}' некорректны. Ожидалось {settings.variableCount} переменных (0 или 1).");
                    return;
                }

                // Проверка на уникальность
                if (!uniqueInputs.Add(input))
                {
                    Console.WriteLine($"Ошибка: Входное значение '{input}' не уникально.");
                    return;
                }
            }

            // Запись результатов в выходной файл
            var results = new string[settings.possibleInputsCount];

            for (int i = 0; i < settings.possibleInputsCount; i++)
            {
                results[i] = $"Вектор {Convert.ToString(i, 2).PadLeft(settings.variableCount, '0')} => {inputs[i]}";
            }

            File.WriteAllLines(settings.outputPath, results);

            watch.allProgram.Stop();
            logger.LogInfo($"Execution time: {watch.allProgram.Elapsed.TotalSeconds} seconds");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }

    static bool IsInputValid(string input, int variableCount)
    {
        // Проверка на количество символов и допустимость только '0' и '1'
        return input.Length == variableCount && input.All(c => c == '0' || c == '1');
    }
}
