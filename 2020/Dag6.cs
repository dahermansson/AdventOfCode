using System;
using System.Linq;

namespace AoC2020
{
    public class Dag6: IDag
    {
        private static string[] Input = InputReader.GetInput("dag6.txt").Split(Utils.DNL);

        public int Star1() => Input.Sum(g => g.RemoveWhiteSpaces().Distinct().Count());

        public int Star2()
        {
            int sum = 0;
            foreach (var group in Input)
            {
                var abc = Utils.ABC;
                foreach (var g in group.Split(Utils.NL))
                    abc = abc.Intersect(g.ToCharArray()).ToArray();
                sum+=abc.Length;
            }
            return sum;
        }
    }
}