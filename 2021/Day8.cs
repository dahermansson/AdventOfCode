using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day8 : IDay
    {
        public string Output => throw new NotImplementedException();

        public string[] Input = InputReader.GetInputLines("8.txt").ToArray();
        public int Star1() => string.Join(" ", Input.Select(t => t.Substring(t.IndexOf("|")))).Split(" ").Count(t => new int[]{2, 4, 3, 7}.Contains(t.Length));

        public int Star2()
        {
            int sum = 0;
            foreach (var line in Input)
            {
                var inputSignalPaterns = line.Split("|")[0].Split(" ").Select(t => t.Trim()).ToArray();
                var digitOutput = line.Split("|")[1].Trim().Split(" ").Select(t => t.Trim()).ToArray();
                
                var lut = new Dictionary<int, string>();
                lut.Add(1, inputSignalPaterns.Single(t => t.Length == 2));
                lut.Add(4, inputSignalPaterns.Single(t => t.Length == 4));
                lut.Add(7, inputSignalPaterns.Single(t => t.Length == 3));
                lut.Add(8, inputSignalPaterns.Single(t => t.Length == 7));

                NineSixAndZero(lut[4], lut[7], inputSignalPaterns.Where(t => t.Length == 6).ToArray()).ToList().ForEach(t => {
                    lut.Add(t.Key, t.Value);
                });
                var diff = string.Join("", lut[8].Where(t => !lut[4].Contains(t)).ToArray());
                TwoThreeAndFive(lut[1], diff, inputSignalPaterns.Where(t => t.Length == 5).ToArray()).ToList().ForEach(t => {
                    lut.Add(t.Key, t.Value);
                });
                sum += int.Parse(string.Format("{0}{1}{2}{3}", FindNumber(digitOutput[0], lut) ,FindNumber(digitOutput[1], lut) , FindNumber(digitOutput[2], lut) , FindNumber(digitOutput[3], lut)));
            }
            return sum;
        }

        private int FindNumber(string v, Dictionary<int, string> lut)
        {
            return lut.Single(t => t.Value.Length == v.Length && v.All(p => t.Value.Contains(p))).Key;
        }

        private IEnumerable<KeyValuePair<int, string>> NineSixAndZero(string four, string seven, IEnumerable<string> patterns)
        {
            foreach (var pattern in patterns)
            {
                if(four.All(t => pattern.Contains(t)))
                    yield return new KeyValuePair<int, string>(9, pattern);
                else if(seven.All(t => pattern.Contains(t)))
                    yield return new KeyValuePair<int, string>(0, pattern);
                else
                    yield return new KeyValuePair<int, string>(6, pattern);
            }

        }

        private IEnumerable<KeyValuePair<int, string>> TwoThreeAndFive(string one, string diff, IEnumerable<string> patterns)
        {
            foreach (var pattern in patterns)
            {
                if(one.All(t => pattern.Contains(t)))
                    yield return new KeyValuePair<int, string>(3, pattern);
                else if(diff.All(t => pattern.Contains(t)))
                    yield return new KeyValuePair<int, string>(2, pattern);
                else
                    yield return new KeyValuePair<int, string>(5, pattern);
            }

        }


        private string FindThree(string[] possibleThrees, string one)
        {
                return "";
        }
    }
}