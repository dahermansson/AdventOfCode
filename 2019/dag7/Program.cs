using System;
using AdventOfCode2019;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace dag7
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
            string input = File.ReadAllText("input.txt");
            var thrusterSignals = new List<BigInteger>();
            var settings = Utils.GetPermutations(new List<int>(){5,6,7,8,9}, 5).Select(t => new List<int>(t));
            
            foreach (var parameters in settings)
            {
                var intcoders = Enumerable.Range(0, 5).Select( t => new Intcoder(input, OutputMode.ReturnAndHalt)).ToArray();
                int activRunning = 0;
                var inputQueue = new Queue<BigInteger>(new BigInteger[]{parameters[0], 0});
                while(intcoders.Any(i => !i.Done))
                {
                    var outputValue = intcoders[activRunning++].Exec(inputQueue);
                    if(activRunning == 5)
                    {
                        thrusterSignals.Add(outputValue);
                        activRunning = 0;
                    }
                    if(intcoders[activRunning].Pointer == 0)
                        inputQueue.Enqueue(parameters[activRunning]);
                    inputQueue.Enqueue(outputValue);
                }
            }
            Console.WriteLine($"Star 2: {thrusterSignals.Max()}");
        }
        static void Star1()
        {
            string input = File.ReadAllText("input.txt");
            var thrusterSignals = new List<BigInteger>();
            var settings = Utils.GetPermutations(new List<BigInteger>(){0,1,2,3,4}, 5).Select(t => new List<BigInteger>(t));
            
            foreach (var parameters in settings)
            {
                Intcoder intcoder = new Intcoder(input, OutputMode.OutputAndRunToEnd);
                intcoder.Outputs.Add(0);
                for (int i = 0; i < 5; i++)
                    intcoder.Exec(new Queue<BigInteger>(new BigInteger[]{parameters[i], intcoder.Outputs.Last()}), true);
                thrusterSignals.Add(intcoder.Outputs.Last());
            }
            Console.WriteLine($"Star 1: {thrusterSignals.Max()}");
        }
    }
}
