using System.Linq;
using System;
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

        public static long LCM(long a, long b) => (a / GCD(a, b)) * b;

        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}