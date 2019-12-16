using System;
using System.IO;
using System.Numerics;
using AdventOfCode2019;
namespace dag2
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
            Star2();
        }

        static void Star2()
        {
            int target = 19690720;
            BigInteger result = 0;
            int noun = 0;
            int verb = 0;
            var intcoder = new Intcoder(File.ReadAllText("input.txt"), OutputMode.OutputAndRunToEnd);
            while (target != result)
            {
                if(noun == 100)
                {
                    noun = 0;
                    verb++;
                }
                intcoder.Init(1, noun++);
                intcoder.Init(2, verb);
                intcoder.Exec();
                result = intcoder.PositionZero;
                intcoder.Reset();
            }
            Console.WriteLine($"Star 2: {100 * (noun-1) + verb}");
        }

        static void Star1()
        {
            var intcoder = new Intcoder(File.ReadAllText("input.txt"), OutputMode.OutputAndRunToEnd);
            intcoder.Init(1, 12);
            intcoder.Init(2, 2);
            intcoder.Exec();
            Console.WriteLine($"Star 1: {intcoder.PositionZero}");

        }
    }
}
