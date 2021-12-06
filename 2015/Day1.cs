using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2015
{
    public class Day1 : IDay
    {
        public string Output => throw new NotImplementedException();
        private string Input = InputReader.GetInput("1.txt");
        public int Star1() => Input.Count(t => t == '(') - Input.Count(t => t == ')');
        public int Star2()
        {
            int floor = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                if(floor == -1)
                    return i;
                if(Input[i] == '(')
                    floor++;
                else
                    floor--;
            }  
            return -1;
        }
    }
}