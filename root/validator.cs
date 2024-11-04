/// <summary>
/// A class for validating input data
/// </summary>
public static class Validator
{

    /// <summary>
    /// Checking the correctness of the input data: a Boolean array represented as an array of strings of 0 and 1
    /// </summary>
    public static bool IsInputCorrect(string[] inputs, Settings settings, Logger logger)
    {

        // Checking the size of an array of Boolean vectors to match the required number for a given number of variables
        if (inputs.Length != settings.InputsCount)
        {
            logger.LogError($"Mistake: The input data should contain a {settings.InputsCount} string, not {inputs.Length}.");
            return false;
        }

        // Checking each row of the input array for correctness and uniqueness
        HashSet<string> uniqueInputs = [];
        foreach (var input in inputs)
        {
            // Checking for correctness
            if (!IsStringVectorValid(input, settings.VariableCount))
            {
                Console.WriteLine($"Mistake: The input data '{input}' is incorrect. Expected {settings.VariableCount} of variables (0 or 1).");
                return false;
            }

            // Checking for uniqueness
            if (!uniqueInputs.Add(input))
            {
                Console.WriteLine($"Mistake: The input value of '{input}' is not unique.");
                return false;
            }
        }

        return true;

    }

    /// <summary>
    /// Checking the input string vector for correctness for the Boolean representation of the vector
    /// </summary>
    static bool IsStringVectorValid(string input, int variableCount)
    {
        return input.Length == variableCount && input.All(c => c == '0' || c == '1');
    }

}