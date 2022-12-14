public class Day14 : IDay
{
    public string Output => throw new NotImplementedException();

    private List<List<Point>> RockLines = InputReader.GetInputLines("Day14.txt").Select(t => new List<Point>(t.Split(" -> ").Select(s => new Point(s)))).ToList();

    public int Star1()
    {
        var maxX = RockLines.Max(t => t.Max(p => p.X));
        var maxY = RockLines.Max(t => t.Max(p => p.Y));
        var cave = new Matrix<char>(maxY, maxX);
        cave.UpdateAll(t => t = '.');


        foreach (var rockLine in RockLines)
        {
            for (int i = 0; i < rockLine.Count-1; i++)
            {
                var start = rockLine[i];
                var end = rockLine[i+1];
                cave.UpdateLine(start.Y, start.X, end.Y, end.X, t => t = '#');
            }
        }
        
        return maxY;
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }
}
