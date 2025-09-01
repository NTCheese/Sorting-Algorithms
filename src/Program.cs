using System.Diagnostics;

public class Program()
{
    ConsoleColor originalForegroundColor = Console.ForegroundColor;
    private List<IAlgorithm> algorithms = new List<IAlgorithm>();
   
    private int[] ints = new int[10]; //50,000,000 is max
    private float timeout = 10; // in seconds
    private float printInterval = 1; // also in seconds


    public static void Main(string[] args)
    {
        Console.Clear();

        Program program = new Program();
        program.Run();
    }

    public void Run()
    {
        GetRandomInts();
        int[] solved = (int[])ints.Clone();
        Array.Sort(solved);

        Setup();


        foreach (var algorithm in algorithms)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Running: {algorithm.Name}");
            Console.ForegroundColor = originalForegroundColor;

            Stopwatch stopwatch = Stopwatch.StartNew();
            Stopwatch printTimer = Stopwatch.StartNew();

            int[] array = (int[])ints.Clone();
            int[] solveda = (int[])array.Clone();
            Array.Sort(solveda);

            bool solvedFlag = false;

        while (stopwatch.ElapsedMilliseconds < timeout * 1000)
        {
            array = algorithm.Sort(array);

            if (array.SequenceEqual(solveda))
            {
                solvedFlag = true;
                break;
            }

            if (printTimer.ElapsedMilliseconds >= 1000) // print every second
            {
                Console.Write("Array state: { ");
                Console.Write(string.Join(", ", array));
                Console.WriteLine(" }");
                printTimer.Restart();
            }
        }
            stopwatch.Stop();

            if (solvedFlag)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{algorithm.Name} sorted the array in {stopwatch.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{algorithm.Name} was not able to sort the array within {timeout} seconds");
            }

            Console.ForegroundColor = originalForegroundColor;
            Console.WriteLine();
        }
    }

    public void Setup()
    {
        FindAlgorithms();

        Console.WriteLine();


        if (ints.Length < 100)
        {
            Console.Write("Algorithms are sorting this array: {");

            for (int i = 0; i < ints.Length; i++)
            {
                if (!(i == ints.Length - 1))
                    Console.Write(ints[i] + ", ");
                else
                {
                    Console.Write(ints[i]);
                }
            }

            Console.WriteLine("}");
        }
        else
        {
            Console.WriteLine("The array is {" + ints.Length + "} ints long");
        }



        Console.WriteLine();
    }

    public void PrintArray() {

        Console.Write("{");

        if (ints.Length < 100)
        {
            for (int i = 0; i < ints.Length; i++)
            {
                if (!(i == ints.Length - 1))
                    Console.Write(ints[i] + ", ");
                else
                {
                    Console.Write(ints[i]);
                }
            }

            Console.Write("}");
        }
        else
        {
            Console.WriteLine("The array is {" + ints.Length + "} ints long");
        }


    }

    public void GetRandomInts()
    {
        Random random = new Random();

        for (int i = 0; i < ints.Length; i++)
        {
            ints[i] = random.Next(0, ints.Length);
        }
        //Console.WriteLine(string.Join(", ", ints));
    }

    public void FindAlgorithms()
    {
        var algoTypes = typeof(IAlgorithm).Assembly.GetTypes().Where(t => typeof(IAlgorithm).IsAssignableFrom(t) && !t.IsInterface);
        Console.ForegroundColor = ConsoleColor.Green;

        foreach (var type in algoTypes)
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

            IAlgorithm algo = (IAlgorithm)Activator.CreateInstance(type);

            Console.WriteLine($"Found: {algo.Name}");

#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

            algorithms.Add(algo);
        }

        Console.ForegroundColor = originalForegroundColor;
    }
}