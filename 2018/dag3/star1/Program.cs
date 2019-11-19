using System;
using System.IO;
using System.Linq;

namespace star1
{
    class Program
    {
        static void Main(string[] args)
        {

            var fabric = new int[1000,1000];
            foreach (var claim in File.ReadAllLines("input.txt").Select(t => new Claim(t)))
            {
                for (int i = claim.FLeft; i < claim.FLeft + claim.Width; i++)
                {
                    for (int c = claim.FTop; c < claim.FTop + claim.Height; c++)
                    {
                        fabric[i,c]++;
                    }
                }
            }
            int cnt = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int c = 0; c < 1000; c++)
                {
                    if(fabric[i,c]>1)
                        cnt++;
                }
            }
            Console.WriteLine(cnt);
        }
    }

    public class Claim
    {
        public string ID{ get; set; }
        public int FLeft { get; set; }
        public int FTop { get; set; }
        public int Width { get; set; }
        public int Height  { get; set; }

        public Claim(string claim)
        {
            var split = claim.Split(" ");
            ID = claim.Substring(0, claim.IndexOf('@'));
            FLeft = int.Parse(split[2].Split(',')[0]);
            FTop = int.Parse(split[2].Split(',')[1].Trim(':'));
            Width = int.Parse(split[3].Split('x')[0]);
            Height = int.Parse(split[3].Split('x')[1]);
        }
    }
}
