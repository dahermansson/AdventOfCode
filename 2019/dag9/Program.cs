using System;
using System.Collections.Generic;
using System.Numerics;
using AdventOfCode2019;


namespace dag9
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
            Star2();
        }

        static void Star1()
        {
            var res = new Intcoder(Utils.ReadAllInput("input.txt"), OutputMode.ReturnAndHalt).Exec(new Queue<BigInteger>(new BigInteger[]{1}));
            Console.WriteLine($"star 1: {res}");
        }
        static void Star2()
        {
            var res = new Intcoder(Utils.ReadAllInput("input.txt"), OutputMode.ReturnAndHalt).Exec(new Queue<BigInteger>(new BigInteger[]{2}));
            Console.WriteLine($"star 2: {res}");
        }
    }
}
