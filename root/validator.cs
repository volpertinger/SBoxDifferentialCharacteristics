/// <summary>
/// Класс для валидации входных данных
/// </summary>
public static class Validator
{

    /// <summary>
    /// Проверка корректности входных данных: булевого массива, представленного в виде массива строк из 0 и 1
    /// </summary>
    /// <param name="inputs">Булевый массив, представленный в виде массива строк из 0 и 1</param>
    /// <param name="settings">Экземпляр класса с настройками, считанными из файла</param>
    /// <param name="logger">Экземпляр класса логгера</param>
    /// <returns></returns>
    public static bool isInputCorrect(string[] inputs, Settings settings, Logger logger)
    {

        // Проверка размера массива булевых векторов на соответствие необходимому количествву при заданном числе переменных
        if (inputs.Length != settings.possibleInputsCount)
        {
            logger.LogError($"Ошибка: Входные данные должны содержать {settings.possibleInputsCount} строк, а не {inputs.Length}.");
            return false;
        }

        // Проверка каждой строки входного массива на корректность и уникальность
        HashSet<string> uniqueInputs = new HashSet<string>();
        foreach (var input in inputs)
        {
            // проверка на корректность
            if (!IsStringVectorValid(input, settings.variableCount))
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

    /// <summary>
    /// Проверка входного строкового вектора на корректность для булевого представления вектора
    /// </summary>
    /// <param name="input">Булевый вектор, представленный в виде строки</param>
    /// <param name="variableCount">Число переменных функции</param>
    /// <returns></returns>
    static bool IsStringVectorValid(string input, int variableCount)
    {
        // Проверка на количество символов и допустимость только '0' и '1'
        return input.Length == variableCount && input.All(c => c == '0' || c == '1');
    }

}