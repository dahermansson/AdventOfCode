public class Day23 : IDay
{
    public string Output => throw new NotImplementedException();
    private HashSet<Point> Elvs = new HashSet<Point>();
    public int Star1()
    {
        int _ = Run(10), rows = 0, cols = 0;;
        for (int y = Elvs.Min(t => t.Y); y <= Elvs.Max(t => t.Y); y++)
            rows++;
        for (int x = Elvs.Min(t => t.X); x <= Elvs.Max(t => t.X); x++)
            cols++;
        return rows * cols - Elvs.Count;
    }

    public int Star2() => Run(10000);
    
    private void InitElvsPosition()
    {
        Elvs.Clear();
        var input = InputReader.GetInputLines("Day23.txt");
        for (int y = 0; y < input.Length; y++)
            for (int x = 0; x < input[y].Length; x++)
                if (input[y][x] == '#')
                    Elvs.Add(new Point(x, y));
    }

    private int Run(int runs)
    {
        InitElvsPosition();
        var directionsOrder = new[] { Direction.North, Direction.South, Direction.West, Direction.East };
        var directionsOrderIndex = 0;
        for (int i = 0; i < runs; i++)
        {
            var wantsToMove = new Dictionary<Point, Point>();
            foreach (var elv in Elvs)
            {
                if(!AnyNeigbour(elv))
                    continue;
                for (int d = directionsOrderIndex; d < directionsOrderIndex + 4; d++)
                {
                    var dir = directionsOrder[d % 4];
                    if (CanMove(elv, dir))
                    {
                        var direction = Directions.DIRECTIONS[(int)dir];
                        wantsToMove.Add(elv, new Point(elv.X + direction.X, elv.Y + direction.Y));
                        break;
                    }
                }
            }
            if(!wantsToMove.Any())
                return i+1;
            var moves = wantsToMove.GroupBy(t => t.Value).Where(t => t.Count() == 1).ToArray();
            foreach (var elvToMove in moves)
            {
                Elvs.Remove(elvToMove.Single().Key);
                Elvs.Add(elvToMove.Single().Value);
            }
            directionsOrderIndex = (directionsOrderIndex + 1) % 4;
        }
        return 0;
    }

    private void Print()
    {
        var minX = Elvs.Min(t => t.X);
        var minY = Elvs.Min(t => t.Y);
        var maxX = Elvs.Max(t => t.X);
        var maxY = Elvs.Max(t => t.Y);

        for (int y = minY; y <= maxY; y++)
        {
            for (int x = minX; x <= maxX; x++)
                if(Elvs.Contains(new Point(x, y)))
                    Console.Write("#");
                else
                    Console.Write(".");
            Console.WriteLine();   
        }
        Console.WriteLine();
    }

    private bool CanMove(Point elv, Direction dir)
    {
        if (dir == Direction.North
            && !Elvs.Contains(elv.GetNextInDir(Direction.NorthWest))
            && !Elvs.Contains(elv.GetNextInDir(Direction.North))
            && !Elvs.Contains(elv.GetNextInDir(Direction.NorthEast)))
            return true;
        if (dir == Direction.East
            && !Elvs.Contains(elv.GetNextInDir(Direction.NorthEast))
            && !Elvs.Contains(elv.GetNextInDir(Direction.East))
            && !Elvs.Contains(elv.GetNextInDir(Direction.SouthEast)))
            return true;
        if (dir == Direction.South
            && !Elvs.Contains(elv.GetNextInDir(Direction.SouthEast))
            && !Elvs.Contains(elv.GetNextInDir(Direction.South))
            && !Elvs.Contains(elv.GetNextInDir(Direction.SouthWest)))
            return true;
        if (dir == Direction.West
            && !Elvs.Contains(elv.GetNextInDir(Direction.NorthWest))
            && !Elvs.Contains(elv.GetNextInDir(Direction.West))
            && !Elvs.Contains(elv.GetNextInDir(Direction.SouthWest)))
            return true;
        return false;
    }
    private bool AnyNeigbour(Point elv) => Directions.DIRECTIONS.Any(t  => Elvs.Contains(new Point(elv.X + t.X, elv.Y + t.Y )));
}
