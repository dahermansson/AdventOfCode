using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2019;

namespace dag10
{
    class Program
    {
        static void Main(string[] args)
        {
            var astroid = Star1();
            Star2(astroid);
        }

        static void Star2(Astroid astroid)
        {
            var vaporized = astroid.InLine.OrderByDescending(t => t.Key).ElementAt(199).Value.First().Value;
            Console.WriteLine($"Star 2: {vaporized.X * 100 +vaporized.Y}");            
        }

        static Astroid Star1()
        {
            var input = File.ReadAllLines("input.txt");
            var astroids = new List<Astroid>();
            for (int i = 0; i < 21; i++)
                for (int c = 0; c < 21; c++)
                    if(input[i][c] == '#')
                        astroids.Add(new Astroid(c, i));

            foreach (var astroid in astroids)
            {
                foreach (var b in astroids.Where(a => a.X != astroid.X || a.Y != astroid.Y))
                {
                    var angel = astroid.Angle(b);
                    var manhattanDistance = astroid.ManhattanDistance(b);
                    if(!astroid.InLine.ContainsKey(angel))
                        astroid.InLine.Add(angel, new SortedList<double, Astroid>());
                    astroid.InLine[angel].Add(manhattanDistance, b);
                }
            }
            var mostInSight = astroids.Max(t => t.InLine.Count);
            Console.WriteLine($"Star 1: {mostInSight}");

            return astroids.First(t => t.InLine.Count == mostInSight);
        }
    }

    class Astroid
    {
        public Astroid(double x, double y)
        {
            X = x;
            Y = y;
            InLine = new Dictionary<double, SortedList<double, Astroid>>();
        }
        public Dictionary<double, SortedList<double, Astroid>> InLine { get; set; }
        public double X { get; set; }
        public double Y{ get; set; }

        public double ManhattanDistance(Astroid b)
        {
            return Utils.ManhattanDistance(this.X, b.X, this.Y, b.Y);
        }

        public double Angle(Astroid b)
        { 
            var x = this.X - b.X;
            var y = this.Y - b.Y;
            var rad = Math.Atan2(x, y);
            return (rad > 0 ? rad : (2*Math.PI + rad)) * 360 / (2*Math.PI);
        }
    }

}
