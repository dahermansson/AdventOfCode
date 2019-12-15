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
            //Star1();
            Star2();
        }

        static void Star2()
        {
            string input = File.ReadAllText("input.txt");
            //Console.WriteLine(input.Split(',').Length);
            //input = "3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26,27,4,27,1001,28,-1,28,1005,28,6,99,0,0,5";
            //input = @"3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5,54,1105,1,12,1,53,54,53,1008,54,0,55,1001,55,1,55,2,53,55,53,4,53,1001,56,-1,56,1005,56,6,99,0,0,0,0,10";
            var thrusterSignals = new List<int>();
            var settings = GetPermutations(new List<int>(){5,6,7,8,9}, 5).Select(t => new List<int>(t));
            
            foreach (var parameters in settings)
            {
                var inputQueue = new Queue<int>();
                //var parameters = new int[]{9,7,8,5,6};
                var intcoders = Enumerable.Range(0, 5).Select( t => new Intcoder(input, t +1)).ToArray();
                //intcoders[0].Inputs.Push(0);
                //for (int i = 0; i < intcoders.Length; i++)
                int activRunning = 0;
                inputQueue.Enqueue(parameters[0]);
                inputQueue.Enqueue(0);
                while(intcoders.Any(i => i.Running || !i.Done))
                {
                    var outputValue = intcoders[activRunning++].Exec(inputQueue);
                    //Console.WriteLine(outputValue);
                    if(activRunning == 5)
                        thrusterSignals.AddRange(outputValue);
                    
                    if(activRunning == 5)
                        activRunning = 0;

                    if(intcoders[activRunning].Pointer == 0)
                        inputQueue.Enqueue(parameters[activRunning]);
                    inputQueue.Enqueue(outputValue.Single());
                    //intcoders[i].Inputs.Push(parameters[i]);
                    //if(i == 0)
                        
                    //if(i == 4)
                    //{
                        //intcoders[i].OutputDelegate += intcoders[0].GetInput;
                    //}
                    //else
                        //intcoders[i].OutputDelegate += intcoders[i+1].GetInput;
                    //intcoders[i].OutputDelegate += (int value, string name) => {Console.WriteLine($"{name}: {value}");};
                }
                //intcoders[0].Exec(); 
                //thrusterSignals.Add(intcoders[0].Inputs.Peek());
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
                //var parameters = new int[]{1,0,4,3,2};
                Intcoder intcoder = new Intcoder(input, 0);
                //intcoder.OutputDelegate = (int value, string name) => {intcoder.Inputs.Push(value);};
                //intcoder.Inputs.Push(0);
                var inputQueue = new Queue<int>();
                for (int i = 0; i < 5; i++)
                {
                    inputQueue.Enqueue(parameters[0]);
                    inputQueue.Enqueue(0);
                        
                    intcoder.Reset(input, false);
                    intcoder.Exec(inputQueue); 
                }

                //thrusterSignals.Add(intcoder.Inputs.Last());
            }
        
       //     Console.WriteLine($"Star 1: {thrusterSignals.Max()}");
        }
    }
}
