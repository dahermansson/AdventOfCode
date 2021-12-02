using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day2 : IDay
    {
        public string Output => throw new NotImplementedException();
        public int Star1()
        {
            var a = InputReader.GetInputLines("2.txt").Select(t => new { cmd = t.Split(" ")[0], val = int.Parse(t.Split(" ")[1])}).GroupBy(t => t.cmd).ToDictionary(t => t.Key, t => t.Sum(g => g.val));
            return a["forward"] * (a["down"] - a["up"]);
        }

        public int Star2()
        {
            int aim = 0, hp = 0, d = 0;
            InputReader.GetInputLines("2.txt").Select(t => new {cmd = t.Split(" ")[0], val = int.Parse(t.Split(" ")[1])}).ToList().ForEach(cmd => 
            {
                switch (cmd.cmd)
                {
                    case "up": aim -= cmd.val; break;
                    case "down": aim += cmd.val; break;
                    case "forward":
                        hp += cmd.val;
                        d += aim * cmd.val;
                        break;
                }
            });
            return hp * d;
        }
    }
}