using System;
using System.IO;

namespace AoC2020
{
    public static class InputReader
    {
        public static string[] GetInputLines(string filename)
        {
            return File.ReadAllLines(Path.Combine("inputs", filename));
        }
    }
}