using System;
using System.Linq;
using System.Collections.Generic;

namespace AoC2020
{
    public class Dag24 : IDag
    {
        public string Output => throw new System.NotImplementedException();

        public string[] input = InputReader.GetInputLines("dag24.txt");

        public int Star1()
        {
            Dictionary<Tuple<int, int>, bool> tiles = new Dictionary<Tuple<int, int>, bool>();
            foreach (var line in input)
            {
                TraverseTiles(line, out int x, out int y);
                if(tiles.ContainsKey(new Tuple<int, int>(x, y)))
                    tiles[new Tuple<int, int>(x, y)] = !tiles[new Tuple<int, int>(x, y)];
                else
                    tiles.Add(new Tuple<int, int>(x, y), true);
            }
            return tiles.Count(t => t.Value);
        }

        public int Star2()
        {
            throw new System.NotImplementedException();
        }

        private void TraverseTiles(string s, out int x, out int y)
        {
            x = 0;
            y = 0;
            while(s.Length > 0)
            {
                if(s.StartsWith("e"))
                {
                    x++;
                    s = s.Remove(0, 1);
                }
                else if(s.StartsWith("se"))
                {
                    y++;
                    s = s.Remove(0, 2);
                }
                else if(s.StartsWith("sw"))
                {
                    x--;
                    y++;
                    s = s.Remove(0, 2);
                }
                else if(s.StartsWith("w"))
                {
                    x--;
                    s = s.Remove(0, 1);
                }
                else if(s.StartsWith("nw"))
                {
                    y--;
                    s = s.Remove(0, 2);
                }
                else if(s.StartsWith("ne"))
                {
                    y--;
                    x++;
                    s = s.Remove(0, 2);
                }
            }
        }
    }
}