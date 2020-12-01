using System;
using System.Collections.Generic;

namespace AoC2020
{
    class Program
    {
        private static SortedList<int, IDag> Dagar = new SortedList<int, IDag>();
        private static readonly int DagToRun = 1;
        static void Main(string[] args)
        {
            Dagar.Add(1, new Dag1());

            Console.WriteLine($"Star 1: {Dagar[DagToRun].Star1()}");
            Console.WriteLine($"Star 2: {Dagar[DagToRun].Star2()}");
        }
    }
}
