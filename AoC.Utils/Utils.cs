using System.Linq;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

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
            //source.Select((e, i) => new {Index = i, Value = e}).Where(t => predicate(t.Value)).Select( t => t.Index);
            int i = 0;
            foreach (var element in source)
            {
                if (predicate(element))
                    yield return i;
                i++;
            }
        }

        public static IEnumerable<T> GetAll<T>(this T[,] source)
        {
            for (int row = 0; row < source.GetLength(0); row++)
                for (int col = 0; col < source.GetLength(1); col++)
                    yield return source[row,col];
        }

        public static IEnumerable<char> GetColumn(this string[] source, int index)
        {
            if(!source.All(t => t.Length == source[0].Length))
                throw new Exception("not square input");
            for (int i = 0; i < source.Length; i++)
                yield return source[i][index];
        }

        public static BitArray ToBitArray(this string s) => new BitArray(s.Reverse().Select(t => t == '1').ToArray());
        public static int ToInt(this BitArray bits)
        {
            int[] res = new int[1];
            bits.CopyTo(res, 0);
            return res[0];
        }
        public static int ToIntRev(this BitArray bits)
        {
            var temp = new BitArray(bits.Length);
            int index = 0;
            for (int i = bits.Length -1; i > -1; i--)
                temp[index++] = bits[i];
            int[] res = new int[1];
            temp.CopyTo(res, 0);
            return res[0];
        }

        public static IEnumerable<IEnumerable<T>> Permutations<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            // Ensure that the source IEnumerable is evaluated only once
            return permutations(source.ToArray());
        }

        private static IEnumerable<IEnumerable<T>> permutations<T>(IEnumerable<T> source)
        {
            var c = source.Count();
            if (c == 1)
                yield return source;
            else
                for (int i = 0; i < c; i++)
                    foreach (var p in permutations(source.Take(i).Concat(source.Skip(i + 1))))
                        yield return source.Skip(i).Take(1).Concat(p);
        }
    }
}