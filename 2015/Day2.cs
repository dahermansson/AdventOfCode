using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2015
{
    public class Day2 : IDay
    {
        public string Output => throw new NotImplementedException();
        private IEnumerable<int[]> Input = InputReader.GetInputLines("2.txt").Select(t => t.Split("x").Select(t => int.Parse(t)).ToArray());
        public int Star1() => Input.Select(t => new { l = t[0]*t[1], w = t[1] * t[2], h = t[2]*t[0]}).Select(t => 2 * t.l + 2 * t.w + 2 * t.h + new int[]{t.l, t.w, t.h}.Min()).Sum();
        public int Star2() => Input.Select(t => new { sides = t.OrderBy(a => a).Take(2).ToArray(), bow = t[0] * t[1] * t[2]}).Select(t => t.sides[0]+t.sides[0]+t.sides[1]+t.sides[1] + t.bow).Sum();
    }
}