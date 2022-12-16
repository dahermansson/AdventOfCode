using System.Diagnostics;

public class Day15 : IDay
{
    public string Output => _ouput;
    private string[] Input = InputReader.GetInputLines("Day15.txt");
    private string _ouput = "";

    public int Star1()
    {
        Dictionary<Point, Point> input = new ();
        foreach (var row in Input)
            input.Add(new Point(row.Split(':')[0]), new Point(row.Split(':')[1]));

        var rowToCheck = 2000000;
        var coversLine = input.Where(t => Utils.ManhattanDistance(t.Key, t.Value) > Math.Abs(t.Key.Y - rowToCheck)).Select(t => (t, Utils.ManhattanDistance(t.Key, t.Value))).ToList();
        
        var ranges = new List<(int, int)>();
        foreach (var sensor in coversLine)
        {
            int diff = sensor.Item2 - Math.Abs(rowToCheck - sensor.t.Key.Y);
            ranges.Add((sensor.t.Key.X - diff, sensor.t.Key.X + diff));   
        }

        HashSet<int> positioner = new();
        foreach (var range in ranges)
            for (int i = range.Item1; i <= range.Item2 ; i++)
                if(!positioner.Contains(i))
                    positioner.Add(i);
        var beaconOnRow = input.Where(t => t.Value.Y == rowToCheck).ToArray();
        foreach (var beacon in beaconOnRow)
            if(positioner.Contains(beacon.Value.X))
                positioner.Remove(beacon.Value.X);
        return positioner.Count;
    }

    public int Star2()
    {
        Dictionary<Point, Point> input = new ();
        foreach (var row in Input)
            input.Add(new Point(row.Split(':')[0]), new Point(row.Split(':')[1]));
        
        int maxRows = 4000001;
        Stopwatch sw = new Stopwatch();
        sw.Start();
        Parallel.For(0, maxRows, y => 
        //for (int y = maxRows; y > 0; --y)
        {
            var coversLine = input.Where(t => Utils.ManhattanDistance(t.Key, t.Value) > Math.Abs(t.Key.Y - y))
                .Select(t => (t, Utils.ManhattanDistance(t.Key, t.Value))).ToList();
            
            var ranges = new List<(int, int)>();
            foreach (var sensor in coversLine)
            {
                int diff = sensor.Item2 - Math.Abs(y - sensor.t.Key.Y);
                ranges.Add((sensor.t.Key.X - diff, sensor.t.Key.X + diff));   
            }

            int? endValue = null;
            var saker = ranges.OrderBy(t => t.Item1).ToArray();
            for (int i = 0; i < saker.Length; i++)
            {
                if(endValue is null)
                {
                    endValue = saker[i].Item2;
                    continue;
                }
                if(saker[i].Item1 <= endValue +1)
                {
                    if(endValue < saker[i].Item2)
                        endValue = saker[i].Item2;
                }
                else
                {
                    Int64 res = ((Int64)endValue + 1) * (Int64)4000000 + (Int64)y;
                    _ouput = res.ToString();
                    Console.WriteLine(sw.Elapsed);
                    break;
                }   
            }
        });
        return -1;
    }
}
