﻿using System;
using System.Collections.Generic;

namespace AoC2020
{
    class Program
    {
        private static Dictionary<int, IDag> Dagar = new Dictionary<int, IDag>();
        private static readonly int DagToRun = 13;
        
        static void Main(string[] args)
        {
            Dagar.Add(1, new Dag1());
            Dagar.Add(2, new Dag2());
            Dagar.Add(3, new Dag3());
            Dagar.Add(4, new Dag4());
            Dagar.Add(5, new Dag5());
            Dagar.Add(6, new Dag6());
            Dagar.Add(8, new Dag8());
            Dagar.Add(9, new Dag9());
            Dagar.Add(10, new Dag10());
            Dagar.Add(12, new Dag12());
            Dagar.Add(13, new Dag13());



            
            Console.WriteLine($"Star 1: {Dagar[DagToRun].Star1()}");

            var star2 = Dagar[DagToRun].Star2();
            var output = star2 == -1 ? Dagar[DagToRun].Output: star2.ToString(); 
            Console.WriteLine($"Star 2: { output}");
        }
    }
}
