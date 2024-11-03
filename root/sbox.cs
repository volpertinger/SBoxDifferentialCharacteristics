public class SBox
{

    //=================================================================================================================
    // Private variables
    //=================================================================================================================
    /// <summary>
    /// Array of function values
    /// </summary>
    private Node[] _output;

    /// <summary>
    /// Number of function variables
    /// </summary>
    private int _variable_count;

    /// <summary>
    /// Array of possible input vectors of the function
    /// </summary>
    private Node[] _input;

    private DifferentialCharacteristic _differentialCharacteristic;

    //=================================================================================================================
    // Constructors
    //=================================================================================================================


    /// <summary>
    /// Constructor
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
    /// Copy Constructor
    /// </summary>
    public SBox(SBox sbox, Node[] input)
    {
        _variable_count = sbox._variable_count;

        // Converting Boolean arrays in an array of the Node class
        _output = sbox._output;
        _input = input;

        // Initialization of empty difference characteristics
        _differentialCharacteristic = new DifferentialCharacteristic(_variable_count);
    }


    //=================================================================================================================
    // Public
    //=================================================================================================================
    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    /// <param name="input">The value of the input in the vector representation</param>
    /// <returns>The value of a function in a vector representation</returns>
    public bool[] getVectorResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].booleanArray;
    }

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    /// <param name="index">The value of the input in decimal representation</param>
    /// <returns>The value of a function in a vector representation</returns>
    public bool[] getVectorResult(int index)
    {
        return _output[index].booleanArray;
    }

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    /// <param name="input">The value of the input in the vector representation</param>
    /// <returns>The value of the function in decimal representation</returns>
    public int getNumberResult(bool[] input)
    {
        return _output[ConvertBooleanArrayToDecimal(input)].decimalNumber;
    }

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    /// <param name="index">The value of the input in decimal representation</param>
    /// <returns>The value of the function in decimal representation</returns>
    public int getNumberResult(int index)
    {
        return _output[index].decimalNumber;
    }

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    /// <param name="input">The value of the input in the vector representation</param>
    /// <returns>The value of the function in the string representation</returns>
    public string getStringResult(bool[] input)
    {
        return string.Join("", getVectorResult(input).Select(b => b ? '1' : '0'));
    }

    /// <summary>
    /// Getting the function value from the input value
    /// </summary>
    /// <param name="index">The value of the input in decimal representation</param>
    /// <returns>The value of the function in the string representation</returns>
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
    /// Generates all possible permutations of 0 and 1 in vector form of length variables
    /// </summary>
    /// <param name="variables">The number of variables of the function, the length of the permutation vectors</param>
    /// <returns>A two-dimensional array of all possible permutations of 0 and 1</returns>
    public static bool[][] GenerateBooleanArrayFromPermutations(int variables)
    {
        int rows = (int)Math.Pow(2, variables); // Calculating the number of rows
        bool[][] result = new bool[rows][]; // Creating a two-dimensional array

        // Generating all possible permutations
        for (int i = 0; i < rows; i++)
        {
            result[i] = new bool[variables];
            string binaryString = Convert.ToString(i, 2).PadLeft(variables, '0'); // We get a binary string
            for (int j = 0; j < variables; j++)
            {
                result[i][j] = binaryString[j] == '1'; // Adding to the array
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
        // Determine the number of rows and columns
        int rows = binaryStrings.Length;
        int cols = binaryStrings[0].Length;

        // Creating a two-dimensional Boolean array
        bool[][] result = new bool[rows][];

        // Filling the array with values from the strings
        for (int i = 0; i < rows; i++)
        {
            result[i] = new bool[cols];
            for (int j = 0; j < cols; j++)
            {
                result[i][j] = binaryStrings[i][j] == '1'; // Assign true if '1', otherwise false
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

        // Iterating through the array
        for (int i = 0; i < boolArray.Length; i++)
        {
            // If the element is true, add the corresponding power of two to the result
            if (boolArray[i])
            {
                decimalValue += (1 << (boolArray.Length - 1 - i)); // Shift bit
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
    public void calculateDifferentialCharacteristicsSequential()
    {
        _differentialCharacteristic.differentialCharacteristic[0][0] = _input.Length;
        for (int i = 0; i < _input.Length; ++i)
        {
            for (int j = i + 1; j < _input.Length; ++j)
            {
                var input_diff = Node.GetDiff(_input[i], _input[j]);
                var output_diff = Node.GetDiff(_output[i], _output[j]);
                // 2 due to the mirrored nature of calculations at j=i
                _differentialCharacteristic.IncrementDifference(input_diff, output_diff, 2);
            }
        }
        return;
    }

    /// <summary>
    /// Calculates differential characteristics of S Box parallel
    /// </summary>
    /// <param name="threads">Threads count</param>
    public void calculateDifferentialCharacteristicsParallel(int threads)
    {
        var splitted_input = SplitArray(_input, threads);
        var result = new SBox[threads];

        // Parallel output of arrays
        Parallel.For(0, splitted_input.Length, i =>
        {
            var sbox = new SBox(this, splitted_input[i]);
            sbox.calculateDifferentialCharacteristicsSequential();
            result[i] = sbox;
        });

        var length = _differentialCharacteristic.differentialCharacteristic.Length;
        for (int i = 0; i < threads; ++i)
        {
            for (int j = 0; j < length; ++j)
            {
                for (int k = 0; k < length; ++k)
                {
                    _differentialCharacteristic.differentialCharacteristic[j][k] += result[i]._differentialCharacteristic.differentialCharacteristic[j][k];
                }
            }
        }
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
        public int decimalNumber { get; set; }

        /// <summary>
        /// Boolean vector
        /// </summary>
        public bool[] booleanArray { get; set; }

        /// <summary>
        /// Basic Class Constructor
        /// </summary>
        /// <param name="_booleanArray">Boolean array</param>
        public Node(bool[] _booleanArray)
        {
            booleanArray = _booleanArray;
            decimalNumber = ConvertBooleanArrayToDecimal(booleanArray);
        }

        /// <summary>
        /// Converts a two-dimensional Boolean array to an array of the Node class
        /// </summary>
        /// <param name="input">Two-dimensional Boolean array</param>
        /// <returns>Array of the Node class</returns>
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
            result = lhs.decimalNumber ^ rhs.decimalNumber;
            return result;
        }
    }

    //=================================================================================================================
    // Differential Characteristics
    //=================================================================================================================

    /// <summary>
    /// A class representing the difference characteristics of the S box, as well as the possibilities for calculating these characteristics
    /// </summary>
    public class DifferentialCharacteristic
    {

        /// <summary>
        /// A two-dimensional array with difference characteristics
        /// </summary>
        public int[][] differentialCharacteristic { get; private set; }

        /// <summary>
        /// The number of characters to align the string representation of the array
        /// </summary>
        private int padLength;

        /// <summary>
        /// Initializing an empty array of difference characteristics
        /// </summary>
        /// <param name="variable_count">The number of variables of a Boolean function</param>
        public DifferentialCharacteristic(int variable_count)
        {
            var length = (int)Math.Pow(2, variable_count);
            differentialCharacteristic = new int[length][];
            for (int i = 0; i < length; ++i)
            {
                differentialCharacteristic[i] = new int[length];
            }

            // +1 due to the use of a separator
            padLength = СountDigits(length) + 1;
        }

        public void IncrementDifference(int input_diff, int output_diff, int value = 1)
        {
            differentialCharacteristic[input_diff][output_diff] += value;
        }

        /// <summary>
        /// Represents a two-dimensional array in human-readable form
        /// </summary>
        /// <returns>String representation of a two-dimensional array</returns>
        public void WriteToFile(string path, int buffer)
        {
            MatrixFormatter.FormatMatrix(differentialCharacteristic, padLength, path, buffer);
        }

        private static int СountDigits(int number)
        {
            // We bring the number to a positive value
            number = Math.Abs(number);

            // Checking whether the number is zero
            if (number == 0)
            {
                return 1; // If the number is zero, then the digits are 1
            }

            // We use the logarithm to count the number of integers
            return (int)Math.Floor(Math.Log10(number)) + 1;
        }

    }

}

