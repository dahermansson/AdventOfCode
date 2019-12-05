using System;
using System.IO;
using System.Linq;
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
            int result = 0;
            int noun = 0;
            int verb = 0;
            while (target != result)
            {
                noun++;
                if(noun == 100)
                {
                    noun = 0;
                    verb++;
                }
                var intcoder = new Intcoder(File.ReadAllText("input.txt"));
                intcoder.Init(1, noun);
                intcoder.Init(2, verb);
                int exec = 0;
                do
                    exec = intcoder.Exec(exec);
                while (exec != -1);
                result = intcoder.PositionZero;
            }
            Console.WriteLine($"Star 2: {100 * noun + verb}");
        }

        static void Star1()
        {
            var intcoder = new Intcoder(File.ReadAllText("input.txt"));
            intcoder.Init(1, 12);
            intcoder.Init(2, 2);
            int exec = 0;
            do
                exec = intcoder.Exec(exec);
            while (exec != -1);
            Console.WriteLine($"Star 1: {intcoder.PositionZero}");

        }
    }
}
