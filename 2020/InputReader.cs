using System;
using System.IO;
using System.Linq;

namespace AoC2020
{
    public static class InputReader
    {
        public static string[] GetInputLines(string filename)
        {
            return File.ReadAllLines(Path.Combine("inputs", filename));
        }
        public static int[] GetIntegerInputLines(string filename)
        {
            return File.ReadAllLines(Path.Combine("inputs", filename)).Select(t => int.Parse(t)).ToArray();
        }
    }
}