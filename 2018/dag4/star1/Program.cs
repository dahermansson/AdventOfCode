using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace star1
{
    class Program
    {
        static void Main(string[] args)
        {
            var records = new List<Record>();
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                records.Add(new Record(line));
            }

            records.OrderBy(t => t.Time).ToList().ForEach(t => Console.WriteLine(t.Time));
        }
    }

    internal class Record
    {
        public DateTime Time { get; set; }

        public Record(string s)
        {
            Time = DateTime.Parse($"{s.Split(" ")[0].Trim('[')} {s.Split(" ")[1].Trim(']')}");
        }
    }
}
