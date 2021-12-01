using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day1 : IDay
    {
        public string Output => throw new NotImplementedException();
        public int[] Input = InputReader.GetInputLines<int>("1.txt");
        private int CountGreaterThenPrevius(IEnumerable<int> integers) => integers.Select((v, i) => new {Index = i, Value = v }).Skip(1).Count(t => t.Value > integers.ElementAt(t.Index-1));
        public int Star1() => CountGreaterThenPrevius(Input);
        public int Star2() => CountGreaterThenPrevius(Input.SkipLast(2).Select((v, i) => Input.Skip(i).Take(3).Sum()));   
    }
}