using System.Linq;
using System.Collections.Generic;
using System;
namespace AoC2020
{
    public class Dag10 : IDag
    {
        private int[] input = InputReader.GetInputLines<int>("dag10.txt");
        public int Star1()
        {
            var diff1 = 0; 
            var diff3 = 0; 
            var sorted = input.Append(0).Append(input.Max() + 3).OrderBy(t => t).ToList();
            for (int i = 0; i < sorted.Count -1; i++)
                if(sorted[i+1] - sorted[i] == 1)
                    diff1++;
                else
                    diff3++;
            
            return diff1 * diff3;
        }

        private long _star2Count = 0;
        private HashSet<int> _hashInput;
        private int _goal;
        public int Star2()
        {
            _hashInput = new HashSet<int>(input);
            _goal = input.Max();
            FindNext(input.Min());
            return -1;
        }

        private void FindNext(int start)
        {
            if(start == _goal)
            {
                _star2Count++;
                if(_star2Count % 100000 ==0)
                    Console.WriteLine(_star2Count);
                return;
            }
            int n1;
            int n2;
            int n3;
            if(_hashInput.TryGetValue(start +1, out n1))
                FindNext(n1);
            if(_hashInput.TryGetValue(start +2, out n2))
                FindNext(n2);            
            if(_hashInput.TryGetValue(start +3, out n3))
                FindNext(n3);
        }
        public string Output() => _star2Count.ToString();
    }
}