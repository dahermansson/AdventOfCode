public class Day14 : IDay
{
    public string Output => throw new NotImplementedException();

    private List<List<Point>> RockLines = InputReader.GetInputLines("Day14.txt").Select(t => new List<Point>(t.Split(" -> ").Select(s => new Point(s)))).ToList();

    private Matrix<char> Cave;
    private int MaxX {get; set;}
    private int MaxY {get; set;}

    public Day14()
    {
        MaxX = RockLines.Max(t => t.Max(p => p.X));
        MaxY = RockLines.Max(t => t.Max(p => p.Y));
        CreateCave();
        CreateRockLinesInCave();
    }

    private void CreateCave()
    {
        Cave = new Matrix<char>(MaxY+2, MaxX + 1, false);
        Cave.UpdateAll(t => t = '.');
    }

    private void CreateCaveTwo()
    {
        Cave = new Matrix<char>(MaxY+3, (MaxX + 1) * 2, false);
        Cave.UpdateAll(t => t = '.');
    }
    private void CreateRockLinesInCave()
    {
        foreach (var rockLine in RockLines)
        {
            for (int i = 0; i < rockLine.Count-1; i++)
            {
                var start = rockLine[i];
                var end = rockLine[i+1];
                Cave.UpdateLine(start.Y, start.X, end.Y, end.X, t => t = '#');
            }
        }
    }

    private void CreateRockLinesInCaveTwo()
    {
        RockLines.Add(new List<Point>(){ new Point($"0,{MaxY + 2}"), new Point($"1007, {MaxY + 2}")});
        CreateRockLinesInCave();
    }

    public int Star1()
    {
        var restAtRow = 0;
        int unitsOfSand = 0;

        while (restAtRow <= MaxY)
        {
            unitsOfSand++;
            (bool, int, int) res = (false, 0, 500);
            while(!res.Item1)
            {
                res = Rest(res.Item2, res.Item3);
                if(res.Item2 > MaxY)
                    break;
            }
            Cave.Update(res.Item2, res.Item3, t => t = 'o');
            restAtRow = res.Item2;
        }

        return unitsOfSand -1;
    }

    public int Star2()
    {
        CreateCaveTwo();
        CreateRockLinesInCaveTwo();
        
        var restAtRow = 1;
        int unitsOfSand = 0;

        while (restAtRow != 0)
        {
            unitsOfSand++;
            (bool, int, int) res = (false, 0, 500);
            while(!res.Item1)
            {
                res = Rest(res.Item2, res.Item3);
                if(res.Item2 > MaxY)
                    break;
            }
            Cave.Update(res.Item2, res.Item3, t => t = 'o');
            restAtRow = res.Item2;
        }

        return unitsOfSand;
    }

    private (bool, int, int) Rest(int row, int col)
    {
        var below = Cave.GetNeighbours(row, col).Where(n => n.Row == row + 1).ToArray();
        if(below.All(t => t.Value != '.'))
            return (true, row, col);
        row++;
        if(below[1].Value != '.')
            if(below[0].Value == '.')
                col--;
            else
                col++;
        return (false, row, col);
    }

    private void PrintTestCave(int maxY, int maxX)
    {
        Console.WriteLine();
        for (int i = 0; i < maxY; i++)
        {
            for(int c = 450; c <= maxX; c++)
                Console.Write(Cave.Get(i, c).Value);
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
