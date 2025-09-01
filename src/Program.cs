using System.Diagnostics;

public class Program()
{
    ConsoleColor originalForegroundColor = Console.ForegroundColor;
    private List<IAlgorithm> algorithms = new List<IAlgorithm>();
   
    private int[] ints = new int[10];
    private float timeout = 10; // in seconds
    private float printInterval = 1; // also in seconds
    long lastPrint = 0;

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

            while (!(algorithm.Sort(ints) == solved) && (stopwatch.ElapsedMilliseconds < timeout * 1000))
            {
                int[] array = algorithm.Sort(ints);

                if (stopwatch.ElapsedMilliseconds - lastPrint >= printInterval * 1000)
                {
                    Console.Write("Array is ");
                    PrintArray();
                    Console.WriteLine(" @ " + stopwatch.ElapsedMilliseconds / 1000 + " seconds");
                    lastPrint = stopwatch.ElapsedMilliseconds;
                }

                if (array == solved)
                {
                    stopwatch.Stop();
                    Console.WriteLine($"{algorithm.Name} took {stopwatch.ElapsedMilliseconds} to sort the array");
                }
            }

            if (algorithm.Sort(ints) != solved)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{algorithm.Name} was not able to sort the array in {timeout} seconds");
                Console.ForegroundColor = originalForegroundColor;
            }

            Console.WriteLine();
        }
    }

    public void Setup()
    {
        FindAlgorithms();

        Console.WriteLine();
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

        Console.WriteLine();
    }

    public void PrintArray() {

        Console.Write("{");
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