using System;
using System.IO;
using System.Linq;

namespace AoC2015
{
    public static class InputReader
    {
        public static string GetInputLine(string filename) => File.ReadAllText(Path.Combine("inputs", filename));
        public static string[] GetInputLines(string filename) => File.ReadAllLines(Path.Combine("inputs", filename));
        public static T[] GetInputLines<T>(string filename) => File.ReadAllLines(Path.Combine("inputs", filename)).Select( s => (T) Convert.ChangeType(s, typeof(T))).ToArray();
        public static string[][] GetInputLinesMatrix(string filename) => 
            File.ReadAllLines(Path.Combine("inputs", filename)).Select(t => t.ToCharArray().Select(p => p.ToString()).ToArray()).ToArray();
    }
}