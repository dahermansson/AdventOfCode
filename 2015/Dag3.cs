using System;
using System.Collections.Generic;
namespace AoC2015
{
    public class Dag3 : IDag
    {
        private static string Input = InputReader.GetInputLine("dag3.txt");
        public int Star1()
        {
            var grid = new Dictionary<Pos, int>();
            var pos = new Pos(0,0);
            grid.Add(pos, 1);
            foreach (var direction in Input.ToCharArray())
            {
                if(direction == '^')
                    pos.X++;
                else if(direction == 'v')
                    pos.X--;
                else if(direction == '>')
                    pos.Y++;
                else if(direction == '<')
                    pos.Y--;
                var newPos = new Pos(pos.X, pos.Y);
                if(grid.ContainsKey(newPos))
                    grid[newPos]++;
                else
                    grid.Add(newPos, 1);
            }
            return grid.Count;
        }

        public int Star2()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Pos
    {
        public Pos(int x, int y)
        {
            X = y;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}