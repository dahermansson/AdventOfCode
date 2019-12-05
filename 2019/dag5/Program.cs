using System.IO;
using AdventOfCode2019;

namespace dag5
{
    class Program
    {
        static void Main(string[] args)
        {
            Star(new int[]{1});
            Star(new int[]{5});
        }

        static void Star(int[] input)
        {
            var intcoder = new Intcoder(File.ReadAllText("input.txt"));
            intcoder.inputs = input;
            var pointer = 0;
            do
                pointer = intcoder.Exec(pointer);
            while (pointer != -1);
        }
    }
}
