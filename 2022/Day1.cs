public class Day1 : IDay
{
    public string Output => throw new NotImplementedException();
    public int Star1() => SumCalories().Max();
    public int Star2() => SumCalories().Order().TakeLast(3).Sum();
    private IEnumerable<int> SumCalories() => InputReader.GetInputLines("day1.txt").Aggregate(new List<int> { 0 }, (l, n) =>
    {
        if (int.TryParse(n, out int x))
            l[^1] += x;
        else
            l.Add(0);
        return l;
    });
}
