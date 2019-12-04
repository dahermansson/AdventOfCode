using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace dag3
{
    class Program
    {
        class Koordinat
        {
            public Koordinat(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int X { get; set; }
            public int Y { get; set; }
        }

        static void Main(string[] args)
        {
            Star1();
            Star2();
        }

        private static void Star2()
        {
            List<Koordinat> intersections = GetKorsning();
            var path1 = GetPath(File.ReadAllLines("input.txt")[0]);
            var path2 = GetPath(File.ReadAllLines("input.txt")[1]);
            
            var minSteps = int.MaxValue;
            foreach (var intersection in intersections)
            {
                int steps = CountStepsToPoint(intersection, path1) + CountStepsToPoint(intersection, path2);
                if(steps < minSteps)
                    minSteps = steps;
            }
            Console.WriteLine($"Star 2: {minSteps}");
        }

        private static int CountStepsToPoint(Koordinat point, List<Koordinat> path)
        {   
            return path.FindIndex(t => t.X == point.X && t.Y == point.Y);
        }

        private static void Star1()
        {
            List<Koordinat> cross = GetKorsning();
            var shortest = int.MaxValue;
            foreach (var koord in cross)
            {
                var dist = ManhattanDistance(new Koordinat(0,0), koord);
                if(dist< shortest)
                    shortest = dist;
            }
            Console.WriteLine($"Star 1: {shortest}");
        }

        private static List<Koordinat> GetKorsning()
        {
            var input = File.ReadAllLines("input.txt");
            Dictionary<int, List<int>> dPath = GetIndexOfPath(input[0]);
            var senaste = new Koordinat(0,0);
            var korsningar = new List<Koordinat>();
            foreach (var twist in input[1].Split(','))
            {
                var koordStracka = CreateKoordinaterFromTwist(senaste, twist);
                foreach (var koordinat in koordStracka)
                {
                    if(dPath.ContainsKey(koordinat.X) && dPath[koordinat.X].IndexOf(koordinat.Y) != -1)
                        korsningar.Add(koordinat);
                }
                senaste = koordStracka.Last();
            }
            return korsningar;
        }

        private static List<Koordinat> GetPath(string input)
        {
            var res = new List<Koordinat>(){new Koordinat(0,0)};
            foreach (var twist in input.Split(','))
            {
                res.AddRange(CreateKoordinaterFromTwist(res.Last(), twist));
            }
            return res;
        }

        private static Dictionary<int, List<int>> GetIndexOfPath(string input)
        {
            var pathIndex = new Dictionary<int, List<int>>();
            var senaste = new Koordinat(0,0);
            pathIndex.Add(senaste.X, new List<int>(){senaste.Y});
            foreach (var twist in input.Split(','))
            {
                var koordStracka = CreateKoordinaterFromTwist(senaste, twist);
                foreach (var koordinat in koordStracka)
                {
                    if(pathIndex.ContainsKey(koordinat.X))
                        pathIndex[koordinat.X].Add(koordinat.Y);
                    else
                        pathIndex.Add(koordinat.X, new List<int>(){koordinat.Y});
                }
                senaste = koordStracka.Last();
            }
            return pathIndex;
        }

        private static int ManhattanDistance(Koordinat a, Koordinat b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }

        private static List<Koordinat> CreateKoordinaterFromTwist(Koordinat prevKoord, string twist)
        {
            var l = new List<Koordinat>();
            if(twist[0] == 'R')
            { 
                var y = prevKoord.Y;
                var startX = prevKoord.X;
                var langd = int.Parse(twist.TrimStart('R'));
                for (int i = 0; i < langd; i++)
                    l.Add(new Koordinat(++startX, y));
            }
            if(twist[0] == 'L')
            {
                var y = prevKoord.Y;
                var startX = prevKoord.X;
                var langd = int.Parse(twist.TrimStart('L'));
                for (int i = 0; i < langd; i++)
                    l.Add(new Koordinat(--startX, y));
            }
            if(twist[0] == 'U')
            {
                var startY = prevKoord.Y;
                var X = prevKoord.X;
                var langd = int.Parse(twist.TrimStart('U'));
                for (int i = 0; i < langd; i++)
                    l.Add(new Koordinat(X, ++startY));
            }
            if(twist[0] == 'D')
             {
                var startY = prevKoord.Y;
                var X = prevKoord.X;
                var langd = int.Parse(twist.TrimStart('D'));
                for (int i = 0; i < langd; i++)
                    l.Add(new Koordinat(X, --startY));
            }
            return l;
        }
    }
}
