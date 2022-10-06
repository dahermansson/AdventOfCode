public class Day14 : IDay
{
    public string Output => _output.ToString();
    public string[] Input = InputReader.GetInputLines("14.txt");
    private long _output;
    public int Star1()
    {
        var pairs = GetPairs(Input.First());
        var count = GetCounter(Input.First());
        var insertions = GetInsertions(Input.Skip(2));
        _output = GeneratePolymer(pairs, insertions, count, 10);
        return -1;
    }

    public int Star2()
    {
        var pairs = GetPairs(Input.First());
        var count = GetCounter(Input.First());
        var insertions = GetInsertions(Input.Skip(2));
        _output = GeneratePolymer(pairs, insertions, count, 40);
        return -1;
    }

    private Dictionary<string, long> GetCounter(string template)
    {
        var count = new Dictionary<string, long>();
        foreach (var c in template)
            count[c.ToString()] = template.Count(d => d == c);
        return count;
    }

    private Dictionary<string, string> GetInsertions(IEnumerable<string> inputs)
    {
        var insertions = new Dictionary<string, string>();
        foreach (var insertion in inputs)
            insertions.Add(insertion.Split(" -> ")[0], insertion.Split(" -> ")[1]);
        return insertions;
    }

    private Dictionary<string, long> GetPairs(string template)
    {
        var pairs = new Dictionary<string, long>();
        for (int i = 1; i < template.Length; i++)
        {
            var pair = $"{template[i - 1]}{template[i]}";
            if (pairs.ContainsKey(pair))
                pairs[pair]++;
            else
                pairs[pair] = 1;
        }
        return pairs;
    }

    private long GeneratePolymer(Dictionary<string, long> pairs, Dictionary<string, string> insertions, Dictionary<string, long> count, int runs)
    {
        for (int i = 0; i < runs; i++)
        {
            var inserts = new List<(string pair, long count)>();
            foreach (var insert in insertions.Where(k => pairs.ContainsKey(k.Key)).ToArray())
            {
                inserts.AddRange(CreateInserts(insert, pairs[insert.Key]));
                if (!count.ContainsKey(insert.Value))
                    count[insert.Value] = pairs[insert.Key];
                else
                    count[insert.Value] += pairs[insert.Key];
                pairs[insert.Key] = 0;
            }

            foreach (var insert in inserts)
                if (pairs.ContainsKey(insert.pair))
                    pairs[insert.pair] += insert.count;
                else
                    pairs[insert.pair] = insert.count;
        }
        return (count.Values.Max() - count.Values.Min());
    }


    private IEnumerable<(string pair, long count)> CreateInserts(KeyValuePair<string, string> insert, long count)
    {
        yield return ($"{insert.Key[0]}{insert.Value}", count);
        yield return ($"{insert.Value}{insert.Key[1]}", count);
    }


}
