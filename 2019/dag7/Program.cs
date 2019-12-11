using System;
using AdventOfCode2019;
using System.IO;
using System.Collections.Generic;
using System.Linq;
namespace dag7
{
    class Program
    {
        public static int[] digitArr(int n)
        {
            if (n == 0) return new int[1] { 0 };

            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();
            Array.Reverse(arr);
            return arr;
        }

        static IEnumerable<IEnumerable<T>>GetPermutations<T>(IEnumerable<T> list, int length)
        {
        if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        static void Main(string[] args)
        {
            Star1();
            Star2();
        }


        static void Star2()
        {
            string input = File.ReadAllText("input.txt");
            //input = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            var thrusterSignals = new List<int>();
            var settings = GetPermutations(new List<int>(){0,1,2,3,4}, 5).Select(t => new List<int>(t));
            
            foreach (var parameters in settings)
            {
                Intcoder intcoder = new Intcoder(input);
                intcoder.inputs.Push(0);
                for (int i = 0; i < 5; i++)
                {
                    intcoder.Reset(input, false);
                    intcoder.inputs.Push(parameters[i]);
                    var pointer = 0;
                    do
                        pointer = intcoder.Exec(pointer);
                    while (pointer != -1);
                }
                //Console.WriteLine(intcoder.inputs.Peek());
                thrusterSignals.Add(intcoder.inputs.Peek());
            }
        
            Console.WriteLine($"Star 1: {thrusterSignals.Max()}");
        }

        static void Star1()
        {
            string input = File.ReadAllText("input.txt");
            //input = "3,31,3,32,1002,32,10,32,1001,31,-2,31,1007,31,0,33,1002,33,7,33,1,33,31,31,1,32,31,31,4,31,99,0,0,0";
            var thrusterSignals = new List<int>();
            var settings = GetPermutations(new List<int>(){0,1,2,3,4}, 5).Select(t => new List<int>(t));
            
            foreach (var parameters in settings)
            {
                Intcoder intcoder = new Intcoder(input);
                intcoder.inputs.Push(0);
                for (int i = 0; i < 5; i++)
                {
                    intcoder.Reset(input, false);
                    intcoder.inputs.Push(parameters[i]);
                    var pointer = 0;
                    do
                        pointer = intcoder.Exec(pointer);
                    while (pointer != -1);
                }
                //Console.WriteLine(intcoder.inputs.Peek());
                thrusterSignals.Add(intcoder.inputs.Peek());
            }
        
            Console.WriteLine($"Star 1: {thrusterSignals.Max()}");
        }
    }
}
