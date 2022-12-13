using System.Text.Json.Nodes;

public class Day13 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1() => InputReader.GetGroupsOfLines("Day13.txt", 3).Select((t, i) => (Pair: i + 1, Left: JsonNode.Parse(t.Value[0]), Rigth: JsonNode.Parse(t.Value[1])))
        .Where(i => Compare(i.Left, i.Rigth) < 0).Select(t => t.Pair).Sum();

    public int Star2()
    {
        var packages = InputReader.GetInputLines("Day13.txt").Where(t => t.Any()).Select((t) => (JsonNode.Parse(t))).ToList();
        var dividers = new[] { JsonNode.Parse("[[2]]"), JsonNode.Parse("[[6]]") };
        packages.AddRange(dividers);
        packages.Sort(Compare);
        return (packages.IndexOf(dividers[0]) + 1) * (packages.IndexOf(dividers[1]) + 1);
    }

    private int Compare(JsonNode? left, JsonNode? rigth)
    {
        if (left == null || rigth == null)
            throw new ArgumentNullException("null not allowed");

        if (left is JsonValue l && rigth is JsonValue r)
            return ((int)left) - ((int)rigth);

        var lArray = left as JsonArray ?? new JsonArray((int)left);
        var rArray = rigth as JsonArray ?? new JsonArray((int)rigth);
        var array = Enumerable.Zip(lArray, rArray);
        foreach (var item in array)
        {
            var diff = Compare(item.First, item.Second);
            if (diff != 0)
                return diff;
        }
        if (lArray.Count() != rArray.Count())
            return lArray.Count() - rArray.Count();
        return 0;
    }
}
