/// <summary>
/// A class for validating input data
/// </summary>
public static class Validator
{

    /// <summary>
    /// Checking the correctness of the input data: a Boolean array represented as an array of strings of 0 and 1
    /// </summary>
    /// <param name="inputs">A Boolean array represented as an array of strings of 0 and 1</param>
    /// <param name="settings">An instance of a class with settings read from a file</param>
    /// <param name="logger">An instance of the logger class</param>
    public static bool isInputCorrect(string[] inputs, Settings settings, Logger logger)
    {

        // Checking the size of an array of Boolean vectors to match the required number for a given number of variables
        if (inputs.Length != settings.possibleInputsCount)
        {
            logger.LogError($"Mistake: The input data should contain a {settings.possibleInputsCount} string, not {inputs.Length}.");
            return false;
        }

        // Checking each row of the input array for correctness and uniqueness
        HashSet<string> uniqueInputs = new HashSet<string>();
        foreach (var input in inputs)
        {
            // Checking for correctness
            if (!IsStringVectorValid(input, settings.variableCount))
            {
                Console.WriteLine($"Mistake: The input data '{input}' is incorrect. Expected {settings.variableCount} of variables (0 or 1).");
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
    /// <param name="input">A Boolean vector represented as a string</param>
    /// <param name="variableCount">Number of function variables</param>
    static bool IsStringVectorValid(string input, int variableCount)
    {
        // Checking for the number of characters and the validity of only '0' and '1'
        return input.Length == variableCount && input.All(c => c == '0' || c == '1');
    }

}