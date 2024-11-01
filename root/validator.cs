public static class Validator
{

    public static bool isInputCorrect(string[] inputs, Settings settings, Logger logger)
    {

        if (inputs.Length != settings.possibleInputsCount)
        {
            logger.LogError($"Ошибка: Входные данные должны содержать {settings.possibleInputsCount} строк, а не {inputs.Length}.");
            return false;
        }

        HashSet<string> uniqueInputs = new HashSet<string>();

        foreach (var input in inputs)
        {
            if (!IsInputValid(input, settings.variableCount))
            {
                Console.WriteLine($"Ошибка: Входные данные '{input}' некорректны. Ожидалось {settings.variableCount} переменных (0 или 1).");
                return false;
            }

            // Проверка на уникальность
            if (!uniqueInputs.Add(input))
            {
                Console.WriteLine($"Ошибка: Входное значение '{input}' не уникально.");
                return false;
            }
        }

        return true;

    }


    static bool IsInputValid(string input, int variableCount)
    {
        // Проверка на количество символов и допустимость только '0' и '1'
        return input.Length == variableCount && input.All(c => c == '0' || c == '1');
    }

}