using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = 0;
            var lines = new List<string>();
            foreach (var line in File.ReadAllLines("input.txt"))
            {   
                sum += int.Parse(line);
                lines.Add(line);
            }
            Console.WriteLine(sum);
            sum = 0;
            var freq = new List<int>();
            var cnt = 0;
            do 
            {
                freq.Add(sum);
                sum += int.Parse(lines[cnt++]);
                if(cnt == lines.Count())
                    cnt = 0;
            } while (freq.IndexOf(sum) < 0);
            Console.WriteLine(sum);
        }
    }
}

