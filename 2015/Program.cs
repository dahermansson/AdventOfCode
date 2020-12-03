using System;
using System.Collections.Generic;

namespace AoC2015
{
    class Program
    {
        private static Dictionary<int, IDag> Dagar = new Dictionary<int, IDag>();
        private static readonly int DagToRun = 1;
        static void Main(string[] args)
        {
            Dagar.Add(1, new Dag1());
            Dagar.Add(2, new Dag2());
            Dagar.Add(3, new Dag3());

            Console.WriteLine($"Star 1: {Dagar[DagToRun].Star1()}");
            Console.WriteLine($"Star 2: {Dagar[DagToRun].Star2()}");
        }
    }
}
