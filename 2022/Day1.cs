public class Day1 : IDay
{
    public string Output => throw new NotImplementedException();
    //public int Star1() => SumCalories().Max();
    public int Star1() => GetMaxSumCalories();
    //public int Star2() => SumCalories().Order().TakeLast(3).Sum();
    public int Star2() => GetSumTop3SumCalories();

    private int GetMaxSumCalories() => InputReader.GetInput("day1.txt").Split("\r\n\r\n").Select(p => p.Split("\r\n").Select(int.Parse).Sum()).Max();
    private int GetSumTop3SumCalories() => InputReader.GetInput("day1.txt").Split("\r\n\r\n").Select(p => p.Split("\r\n").Select(int.Parse).Sum()).Order().TakeLast(3).Sum();
    private IEnumerable<int> SumCalories() => InputReader.GetInputLines("day1.txt").Aggregate(new List<int> { 0 }, (l, n) =>
    {
        if (int.TryParse(n, out int x))
            l[^1] += x;
        else
            l.Add(0);
        return l;
    });
}
