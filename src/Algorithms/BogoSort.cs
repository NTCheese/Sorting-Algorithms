using System.Collections;

public class BogoSort : IAlgorithm
{
    public int[] Sort(int[] nums)
    {
        int[] numbers = nums;
        int[] randoming = new int[nums.Length];


        int length = nums.Length;
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            randoming[i] = random.Next(1, 300);
        }

        Array.Sort(randoming, numbers);

        // foreach (int val in numbers)
        //     Console.Write($" {val}");

        // Console.Write("\n");


        return numbers;
    }
}