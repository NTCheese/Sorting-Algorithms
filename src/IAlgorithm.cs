
public interface IAlgorithm
{
    public string Name => System.Text.RegularExpressions.Regex.Replace(GetType().Name, "(\\B[A-Z])", " $1");

    public int[] Sort(int[] nums);
}