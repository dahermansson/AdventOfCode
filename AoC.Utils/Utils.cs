using System.Linq;
using System;
using System.Collections.Generic;
namespace AoC.Utils
{
    public static class Utils
    {
        public static string RemoveWhiteSpaces(this string s) => new string(s.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray());
        public static char[] ABC { get{return Enumerable.Range(97, 26).Select(n => (char)n).ToArray();}}
        public static readonly string NL = "\r\n";
        public static readonly string DNL = $"{NL}{NL}";
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
        public static IEnumerable<T> LoopMatrix<T>(this T[,] source, int row, int col)
        {
            for (int i = 0; i < row; i++)
                for (int c = 0; c < col; c++)
                    yield return source[i,c];
        }
        
    }
}