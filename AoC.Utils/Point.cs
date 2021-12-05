using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(string input, string separator = ",")
        {
            X = int.Parse(input.Split(separator)[0]);
            Y = int.Parse(input.Split(separator)[1]);
        }
    }
}