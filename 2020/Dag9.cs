using System.Collections.Generic;
using System.Linq;

namespace AoC2020
{
    public class Dag9 : IDag
    {
        private double[] input = InputReader.GetInputLines<double>("dag9.txt");
        public int Star1()
        {
            int start = 24;
            while (FindSumInRange(input.Skip(++start - 25).Take(25).ToList(), input[start])) {}
            return (int)input[start];
        }

        private bool FindSumInRange(List<double> range, double sum)
        {
            for (int i = 0; i < range.Count; i++)
                for (int c = 0; c < range.Count; c++)
                    if(range[i] != range[c] && range[i] + range[c] == sum)
                        return true;
            return false;
        }

        public int Star2()
        {
            var goal = Star1();
            int start = 0;
            int count = 2;
            double sum = 0;
            while (goal != sum)
            {
                sum = input.Skip(start).Take(count++).Sum();
                if(sum > goal)
                {
                    count = 2;
                    start++;
                }    
            }
            return (int)(input.Skip(start).Take(count).Min() + input.Skip(start).Take(count).Max());
        }
    }
}