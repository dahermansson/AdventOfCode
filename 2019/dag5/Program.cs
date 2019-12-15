using System;
using System.IO;
using AdventOfCode2019;

namespace dag5
{
    class Program
    {
        static void Main(string[] args)
        {
            Star(1);
            Star(5);
        }

        static void Star(int input)
        {
            var intcode = File.ReadAllText("input.txt");
            var intcoder = new Intcoder(intcode, 1);
            intcoder.Inputs.Push(input);
            intcoder.OutputDelegate = (int value, string name) => {Console.WriteLine(value); };
            intcoder.Exec();
        }
    }
}
