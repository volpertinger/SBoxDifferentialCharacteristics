using System.Text;
/// <summary>
/// Класс для форматирования двумерных массивов
/// </summary>
public static class MatrixFormatter
{

    /// <summary>
    /// Returns matrix in string format
    /// </summary>
    public static string FormatMatrix(int[][] matrix, int padding)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
            return "";

        int numRows = matrix.Length;
        int numCols = matrix[0].Length;

        StringBuilder result = new StringBuilder();

        // Печатаем верхний разделитель
        PrintDelimiter(result, numCols, padding);

        // Печатаем индексы столбцов
        PrintColumnIndices(result, numCols, padding);

        // Печатаем каждую строку матрицы с индексами строк
        for (int i = 0; i < numRows; i++)
        {
            result.Append($"{i.ToString().PadLeft(padding)}|");
            for (int j = 0; j < numCols; j++)
            {
                result.Append($"{matrix[i][j].ToString().PadLeft(padding)}:");
            }
            result.AppendLine();
        }

        // Печатаем нижний разделитель
        PrintDelimiter(result, numCols, padding);

        return result.ToString();
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

        result.AppendLine();
    }

    /// <summary>
    /// Returns columns indexes
    /// </summary>
    private static void PrintColumnIndices(StringBuilder result, int numCols, int padding)
    {
        result.Append("dy|");

        for (int j = 0; j < numCols; j++)
        {
            result.Append($"{j.ToString().PadLeft(padding)}|");
        }

        result.Append("\ndx|");
        for (int j = 0; j < numCols; j++)
        {
            result.Append($"{" ".PadLeft(padding)}|");
        }
        result.AppendLine();
        PrintDelimiter(result, numCols, padding);
    }
}