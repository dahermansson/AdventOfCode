using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace dag1
{
    class Program
    {
        static void Main(string[] args)
        {
            var masses = File.ReadAllLines("input.txt");
            var fuelCount = new List<int>();
            foreach (var mass in masses)
            {
                fuelCount.Add(int.Parse(mass)/3 -2);
            }
            
            int totalFuel = fuelCount.Sum();
            Console.WriteLine($"Sum: {totalFuel}");
            fuelCount.Clear();

            foreach (var mass in masses)
            {
                fuelCount.Add(int.Parse(mass)/3 -2);
                int addedFuel = fuelCount.Last();
                while (addedFuel > 0)
                {
                    addedFuel = addedFuel/3 -2;
                    if(addedFuel > 0)
                    {
                        fuelCount.Add(addedFuel);
                        addedFuel = fuelCount.Last();
                    }
                }
            }

            Console.WriteLine($"Sum: {fuelCount.Sum()}");

        }
    }
}
