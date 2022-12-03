public class Day3 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        var splitItems = InputReader.GetInputLines("Day3.txt").Select(t => (a: t.Take(t.Length / 2), b: t.Skip(t.Length / 2))).ToArray();
        var intersects = splitItems.Select(p => p.a.Intersect(p.b)).Where(t => t.Count() > 0).Select(t => t.Single()).ToArray();
        return intersects.Where(char.IsUpper).Select(t => t - 38).Sum() + intersects.Where(char.IsLower).Select(t => t - 96).Sum();
    }

    public int Star2()
    {
        var groupItems = InputReader.GetGroupsOfLines("Day3.txt", 3);
        var sum = 0;
        foreach (var group in groupItems)
        {
            var intersects = group.Value[0].Intersect(group.Value[1]).Intersect(group.Value[2]).Single();
            sum += Char.IsUpper(intersects) ? intersects - 38 : intersects - 96;
        }
        return sum;
    }
}