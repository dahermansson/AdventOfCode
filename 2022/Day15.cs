public class Day15 : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input = InputReader.GetInputLines("Day15.txt");

    public int Star1()
    {
        Dictionary<Point, string> input = new ();
        foreach (var row in Input)
        {
            input.Add(new Point(row.Split(':')[0]), "s");
            input.Add(new Point(row.Split(':')[1]), "b");
        }
        return 1;
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }
}
