using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2019
{
    public static class Utils
    {
        public static IEnumerable<T[]> Split<T>(IEnumerable<T> i, int batchSize)
        {
            int batches = i.Count()/batchSize;
            for (int c = 0; c < batches; c++)
                yield return i.Skip(c*batchSize).Take(batchSize).ToArray();
        }
		
		public static IEnumerable<IEnumerable<T>>GetPermutations<T>(IEnumerable<T> list, int length)
        {
        if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        public static string ReadAllInput(string file)
        {
            return File.ReadAllText(file);
        }

        private static int ManhattanDistance(int x1, int x2, int y1, int y2)
        {
            return System.Math.Abs(x1 - x2) + System.Math.Abs(y1 - y2);
        }
    }

    

    public static class Extensions
    {
        public static IEnumerable<int> ToIntArray(this string s) 
        {
            return s.Select(t => int.Parse(t.ToString()));
        }
    }

}