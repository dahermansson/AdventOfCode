using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2017
{
    public class Day1 : IDay
    {
        public string Output => throw new NotImplementedException();
        private List<int> Input = InputReader.GetInput("1.txt").Select(s => int.Parse(s.ToString())).ToList();

        public int Star1()
        {
            Input.Add(Input.First());
            return Input.SkipLast(1).Select((value, index) => Input.Skip(index).Take(2)).Where(s => s.First() == s.Last()).Sum(v => v.First());
        }

        public int Star2()
        {
            //Input = new List<int>(){1,2,1,2,1};
            Input.AddRange(Input.Skip(1).SkipLast(1).ToList());
            var indexAhead = Input.Count/2;
            return Input.SkipLast(indexAhead).Select((value, index) => new {v1 = value, v2 = Input.ElementAt(index + indexAhead)}).Where(v => v.v1 == v.v2).Sum(v => v.v1);
            return 1;
        }
    }
}