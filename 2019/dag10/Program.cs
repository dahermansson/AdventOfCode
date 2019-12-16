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
            var astroidLines = new List<SortedList<int, Astroid>>();
            var orderdLines = astroid.InLine.Where(t => t.Key <= Math.PI/2 && t.Key > 0).OrderByDescending(t => t.Key).Select(t => t.Value).ToList();
            orderdLines.AddRange(
                astroid.InLine.Where(t => t.Key >= 0 && t.Key <= Math.PI).OrderBy(t => t.Key).Select(t => t.Value).ToList()
            );
            orderdLines.AddRange(
                astroid.InLine.Where(t => t.Key <= Math.PI && t.Key <= Math.PI/2).OrderByDescending(t => t.Key).Select(t => t.Value).ToList()
            );
            
            //var sordetInsigthLists = 
            foreach (var line in orderdLines)
            {
              //  line
            }

            int laserd = 0;
            while (laserd < 200)
            {
                for (int i = 0; i < orderdLines.Count; i++)
                {
                    if(orderdLines[i].Any())
                        orderdLines[i].RemoveAt(0);
                }
            }

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
                    if(astroid.InLine.ContainsKey(angel))
                        astroid.InLine[angel].Add(b);
                    else
                        astroid.InLine.Add(angel, new List<Astroid>(){b});
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
            InLine = new Dictionary<double, List<Astroid>>();
        }
        public Dictionary<double, List<Astroid>> InLine { get; set; }
        public double X { get; set; }
        public double Y{ get; set; }

        public double Angle(Astroid b)
        {
            return Math.Atan2((this.X - b.X), (this.Y - b.Y));
        }
    }

}
