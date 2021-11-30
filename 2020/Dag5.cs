using System;
using System.Linq;
using AoC.Utils;

namespace AoC2020
{
    public class Dag5 : IDay
    {
        public static int[] Input = InputReader.GetInputLines("dag5.txt").Select(s => ToInt(s)).ToArray();
        public int Star1() => Input.Max();
        public int Star2() => Enumerable.Range(2*8+0, 126*8 + 7).Where(seat => !Input.Contains(seat) && Input.Contains(seat -1) && Input.Contains(seat +1)).SingleOrDefault();
        public static int ToInt(string notBinery)
        {
            var binery = notBinery.Replace("F", "0").Replace("B", "1").Replace("R", "1").Replace("L", "0");
            return Convert.ToInt32(binery.Substring(0, 7), 2) * 8 + Convert.ToInt32(binery.Substring(7, 3), 2);
        }
        public string Output => throw new System.NotImplementedException();
    }
}