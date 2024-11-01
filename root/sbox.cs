public class SBox
{

    //=================================================================================================================
    // Private variables
    //=================================================================================================================
    private Node[] _output;

    private int _variable_count;

    private Node[] _input;

    //=================================================================================================================
    // Constructors
    //=================================================================================================================


    public SBox(string[] output, int variable_count)
    {
        _variable_count = variable_count;
        var output_array = GenerateBooleanArrayFromStrings(output);
        var input_array = GenerateBooleanArrayFromPermutations(_variable_count);
        _output = Node.ConvertBooleanArrayToNode(output_array);
        _input = Node.ConvertBooleanArrayToNode(input_array);
    }


    //=================================================================================================================
    // Public
    //=================================================================================================================
    public bool[] getVectorResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].booleanArray;
    }

    public bool[] getVectorResult(int index)
    {
        return _output[index].booleanArray;
    }

    public int getNumberResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].decimalNumber;
    }

    public int getNumberResult(int index)
    {
        return _output[index].decimalNumber;
    }

    public string getStringResult(bool[] input)
    {
        return string.Join("", getVectorResult(input).Select(b => b ? '1' : '0'));
    }

    public string getStringResult(int index)
    {
        return string.Join("", getVectorResult(index).Select(b => b ? '1' : '0'));
    }

    //=================================================================================================================
    // Static
    //=================================================================================================================

    public static bool[][] GenerateBooleanArrayFromPermutations(int variables)
    {
        int rows = (int)Math.Pow(2, variables); // Вычисляем количество строк
        bool[][] result = new bool[rows][]; // Создаем двумерный массив

        // Генерируем все возможные перестановки
        for (int i = 0; i < rows; i++)
        {
            result[i] = new bool[variables];
            string binaryString = Convert.ToString(i, 2).PadLeft(variables, '0'); // Получаем бинарную строку
            for (int j = 0; j < variables; j++)
            {
                result[i][j] = binaryString[j] == '1'; // Добавляем в массив
            }
        }

        return result;
    }

    public static bool[][] GenerateBooleanArrayFromStrings(string[] binaryStrings)
    {
        // Определяем количество строк и столбцов
        int rows = binaryStrings.Length;
        int cols = binaryStrings[0].Length;

        // Создаем двумерный булевый массив
        bool[][] result = new bool[rows][];

        // Заполняем массив значениями из строк
        for (int i = 0; i < rows; i++)
        {
            result[i] = new bool[cols];
            for (int j = 0; j < cols; j++)
            {
                var ss = binaryStrings[i][j];
                result[i][j] = binaryStrings[i][j] == '1'; // Присваиваем true если '1', иначе false
            }
        }

        return result;
    }

    public static int ConvertBooleanArrayToDecimal(bool[] boolArray)
    {
        int decimalValue = 0;

        // Перебираем массив
        for (int i = 0; i < boolArray.Length; i++)
        {
            // Если элемент true, добавляем соответствующую степень двойки к результату
            if (boolArray[i])
            {
                decimalValue += (1 << (boolArray.Length - 1 - i)); // Бит сдвига
            }
        }

        return decimalValue;
    }

    //=================================================================================================================
    // Static
    //=================================================================================================================

    private class Node
    {
        public int decimalNumber { get; set; }
        public bool[] booleanArray { get; set; }

        public Node(bool[] _booleanArray)
        {
            booleanArray = _booleanArray;
            decimalNumber = ConvertBooleanArrayToDecimal(booleanArray);
        }

        public static Node[] ConvertBooleanArrayToNode(bool[][] input)
        {
            Node[] result = new Node[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                result[i] = new Node(input[i]);
            }
            return result;
        }
    }

}

