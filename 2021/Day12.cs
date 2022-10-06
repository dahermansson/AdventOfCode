public class Day12 : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input = InputReader.GetInputLines("12.txt");

    private Graph<string> _graph { get; set; }
    public Day12()
    {
        var nodes = Input.Select(s => s.Split("-")).SelectMany(t => t).Distinct();
        var edges = Input.Select(s => s.Split("-")).Select(s => new Edge<string>(s[0], s[1]));
        _graph = new Graph<string>(nodes, edges);
    }

    public int Star1() => new GraphPathFinding<string>().BreadthFirst(_graph, "start", "end", IsAllowedStar1);
    public int Star2() => new GraphPathFinding<string>().BreadthFirst(_graph, "start", "end", IsAllowedStar2);

    private bool IsAllowedStar1(List<string> visited, string node)
    {
        if (!node.All(char.IsLower))
            return true;
        return !visited.Contains(node);
    }

    private bool IsAllowedStar2(List<string> visited, string node)
    {
        if ((node == "start" || node == "end") && visited.Contains(node))
            return false;
        if (node.All(char.IsLower) && visited.Count(n => n == node) >= 2)
            return false;
        if (!node.All(char.IsLower))
            return true;
        if (!visited.Contains(node))
            return true;
        return !visited.Where(l => l.All(char.IsLower)).GroupBy(g => g).Any(g => g.Count() > 1);
    }
}
