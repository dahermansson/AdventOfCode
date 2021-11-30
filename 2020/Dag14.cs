using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using AoC.Utils;

namespace AoC2020
{
    public class Dag14 : IDag
    {
        public string Output => _output;
        private string _output;
        private List<string> input = InputReader.GetInputLines("dag14.txt").ToList();
        public int Star1()
        {
            var mask = string.Empty;
            Dictionary<long, long> memo = new Dictionary<long, long>();

            input.ForEach( line => 
            {
                if(line.StartsWith("mask"))
                    mask = line.Replace("mask = ", "");
                else
                {
                    var d = ReadLine(line);
                    memo[d.Item1] = ApplayMask(mask, d.Item2);
                }
            });

         _output = memo.Values.Sum().ToString();
         return -1;
        }

        public int Star2()
        {
            Dictionary<long, long> memo = new Dictionary<long, long>();
            var mask = string.Empty;
            input.ForEach( line => 
            {
                if(line.StartsWith("mask"))
                    mask = line.Replace("mask = ", "");
                else
                {
                    var d = ReadLine(line);
                    foreach (long adress in MAD(mask, d.Item1))
                        memo[adress] = d.Item2;

                }    
            });
            _output = memo.Values.Sum().ToString();
            return -1;
        }

        private static Tuple<long, long> ReadLine(string s)
        {
            var parts = s.Split(" ");
            var pointer = int.Parse(parts[0].Replace("mem[", "").Replace("]", ""));
            return new Tuple<long, long>(pointer, long.Parse(parts[2]));
        }

        private static Int64 ApplayMask(string mask, Int64 value)
        {
            var reverseMask = string.Join("", mask.Reverse());
            var bitArray = Int64ToBitArray(value);
            for (int i = 0; i < reverseMask.Length ; i++)
                if(reverseMask[i] != 'X')
                    bitArray.Set(i, reverseMask[i] == '1');
            return BitArrayToInt64(bitArray);
        }

        private static IEnumerable<long> MAD(string mask, long adress)
        {
            var reverseMask = string.Join("", mask.Reverse());
            var bitArray = Int64ToBitArray(adress);
            var floatingIndex = reverseMask.IndexOfMany(t => t == 'X').ToList();

            for (int i = 0 ; i < reverseMask.Length; i++)
                if(reverseMask[i] == '1')
                    bitArray.Set(i, reverseMask[i] == '1');
                
            for (int r = 0; r < Math.Pow(2, floatingIndex.Count); r++)
                yield return ApplayMask(CreateFloatingMask(r, floatingIndex), BitArrayToInt64(bitArray));
        }

        private static string CreateFloatingMask(int r, List<int> floatingIndex)
        {
            var bits = Int64ToBitArray(r);
            int bitIndex = 0;
            var mask = string.Join("", Enumerable.Range(0, 36).Select(t => "X")).ToCharArray();
            foreach (var fix in floatingIndex)
                mask[fix] = bits.Get(bitIndex++) ? '1' : '0';
            return string.Join("", mask.Reverse());
        }

        private static BitArray Int64ToBitArray(Int64 value) => new BitArray(BitConverter.GetBytes(value));

        private static Int64 BitArrayToInt64(BitArray bitArray)
        {
            byte[] bytes = new byte[64];
            bitArray.CopyTo(bytes, 0);
            return BitConverter.ToInt64(bytes, 0);

            
        }
    }
}