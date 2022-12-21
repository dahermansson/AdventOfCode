public class Day18 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        var cubes = InputReader.GetInputLines("Day18.txt").Select(s =>new Cube(s)).ToList();
        int coverd = 0;
        foreach (var cube in cubes)
            coverd += 6 - cubes.Where(t => Math.Abs(cube.X - t.X) + Math.Abs(cube.Y - t.Y) + Math.Abs(cube.Z - t.Z) == 1).Count();
        return coverd;
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }

    private class Cube
    {
        public Cube(string s)
        {
            var split = s.Split(",");
            X = int.Parse(split[0]);
            Y = int.Parse(split[1]);
            Z = int.Parse(split[2]);
        }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
