public class SBox
{

    //=================================================================================================================
    // Private variables
    //=================================================================================================================
    /// <summary>
    /// Array of function values
    /// </summary>
    private readonly Node[] _output;

    /// <summary>
    /// Array of possible input vectors of the function
    /// </summary>
    private readonly Node[] _input;

    /// <summary>
    /// Number of function variables
    /// </summary>
    private readonly int _variable_count;

    /// <summary>
    /// Differential characteristics
    /// </summary>
    private readonly DifferentialCharacteristic _differentialCharacteristic;

    //=================================================================================================================
    // Constructors
    //=================================================================================================================


    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="output">An array of function values, the vector is represented in string form</param>
    /// <param name="variable_count">Number of function variables</param>
    public SBox(string[] output, int variable_count)
    {
        _variable_count = variable_count;

        // Getting Boolean arrays from strings
        var output_array = GenerateBooleanArrayFromStrings(output);
        var input_array = GenerateBooleanArrayFromPermutations(_variable_count);

        // Converting Boolean arrays in an array of the Node class
        _output = Node.ConvertBooleanArrayToNode(output_array);
        _input = Node.ConvertBooleanArrayToNode(input_array);

        // Initialization of empty difference characteristics
        _differentialCharacteristic = new DifferentialCharacteristic(variable_count);
    }

    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="output">An array of function values, the vector is represented in two-dimensional boolean array form</param>
    /// <param name="variable_count">Number of function variables</param>
    public SBox(bool[][] output, int variable_count)
    {
        _variable_count = variable_count;
        var input_array = GenerateBooleanArrayFromPermutations(_variable_count);

        _output = Node.ConvertBooleanArrayToNode(output);
        _input = Node.ConvertBooleanArrayToNode(input_array);

        _differentialCharacteristic = new DifferentialCharacteristic(variable_count);
    }

    /// <summary>
    /// Copy Constructor
    /// </summary>
    public SBox(SBox sbox)
    {
        _variable_count = sbox._variable_count;

        // Converting Boolean arrays in an array of the Node class
        _output = sbox._output;
        _input = sbox._input;

        // Initialization of empty difference characteristics
        _differentialCharacteristic = new DifferentialCharacteristic(_variable_count);
    }


    //=================================================================================================================
    // Public
    //=================================================================================================================

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    public int GetNumberResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].Value;
    }

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    public int GetNumberResult(int index)
    {
        return _output[index].Value;
    }

    public DifferentialCharacteristic GetDifferentialCharacteristic()
    {
        return _differentialCharacteristic;
    }

    //=================================================================================================================
    // Static
    //=================================================================================================================

    /// <summary>
    /// Generates all possible permutations of 0 and 1 in vector form of length variables
    /// </summary>
    /// <param name="variables">The number of variables of the function, the length of the permutation vectors</param>
    /// <returns>A two-dimensional array of all possible permutations of 0 and 1</returns>
    public static bool[][] GenerateBooleanArrayFromPermutations(int variables)
    {
        int rows = (int)Math.Pow(2, variables);
        bool[][] result = new bool[rows][];

        // Generating all possible permutations
        for (int i = 0; i < rows; i++)
        {
            result[i] = new bool[variables];
            string binaryString = Convert.ToString(i, 2).PadLeft(variables, '0');
            for (int j = 0; j < variables; j++)
            {
                result[i][j] = binaryString[j] == '1';
            }
        }

        return result;
    }

    /// <summary>
    /// Generates an array of Boolean vectors based on an array of strings
    /// </summary>
    /// <param name="binaryStrings">Strings representing a Boolean vector and consisting of 0 and 1 without spaces</param>
    /// <returns>Two-dimensional Boolean array</returns>
    public static bool[][] GenerateBooleanArrayFromStrings(string[] binaryStrings)
    {
        int rows = binaryStrings.Length;
        int cols = binaryStrings[0].Length;
        bool[][] result = new bool[rows][];

        // Filling the array with values from the strings
        for (int i = 0; i < rows; i++)
        {
            result[i] = new bool[cols];
            for (int j = 0; j < cols; j++)
            {
                result[i][j] = binaryStrings[i][j] == '1';
            }
        }

        return result;
    }

    /// <summary>
    /// Converts a Boolean vector to the corresponding numeric value
    /// </summary>
    /// <param name="boolArray">Boolean array</param>
    /// <returns>Decimal representation of a Boolean array</returns>
    public static int ConvertBooleanArrayToDecimal(bool[] boolArray)
    {
        int decimalValue = 0;

        for (int i = 0; i < boolArray.Length; i++)
        {
            if (boolArray[i])
            {
                decimalValue += (1 << (boolArray.Length - 1 - i));
            }
        }

        return decimalValue;
    }

    /// <summary>
    /// Splits the array into split_count of almost equal parts
    /// </summary>
    private static Node[][] SplitArray(Node[] array, int split_count)
    {
        if (split_count <= 0)
            throw new ArgumentException($"split_count must be grater than 0, {split_count} <= 0");

        int totalLength = array.Length;
        int partLength = totalLength / split_count; // The length of each part
        int remainder = totalLength % split_count; // Remains

        // Splitting an array using LINQ
        return Enumerable.Range(0, split_count)
                         .Select(i => array.Skip(i * partLength).Take(partLength + (i == split_count - 1 ? remainder : 0)).ToArray())
                         .ToArray();
    }

    //=================================================================================================================
    // Differential Characteristics Calculation
    //=================================================================================================================

    /// <summary>
    /// Calculates differential characteristics of S Box sequential
    /// </summary>
    public void CalculateDifferentialCharacteristicsSequential()
    {
        _differentialCharacteristic.Matrix[0][0] = _input.Length;
        for (int i = 0; i < _input.Length; ++i)
        {
            for (int j = i + 1; j < _input.Length; ++j)
            {
                var input_diff = Node.GetDiff(_input[i], _input[j]);
                var output_diff = Node.GetDiff(_output[i], _output[j]);
                _differentialCharacteristic.IncrementDifference(input_diff, output_diff, 2);
            }
        }
        return;
    }

    /// <summary>
    /// Calculates differential characteristics of S Box sequential with granular algorithm for parallel algorithm
    /// </summary>
    public void CalculateDifferentialCharacteristicsSequentialGranular(int left_border, int right_border)
    {
        for (int i = left_border; i <= right_border; ++i)
        {
            for (int j = i + 1; j < _input.Length; ++j)
            {
                var input_diff = Node.GetDiff(_input[i], _input[j]);
                var output_diff = Node.GetDiff(_output[i], _output[j]);
                _differentialCharacteristic.IncrementDifference(input_diff, output_diff, 2);
            }
        }
        return;
    }

    /// <summary>
    /// Calculates differential characteristics of S Box parallel
    /// </summary>
    /// <param name="threads">Threads count</param>
    public void CalculateDifferentialCharacteristicsParallel(int threads)
    {
        
        var splitted_input = SplitArray(_input, threads);
        var result = new SBox[threads];

        // Parallel output of arrays
        Parallel.For(0, splitted_input.Length, i =>
        {
            var sbox = new SBox(this);
            sbox.CalculateDifferentialCharacteristicsSequentialGranular(splitted_input[i][0].Value, splitted_input[i][^1].Value);
            result[i] = sbox;
        });

        var length = _differentialCharacteristic.Matrix.Length;
        for (int i = 0; i < threads; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                for (int k = 0; k < length; ++k)
                {
                    _differentialCharacteristic.Matrix[j][k] += result[i]._differentialCharacteristic.Matrix[j][k];
                }
            }
        }
        _differentialCharacteristic.Matrix[0][0] = length;
        return;
    }


    //=================================================================================================================
    // Node
    //=================================================================================================================

    /// <summary>
    /// A class representing a Boolean vector in the S box
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Decimal representation of a Boolean vector
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// Basic class constructor from boolean array
        /// </summary>
        public Node(bool[] booleanArray)
        {
            Value = ConvertBooleanArrayToDecimal(booleanArray);
        }

        /// <summary>
        /// Basic class constructor from int number
        /// </summary>
        public Node(int value)
        {
            Value = value;
        }

        /// <summary>
        /// Converts a two-dimensional Boolean array to an array of the Node class
        /// </summary>
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
            return lhs.Value ^ rhs.Value;
        }
    }

    //=================================================================================================================
    // Differential Characteristics
    //=================================================================================================================

    /// <summary>
    /// A class representing the difference characteristics of the S box and calculating these characteristics
    /// </summary>
    public class DifferentialCharacteristic
    {

        /// <summary>
        /// A two-dimensional array with difference characteristics
        /// </summary>
        public int[][] Matrix { get; private set; }

        /// <summary>
        /// The number of characters to align the string representation of the array
        /// </summary>
        public int Padding { get; private set; }

        /// <summary>
        /// Initializing an empty array of difference characteristics
        /// </summary>
        /// <param name="variable_count">The number of variables of a boolean function</param>
        public DifferentialCharacteristic(int variable_count)
        {
            var length = (int)Math.Pow(2, variable_count);
            Matrix = new int[length][];
            for (int i = 0; i < length; ++i)
            {
                Matrix[i] = new int[length];
            }

            // +1 due to the use of a separator
            Padding = СountDigits(length) + 1;
        }

        public void IncrementDifference(int input_diff, int output_diff, int value = 1)
        {
            Matrix[input_diff][output_diff] += value;
        }

        /// <summary>
        /// Writes a two-dimensional array in human-readable form to file
        /// </summary>
        public void WriteToFile(string path, int buffer)
        {
            MatrixWriter.WriteMatrix(Matrix, Padding, path, buffer);
        }

        private static int СountDigits(int number)
        {
            number = Math.Abs(number);

            if (number == 0)
            {
                return 1;
            }

            return (int)Math.Floor(Math.Log10(number)) + 1;
        }

    }

}
