public class SBox
{

    //=================================================================================================================
    // Private variables
    //=================================================================================================================
    /// <summary>
    /// Массив значений функции
    /// </summary>
    private Node[] _output;

    /// <summary>
    /// Число переменных функции
    /// </summary>
    private int _variable_count;

    /// <summary>
    /// Массив возможных входных векторов функции
    /// </summary>
    private Node[] _input;

    private DifferentialCharacteristic _differentialCharacteristic;

    //=================================================================================================================
    // Constructors
    //=================================================================================================================


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="output">Массив значений функции, вектор представлен в строковом виде</param>
    /// <param name="variable_count">Количество переменных функции</param>
    public SBox(string[] output, int variable_count)
    {
        _variable_count = variable_count;

        // Получение булевых массивов из строк
        var output_array = GenerateBooleanArrayFromStrings(output);
        var input_array = GenerateBooleanArrayFromPermutations(_variable_count);

        // Конвертация булевых массивов в массива класса Node
        _output = Node.ConvertBooleanArrayToNode(output_array);
        _input = Node.ConvertBooleanArrayToNode(input_array);

        // Инициализация пустых разностных характеристик
        _differentialCharacteristic = new DifferentialCharacteristic(variable_count);
    }


    //=================================================================================================================
    // Public
    //=================================================================================================================
    /// <summary>
    /// Получение значения функции по входному значению
    /// </summary>
    /// <param name="input">Значение входа в векторном представлении</param>
    /// <returns>Значение функции в векторном представлении</returns>
    public bool[] getVectorResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].booleanArray;
    }

    /// <summary>
    /// Получение значения функции по входному значению
    /// </summary>
    /// <param name="index">Значение входа в десятичном представлении</param>
    /// <returns>Значение функции в векторном представлении</returns>
    public bool[] getVectorResult(int index)
    {
        return _output[index].booleanArray;
    }

    /// <summary>
    /// Получение значения функции по входному значению
    /// </summary>
    /// <param name="input">Значение входа в векторном представлении</param>
    /// <returns>Значение функции в десятичном представлении</returns>
    public int getNumberResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].decimalNumber;
    }

    /// <summary>
    /// Получение значения функции по входному значению
    /// </summary>
    /// <param name="index">Значение входа в десятичном представлении</param>
    /// <returns>Значение функции в десятичном представлении</returns>
    public int getNumberResult(int index)
    {
        return _output[index].decimalNumber;
    }

    /// <summary>
    /// Получение значения функции по входному значению
    /// </summary>
    /// <param name="input">Значение входа в векторном представлении</param>
    /// <returns>Значение функции в строковом представлении</returns>
    public string getStringResult(bool[] input)
    {
        return string.Join("", getVectorResult(input).Select(b => b ? '1' : '0'));
    }

    /// <summary>
    /// Получение значения функции по входному значению
    /// </summary>
    /// <param name="index">Значение входа в десятичном представлении</param>
    /// <returns>Значение функции в строковом представлении</returns>
    public string getStringResult(int index)
    {
        return string.Join("", getVectorResult(index).Select(b => b ? '1' : '0'));
    }

    public DifferentialCharacteristic GetDifferentialCharacteristic()
    {
        return _differentialCharacteristic;
    }

    //=================================================================================================================
    // Static
    //=================================================================================================================

    /// <summary>
    /// Генерирует все возможные перестановки 0 и 1 в векторном виде длниы variables
    /// </summary>
    /// <param name="variables">Число переменных функции, длина векторов перестановок</param>
    /// <returns>Двумерный массив всех возможных перестановок 0 и 1</returns>
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

    /// <summary>
    /// Генерирует массив булевых векторов по массиву строк
    /// </summary>
    /// <param name="binaryStrings">Строки, представляющие собой булевый вектор и состоящие из 0 и 1 без пробелов</param>
    /// <returns>Двумерный булевый массив</returns>
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

    /// <summary>
    /// Переводит булевый вектор в соответствующее числовое значение
    /// </summary>
    /// <param name="boolArray">Булевый массив</param>
    /// <returns>Десятичное представление булевого массива</returns>
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
    // Differential Characteristics Calculation
    //=================================================================================================================

    public void calculateDifferentialCharacteristicsSequential()
    {
        return;
    }

    public void calculateDifferentialCharacteristicsParallel(int threads)
    {
        return;
    }


    //=================================================================================================================
    // Node
    //=================================================================================================================

    /// <summary>
    /// Класс, представляющий собой булевый вектор в S box
    /// </summary>
    private class Node
    {
        /// <summary>
        /// Десятичное представление булевого вектора
        /// </summary>
        public int decimalNumber { get; set; }

        /// <summary>
        /// Булевый вектор
        /// </summary>
        public bool[] booleanArray { get; set; }

        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        /// <param name="_booleanArray">Булевый массив</param>
        public Node(bool[] _booleanArray)
        {
            booleanArray = _booleanArray;
            decimalNumber = ConvertBooleanArrayToDecimal(booleanArray);
        }

        /// <summary>
        /// Конвертирует двумерный булевый массив в массив класса Node
        /// </summary>
        /// <param name="input">Двумерный булевый массив</param>
        /// <returns>Массив класса Node</returns>
        public static Node[] ConvertBooleanArrayToNode(bool[][] input)
        {
            Node[] result = new Node[input.Length];
            for (int i = 0; i < input.Length; ++i)
            {
                result[i] = new Node(input[i]);
            }
            return result;
        }

        public static int GetDiff(Node lhs, Node rhs)
        {
            int result = 0;
            for (int i = 0; i < lhs.booleanArray.Length; ++i)
            {
                result += (lhs.booleanArray[i] ^ rhs.booleanArray[i]) ? 1 : 0;
            }
            return result;
        }
    }

    //=================================================================================================================
    // Differential Characteristics
    //=================================================================================================================

    /// <summary>
    /// Класс, представляющий собой разностные характеристики S box, а также возможности по вычислению этих характеристик
    /// </summary>
    public class DifferentialCharacteristic
    {

        /// <summary>
        /// Двумерный массив с разностынми характеристиками
        /// </summary>
        public int[][] differentialCharacteristic { get; private set; }

        /// <summary>
        /// Число символов для выравнивания строкового представления массива
        /// </summary>
        private int padLength;

        /// <summary>
        /// Инициализация пустого массива разностных характеристик
        /// </summary>
        /// <param name="variable_count">Число епеременных булевой функции</param>
        public DifferentialCharacteristic(int variable_count)
        {
            var length = (int)Math.Pow(2, variable_count);
            differentialCharacteristic = new int[length][];
            for (int i = 0; i < length; ++i)
            {
                differentialCharacteristic[i] = new int[length];
            }

            padLength = СountDigits(length);
        }

        /// <summary>
        /// Представляет двумерный массив в человекочитаемом виде
        /// </summary>
        /// <returns>Строковое представление двумерного массива</returns>
        public override string ToString()
        {
            return MatrixFormatter.FormatMatrix(differentialCharacteristic, padLength);
        }

        private static int СountDigits(int number)
        {
            // Приводим число к положительному значению
            number = Math.Abs(number);

            // Проверка, является ли число нулем
            if (number == 0)
            {
                return 1; // Если число равно нулю, то цифр 1
            }

            // Используем логарифм для подсчета количества целых цифр
            return (int)Math.Floor(Math.Log10(number)) + 1;
        }

    }

}

