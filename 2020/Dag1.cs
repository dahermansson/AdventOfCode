using System;

namespace AoC2020
{
    public class Dag1 : IDag
    {
        public int Star1(string input)
        {
             var expensReport = InputReader.GetInputLines(input);
            for (int i = 0; i < expensReport.Length; i++)
            {
                var current = int.Parse(expensReport[i]);
                for (int c = 0; c < expensReport.Length; c++)
                {
                    if(current + int.Parse(expensReport[c]) == 2020)
                        return current * int.Parse(expensReport[c]);
                }
            }
            return -1;
        }

        public int Star2(string input)
        {
            throw new NotImplementedException();
        }
    }
}