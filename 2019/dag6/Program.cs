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
            Star2();
        }

        static void Star2()
        {
            var map = new List<Obj>();
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var name = line.Split(')')[1];
                var directOrbit = line.Split(')')[0];
                map.Add(new Obj(name, directOrbit));
            }

            var start = map.First(t => t.Name == "YOU").DirectOrbit;
            var goal = map.First(t => t.Name == "SAN").DirectOrbit;

            FindSanta(map, start, goal);
        }

        static void FindSanta(List<Obj> map, string start, string goal)
        {
            var openSet = new List<Obj>(){map.First(t => t.Name == start)};
            var cameFrom = new Dictionary<string, string>();
            var cost = new Dictionary<string, int>();
            cost.Add(start, 0);
            var estCost = new Dictionary<string, int>();
            estCost.Add(start, map.Count);

            while(openSet.Count > 0)
            {
                var current = openSet.First();
                if(current.Name == goal)
                {
                    Console.WriteLine($"Star 2: {cost[goal]}");
                    return;
                }
                openSet.Remove(current);
                foreach (var neighbour in map.Where(t => t.DirectOrbit == current.Name || t.Name == current.DirectOrbit))
                {
                    var currentCost = cost[current.Name] + 1;

                    if(!cost.ContainsKey(neighbour.Name) || currentCost < cost[neighbour.Name])
                    {
                        if(!cameFrom.ContainsKey(neighbour.Name))
                            cameFrom.Add(neighbour.Name, current.Name);
                        else
                            cameFrom[neighbour.Name] = current.Name;
                        if(!cost.ContainsKey(neighbour.Name))
                            cost.Add(neighbour.Name, currentCost);
                        else
                            cost[neighbour.Name] = currentCost;

                        if(!estCost.ContainsKey(neighbour.Name))
                            estCost.Add(neighbour.Name, map.Count + cost[neighbour.Name]);
                        else
                            estCost[neighbour.Name] = map.Count + cost[neighbour.Name];

                        if(!openSet.Any(t => t.Name == neighbour.Name))
                            openSet.Add(neighbour);
                    }
                }
            }

        }

        static void Star1()
        {
            var dLines = new List<Obj>();
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                var obj = line.Split(')')[1];
                var directOrbit = line.Split(')')[0];
                dLines.Add(new Obj(obj, directOrbit));
            }
            var stack = new Stack<Obj>();
            stack.Push(new Obj("COM", string.Empty));

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
        public Obj(string name, string orbit)
        {
            Name = name;
            DirectOrbit = orbit;
        }
        public string Name { get; set; }
        public string DirectOrbit { get; set; }
    }
}
