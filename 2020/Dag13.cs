using System;
using System.Linq;
using System.Collections.Generic;
namespace AoC2020
{
    public class Dag13 : IDag
    {
        public string Output {get ;set;}

        private int depart = int.Parse(InputReader.GetInputLines("dag13.txt")[0]);
        private int[] busses = InputReader.GetInputLines("dag13.txt")[1].Split(",").Where(t => t != "x").Select(t => int.Parse(t)).ToArray();
        public int Star1()
        {
            var theBus = busses.Select(b => (b, (b * Math.Ceiling((decimal)depart / b) - depart))).OrderBy(t => t.Item2).First();
            return (int)theBus.Item2 * theBus.b;
        }

        public int Star2()
        {
            throw new NotImplementedException();
            var busses = InputReader.GetInputLines("dag13.txt")[1].Split(",");

            var a = new Dictionary<int, int>();
            for (int i = 0; i < busses.Length; i++)
            {
                if(busses[i] != "x")
                    a.Add(i, int.Parse(busses[i]));
            }

            var max = a.OrderBy(t => t.Value).Last();
            long t = max.Value - max.Key;
            var p = max.Value; 

            while (true)
            {
                if(!(a.Any(b => (t + b.Key) % b.Value != 0)))
                {
                    Output = t.ToString();
                    return -1;
                }

                    if((t + a.ElementAt(1).Key) % a.ElementAt(1).Value == 0 )
                        if((t + a.ElementAt(2).Key) % a.ElementAt(2).Value == 0 )
                            if((t + a.ElementAt(3).Key) % a.ElementAt(3).Value == 0 )
                                if((t + a.ElementAt(4).Key) % a.ElementAt(4).Value == 0 )
                                {
                                    Console.WriteLine($"{t}");
                            //        return -1;
                                }
                t += a.First().Value; 
            }
           






            return -1;
        }
    }
}