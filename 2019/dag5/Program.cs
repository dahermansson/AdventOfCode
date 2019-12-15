﻿using System;
using System.IO;
using AdventOfCode2019;
using System.Collections.Generic;
using System.Linq;

namespace dag5
{
    class Program
    {
        static void Main(string[] args)
        {
            Star(1, 1);
            Star(5, 2);
        }
        static void Star(int input, int star)
        {
            var intcoder = new Intcoder(File.ReadAllText("input.txt"), OutputMode.OutputAndRunToEnd);
            intcoder.Exec(new Queue<int>(new int[]{input}));
            Console.WriteLine($"Star {star}: {intcoder.Outputs.Last()}");
        }
    }
}
