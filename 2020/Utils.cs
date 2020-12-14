using System.Linq;
using System;
using System.Collections.Generic;
namespace AoC2020
{
    public static class Utils
    {
        public static string RemoveWhiteSpaces(this string s) => new string(s.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        public static char[] ABC { get{return Enumerable.Range(97, 26).Select(n => (char)n).ToArray();}}
        public static readonly string NL = "\r\n";
        public static readonly string DNL = $"{NL}{NL}";
        public static Instruction ToInstruction(this string s) => new Instruction {Type = (Instructions)Enum.Parse(typeof(Instructions), s.Substring(0, 3)), Arg = int.Parse(s.Split(' ')[1]) };
        public static int ManhattanDistance(int x, int y) => Math.Abs(x) + Math.Abs(y);

        public static IEnumerable<int> IndexOfMany<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int i = 0;
            foreach (var element in source)
            {
                if (predicate(element))
                    yield return i;
                i++;
            }
        }
    }
}