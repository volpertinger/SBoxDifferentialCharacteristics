using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


/// <summary>
/// Класс для форматирования двумерных массивов
/// </summary>
public static class MatrixFormatter
{

    /// <summary>
    /// Returns matrix in string format
    /// </summary>
    public static void FormatMatrix(int[][] matrix, int padding, string path, int buffer)
    {

        // Запись строки в файл с помощью StreamWriter
        using (StreamWriter writer = new StreamWriter(path))
        {
            if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            {
                writer.WriteLine("");
                return;
            }

            int numRows = matrix.Length;
            int numCols = matrix[0].Length;

            StringBuilder result = new StringBuilder();

            // Печатаем верхний разделитель
            PrintDelimiter(result, numCols, padding);
            result.AppendLine();
            // Печатаем индексы столбцов
            PrintColumnIndices(result, numCols, padding);
            writer.WriteLine(result.ToString());
            result.Clear();

            // Печатаем каждую строку матрицы с индексами строк
            for (int i = 0; i < numRows; i++)
            {
                result.Append($"{i.ToString().PadLeft(padding)}|");
                for (int j = 0; j < numCols; j++)
                {
                    result.Append($"{matrix[i][j].ToString().PadLeft(padding)} ");
                }
                result.AppendLine();
                if (result.Length > buffer)
                {
                    writer.Write(result);
                    result.Clear();
                }
            }

            // Печатаем нижний разделитель
            PrintDelimiter(result, numCols, padding);
            writer.Write(result);
            return;
        }

    }

    /// <summary>
    /// Returns row delimeter
    /// </summary>
    private static void PrintDelimiter(StringBuilder result, int numCols, int padding)
    {
        result.Append(new string('-', padding)); // Печатаем отступ
        result.Append("+");

        for (int j = 0; j < numCols; j++)
        {
            result.Append(new string('-', padding)); // Печатаем ячейки разделителя
            result.Append("+");
        }
    }

    /// <summary>
    /// Returns columns indexes
    /// </summary>
    private static void PrintColumnIndices(StringBuilder result, int numCols, int padding)
    {
        result.Append($"{"dy".PadLeft(padding)}|");

        for (int j = 0; j < numCols; j++)
        {
            result.Append($"{j.ToString().PadLeft(padding)}|");
        }

        result.AppendLine();
        result.Append($"{"dx".PadLeft(padding)}|");
        for (int j = 0; j < numCols; j++)
        {
            result.Append($"{" ".PadLeft(padding)}|");
        }
        result.AppendLine();
        PrintDelimiter(result, numCols, padding);
    }
}


/// <summary>
/// Генерация S box
/// </summary>
public static class PermutationsGenerator
{
    public static void generate(string path, int length)
    {
        // Генерация всех возможных перестановок
        List<string> permutations = GenerateBinaryPermutations(length);

        // Перемешивание перестановок
        Random rand = new Random();
        var shuffledPermutations = permutations.OrderBy(x => rand.Next()).ToList();

        // Запись в файл
        File.WriteAllLines(path, shuffledPermutations);

        Console.WriteLine($"Перестановки записаны в файл: {path}");
    }

    static List<string> GenerateBinaryPermutations(int length)
    {
        List<string> result = new List<string>();
        int totalPerms = (int)Math.Pow(2, length);

        for (int i = 0; i < totalPerms; i++)
        {
            // Генерация двоичной строки
            string binaryString = Convert.ToString(i, 2).PadLeft(length, '0');
            result.Add(binaryString);
        }

        return result;
    }
}