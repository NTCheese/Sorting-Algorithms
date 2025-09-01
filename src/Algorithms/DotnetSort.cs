public class DotnetSort : IAlgorithm
{
    public int[] Sort(int[] nums)
    {
        int[] numbers = nums;
        Array.Sort(numbers);

        return numbers;
    }
}