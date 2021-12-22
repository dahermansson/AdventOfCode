using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day17 : IDay
    {
        public string Output => throw new NotImplementedException();
        private int yMin = -144;
        private int yMax = -98;
        private int xMin = 96;
        private int xMax = 125;
        public int Star1()
        {
            var hits = new List<int[]>();
            for (int i = Math.Abs(yMax); i < Math.Abs(yMin); i++)
            {
                var yn = CalcY(i).ToArray();
                if(yn.Any(y => InYRange(y)))
                    hits.Add(yn);     
            }
            return hits.Max(m => m.Max(y => y));
        }

        public int Star2()
        {
            var hits = 0;
            for (int y = yMin; y <= Math.Abs(yMin); y++)
                for (int x = 1; x <= xMax+1; x++)
                    if(Trejectory(x, y))
                        hits++;
            return hits;
        }
        public bool InYRange(int i) => (i >= yMin && i <= yMax);
        public bool InXRange(int i) => (i >= xMin && i <= xMax);
        public bool InRange(int x, int y) => InXRange(x) && InYRange(y);
        public bool Trejectory(int vx, int vy)
        {
            var yn = CalcY(vy).ToArray();
            var xn = CalcX(vx).ToArray();
            for (int i = 0; i < yn.Length ; i++)
                if(InRange(i >= xn.Length ? xn.Last() : xn[i], yn[i]))
                    return true;
            return false;
        }

        public IEnumerable<int> CalcY(int i)
        {
            var v = i;
            while (i >= yMin)
            {
                yield return i;
                i += --v;
            }
        }

        public IEnumerable<int> CalcX(int i)
        {
            var v = i;
            while (v != 0)
            {
                yield return i;
                    i += --v;
            }
        }
    }
}