public class Day3 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        Dictionary<(int, int), int> houses = new();
        var directions = InputReader.GetInput("Day3.txt");
        int x = 0;
        int y = 0;
        houses[(x,y)] =  1;
        foreach (char dir in directions)
        {
            if(dir == 'v') x--;
            if(dir == '^') x++;
            if(dir == '<') y--;
            if(dir == '>') y++;
            if(!houses.ContainsKey((x,y)))
                houses[(x,y)] =  1;
            else
                houses[(x, y)] += 1;
        }
        return houses.Count;
    }

    public int Star2()
    {
        Dictionary<(int, int), int> houses = new();
        var directions = InputReader.GetInput("Day3.txt");
        int santax = 0;
        int santay = 0;
        int robosantax = 0;
        int robosantay = 0;
        houses[(santax,santay)] =  1;
        for (int i = 0; i < directions.Length; i++)
        {
            if(i % 2 == 0)
            {
                Move(directions[i], ref santax, ref santay);
                if(!houses.ContainsKey((santax,santay)))
                    houses[(santax,santay)] =  1;
                else
                    houses[(santax, santay)] += 1;
            }
            else
            {
                Move(directions[i], ref robosantax, ref robosantay);
                if(!houses.ContainsKey((robosantax,robosantay)))
                    houses[(robosantax,robosantay)] =  1;
                else
                    houses[(robosantax, robosantay)] += 1;
            }
        }
        return houses.Count;
    }

    private void Move(char dir, ref int x, ref int y)
    {
        if(dir == 'v') x--;
        if(dir == '^') x++;
        if(dir == '<') y--;
        if(dir == '>') y++;
    }

    private record House
    {
        public House(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; } 
        public int Y { get; set; }
    }
}
