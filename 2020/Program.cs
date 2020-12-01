using System;
using System.Collections.Generic;

namespace AoC2020
{
    class Program
    {
        private static SortedList<int, IDag> Dagar = new SortedList<int, IDag>();
        static void Main(string[] args)
        {

            Dagar.Add(1, new Dag1());

            Console.WriteLine(Dagar[1].Star1("dag1.txt"));

        }
    }
}
