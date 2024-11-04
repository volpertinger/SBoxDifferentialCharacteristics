using System.Text;

/// <summary>
/// A class for formatting two-dimensional arrays and writing them to file
/// </summary>
public static class MatrixWriter
{

    /// <summary>
    /// Writes matrix in string format to file
    /// </summary>
    public static void WriteMatrix(int[][] matrix, int padding, string path, int buffer)
    {
        using StreamWriter writer = new(path);
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            writer.WriteLine("");
            return;
        }

        int rows_number = matrix.Length;
        int cols_number = matrix[0].Length;

        StringBuilder result = new();

        // Upper delimeter
        GetRowDelimiter(result, cols_number, padding);
        result.AppendLine();
        GetColumnIndices(result, cols_number, padding);
        writer.WriteLine(result.ToString());
        result.Clear();

        // Matrix print
        for (int i = 0; i < rows_number; ++i)
        {
            result.Append($"{i.ToString().PadLeft(padding)}|");
            for (int j = 0; j < cols_number; ++j)
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

        // Bottom separator
        GetRowDelimiter(result, cols_number, padding);
        writer.Write(result);
        return;
    }

    /// <summary>
    /// Returns row delimeter
    /// </summary>
    private static void GetRowDelimiter(StringBuilder result, int size, int padding)
    {
        result.Append(new string('-', padding));
        result.Append('+');

        for (int j = 0; j < size; j++)
        {
            result.Append(new string('-', padding));
            result.Append('+');
        }
    }

    /// <summary>
    /// Returns columns indexes
    /// </summary>
    private static void GetColumnIndices(StringBuilder result, int numCols, int padding)
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
        GetRowDelimiter(result, numCols, padding);
    }
}


/// <summary>
/// Boolean function generation
/// </summary>
public static class PermutationsGenerator
{
    public static void Generate(string path, int length)
    {
        // Generating all possible permutations
        List<string> permutations = GenerateBinaryPermutations(length);

        // Shuffling permutations
        Random rand = new();
        var shuffledPermutations = permutations.OrderBy(x => rand.Next()).ToList();

        // Writing to a file
        File.WriteAllLines(path, shuffledPermutations);
        return;
    }

    private static List<string> GenerateBinaryPermutations(int length)
    {
        List<string> result = [];
        int totalPerms = (int)Math.Pow(2, length);

        for (int i = 0; i < totalPerms; i++)
        {
            string binaryString = Convert.ToString(i, 2).PadLeft(length, '0');
            result.Add(binaryString);
        }

        return result;
    }
}