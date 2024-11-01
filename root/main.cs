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
            // Чтение конфигурации
            var settings = new Settings();

            // Количество возможных входных векторов
            int possibleInputsCount = (int)Math.Pow(2, settings.variableCount);

            // Логирование
            using (StreamWriter logWriter = new StreamWriter(settings.logPath, true))
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                
                // Чтение входных данных
                string[] inputs = File.ReadAllLines(settings.inputPath);

                // Проверка входных данных на корректность
                if (inputs.Length != possibleInputsCount)
                {
                    Console.WriteLine($"Ошибка: Входные данные должны содержать {possibleInputsCount} строк, а не {inputs.Length}.");
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
                var results = new string[possibleInputsCount];

                for (int i = 0; i < possibleInputsCount; i++)
                {
                    results[i] = $"Вектор {Convert.ToString(i, 2).PadLeft(settings.variableCount, '0')} => {inputs[i]}";
                }

                File.WriteAllLines(settings.outputPath, results);
                
                stopwatch.Stop();
                logWriter.WriteLine($"Execution time: {stopwatch.Elapsed.TotalSeconds} seconds");
            }
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
