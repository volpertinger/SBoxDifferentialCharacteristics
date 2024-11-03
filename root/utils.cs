using System.Text;

/// <summary>
/// A class for formatting two-dimensional arrays
/// </summary>
public static class MatrixFormatter
{

    /// <summary>
    /// Returns a matrix in string format
    /// </summary>
    public static void FormatMatrix(int[][] matrix, int padding, string path, int buffer)
    {

        // Writing a line to a file using StreamWriter
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

            // We print the upper separator
            PrintDelimiter(result, numCols, padding);
            result.AppendLine();
            // Printing column indexes
            PrintColumnIndices(result, numCols, padding);
            writer.WriteLine(result.ToString());
            result.Clear();

            // We print each row of the matrix with row indexes
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

            // We print the bottom separator
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
        result.Append(new string('-', padding)); // We print the indentation
        result.Append("+");

        for (int j = 0; j < numCols; j++)
        {
            result.Append(new string('-', padding)); // Printing the separator cells
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
/// S box generation
/// </summary>
public static class PermutationsGenerator
{
    public static void generate(string path, int length)
    {
        // Generating all possible permutations
        List<string> permutations = GenerateBinaryPermutations(length);

        // Shuffling permutations
        Random rand = new Random();
        var shuffledPermutations = permutations.OrderBy(x => rand.Next()).ToList();

        // Writing to a file
        File.WriteAllLines(path, shuffledPermutations);

        Console.WriteLine($"The permutations are written to a file: {path}");
    }

    static List<string> GenerateBinaryPermutations(int length)
    {
        List<string> result = new List<string>();
        int totalPerms = (int)Math.Pow(2, length);

        for (int i = 0; i < totalPerms; i++)
        {
            // Generating a binary string
            string binaryString = Convert.ToString(i, 2).PadLeft(length, '0');
            result.Add(binaryString);
        }

        return result;
    }
}