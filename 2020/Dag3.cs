using System.Collections.Generic;
using System.Linq;
using AoC.Utils;

namespace AoC2020
{
    public class Dag3 : IDay
    {
        private static string[][] InputLines = InputReader.GetInputLinesMatrix("dag3.txt");
        public int Star1() => TobogganRun((1, 3));
        public int Star2() => DoTobogganRuns(new [] { (1,1),(1,3),(1,5),(1,7),(2,1) }).Aggregate((prod, next) => prod * next);

        private IEnumerable<int> DoTobogganRuns((int x, int y)[] slopes)
        {
            foreach (var slope in slopes)
                yield return TobogganRun(slope);    
        }   

        private int TobogganRun((int x, int y) slope)
        {
            var position = (x: 0, y: 0);
            var treeCount = 0;
            while (position.x < InputLines.Length)
            {
                if(InputLines[position.x][position.y % InputLines[0].Length] == "#")
                   treeCount++;
                position.x += slope.x;
                position.y += slope.y;
            }
            return treeCount;
        }
        public string Output => throw new System.NotImplementedException();
    }
}