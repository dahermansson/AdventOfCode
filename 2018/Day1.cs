public class Day1 : IDay
{
    public string Output => throw new NotImplementedException();
    private List<int> Input = InputReader.GetInput("1.txt").Select(s => int.Parse(s.ToString())).ToList();

    public int Star1() => Input.Sum();

    public int Star2()
    {
        var sum = 0;
        var freq = new List<int>();
        var cnt = 0;
        do
        {
            freq.Add(sum);
            sum += Input[cnt++];
            if (cnt == Input.Count)
                cnt = 0;
        } while (freq.IndexOf(sum) < 0);
        return sum;
    }
}