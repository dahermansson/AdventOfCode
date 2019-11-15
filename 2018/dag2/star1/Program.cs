using System;
using System.IO;
using System.Linq;

namespace star1
{
    class Program
    {
        static void Main(string[] args)
        {
            int two = 0;
            int three = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var count = line.GroupBy(t => t).ToDictionary(k =>  k.Key, t => t.Count());
                bool doneTwo = false;
                bool doneThree = false;
                foreach (var c in count.Where(t => t.Value == 2 || t.Value == 3))
                {
                    if(!doneTwo && c.Value == 2)
                    {
                        two++;
                        doneTwo = true;
                    }
                    if(!doneThree && c.Value == 3)
                    {
                        three++;
                        doneThree = true;
                    }
                }
            }

            Console.WriteLine(two * three);
        }
    }
}
