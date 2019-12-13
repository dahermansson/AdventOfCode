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
            //Star2();
        }

        static void Star2()
        {
            string input = File.ReadAllText("input.txt");
            var thrusterSignals = new List<int>();
            var settings = GetPermutations(new List<int>(){5,6,7,8,9}, 5).Select(t => new List<int>(t));
            
            foreach (var parameters in settings)
            {
                var intcoders = Enumerable.Range(0, 5).Select( t => new Intcoder(input)).ToArray();
                intcoders[0].Inputs.Enqueue(0);

                for (int i = 0; i < intcoders.Length; i++)
                {
                    if(i == 0)
                        intcoders[i].Inputs.Enqueue(intcoders[intcoders.Length -1].Exec().Last());
                    else
                        intcoders[i].Inputs.Enqueue(intcoders[i +1].Exec().Last());
                    
                }
                thrusterSignals.Add(intcoders[intcoders.Length - 1].Outputs.Peek());
            }
        
            Console.WriteLine($"Star 2: {thrusterSignals.Max()}");
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
                intcoder.Inputs.Enqueue(0);
                for (int i = 0; i < 5; i++)
                {
                    intcoder.Reset(input, false);
                    intcoder.Inputs.Enqueue(parameters[i]);
                    intcoder.Inputs.Enqueue(intcoder.Exec().Last());
                    
                }
                thrusterSignals.Add(intcoder.Inputs.Peek());
            }
        
            Console.WriteLine($"Star 1: {thrusterSignals.Max()}");
        }
    }
}
