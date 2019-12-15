using System.Linq;
using System.Collections.Generic;

namespace AdventofCode.Utils
{
    public static class Utils
    {
        public static IEnumerable<T[]> Split<T>(IEnumerable<T> i, int batchSize)
        {
            int batches = i.Count()/batchSize;
            for (int c = 0; c < batches; c++)
                yield return i.Skip(c*batchSize).Take(batchSize).ToArray();
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