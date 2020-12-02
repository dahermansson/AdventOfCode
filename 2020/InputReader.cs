using System;
using System.IO;
using System.Linq;

namespace AoC2020
{
    public static class InputReader
    {
        public static string[] GetInputLines(string filename) => File.ReadAllLines(Path.Combine("inputs", filename));
        public static T[] GetInputLines<T>(string filename) => File.ReadAllLines(Path.Combine("inputs", filename)).Select( s => (T) Convert.ChangeType(s, typeof(T))).ToArray();
    }
}