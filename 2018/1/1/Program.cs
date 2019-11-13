using System;
using System.IO;
namespace _1
{
    class Program
    {
        static void Main(string[] args)
        {
            var summa = 0;
            foreach (var line in File.ReadAllLines("input.txt"))
                summa += int.Parse(line);
            Console.WriteLine(summa);
        }
    }
}
