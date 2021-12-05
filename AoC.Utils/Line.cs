using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public class Line
    {
        public IEnumerable<Point> Points { get; set; } = new List<Point>();
        public Line(string input, string separator, string pointSeparator = ",") => ((List<Point>)Points).AddRange(input.Split(separator).Select(t => new Point(t, pointSeparator)));
        public Point Start { get { return Points.First(); } }
        public Point End { get { return Points.Last(); } }
        public bool IsDiagonal {get {return Start.X != End.X && Start.Y != End.Y; } }
        public int MaxX { get { return Points.Max(t => t.X); } }
        public int MaxY { get { return Points.Max(t => t.Y); } }
        public int MinX { get { return Points.Min(t => t.X); } }
        public int MinY { get { return Points.Min(t => t.Y); } }



    }
}