using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace star2
{
    class Program
    {
        static void Main(string[] args)
        {
            var noOwerlaps = new List<int>();
            var fabric = new int[1000,1000];
            foreach (var claim in File.ReadAllLines("input.txt").Select(t => new Claim(t)))
            {
                bool overlap = false;
                for (int i = claim.FLeft; i < claim.FLeft + claim.Width; i++)
                {
                    for (int c = claim.FTop; c < claim.FTop + claim.Height; c++)
                    {
                        if(fabric[i,c] != 0)
                        {
                            overlap = true;
                            if(noOwerlaps.Any(t => t == fabric[i,c]))
                                noOwerlaps.Remove(fabric[i,c]);
                        }
                        fabric[i,c] = claim.ID;
                    }
                }
                if(!overlap)
                {
                    noOwerlaps.Add(claim.ID);
                }
            }
            noOwerlaps.ForEach(t => Console.WriteLine(t));
        }
    }

    public class Claim
    {
        public int ID { get; set; }
        public int FLeft { get; set; }
        public int FTop { get; set; }
        public int Width { get; set; }
        public int Height  { get; set; }

        public Claim(string claim)
        {
            var split = claim.Split(" ");
            ID = int.Parse(split[0].Trim('#'));
            FLeft = int.Parse(split[2].Split(',')[0]);
            FTop = int.Parse(split[2].Split(',')[1].Trim(':'));
            Width = int.Parse(split[3].Split('x')[0]);
            Height = int.Parse(split[3].Split('x')[1]);
        }
    }
}
