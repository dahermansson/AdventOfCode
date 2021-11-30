using System.Linq;
using System.Collections.Generic;
using System;
using AoC.Utils;

namespace AoC2020
{
    public class Dag10 : IDay
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
        private HashSet<int> _hashInput = new HashSet<int>();
        public int Star2()
        {
            List<long> parts = new List<long>();
            var sorted = new List<int>(input.Append(0).OrderBy(t => t));
            for (int i = 0; i < sorted.Count -1 ; i++)
            {
                _hashInput.Add(sorted[i]);
                if(sorted[i + 1] - sorted[i] == 3)
                {
                    FindNext(_hashInput.Min(), _hashInput.Max());
                    if(_star2Count != 0)
                    {
                        parts.Add(_star2Count);
                        _star2Count = 0;
                    }
                    _hashInput.Clear();
                }
            }
            _star2Count =  parts.Aggregate((t , p) => t*p);
            return -1;
        }

        private void FindNext(int start, int goal)
        {
            if(start == goal)
            {
                _star2Count++;
                return;
            }
            int n1;
            int n2;
            int n3;
            if(_hashInput.TryGetValue(start +1, out n1))
                FindNext(n1, goal);
            if(_hashInput.TryGetValue(start +2, out n2))
                FindNext(n2, goal);            
            if(_hashInput.TryGetValue(start +3, out n3))
                FindNext(n3, goal);
        }
        public string Output => _star2Count.ToString();
    }
}