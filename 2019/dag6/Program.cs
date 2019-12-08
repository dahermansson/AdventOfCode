using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace dag6
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
        }

        static void Star1()
        {
            var dLines = new List<Obj>();
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var obj = line.Split(')')[1];
                var directOrbit = line.Split(')')[0];
                dLines.Add(new Obj(){DirectOrbit = directOrbit, Name = obj});
            }
            var stack = new Stack<Obj>();
            stack.Push(new Obj(){Name = "COM", DirectOrbit = string.Empty});

            int orbits = 0;
            var orbitsIndex = new Dictionary<string, int>();
            orbitsIndex.Add("COM", -1);
            while (stack.Count > 0)
            {
                var obj = stack.Pop();
                orbits += orbitsIndex[obj.Name]+1;
                foreach(var o in dLines.Where(t => t.DirectOrbit == obj.Name))
                {
                    stack.Push(o);
                    orbitsIndex.Add(o.Name, orbitsIndex[obj.Name] + 1);
                }
            }
            Console.WriteLine($"Star 1: {orbits}");
        }
    }

    class Obj
    {
        public string Name { get; set; }
        public string DirectOrbit { get; set; }
    }
}
