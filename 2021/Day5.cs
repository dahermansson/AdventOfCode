using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day5 : IDay
    {
        public string Output => throw new NotImplementedException();
        public IEnumerable<Line> Input = InputReader.GetInputLines("5.txt").Select(t => new Line(t, "->"));
        public int Star1() => AddLines(Input.Where(l => !l.IsDiagonal).ToArray());
        public int Star2() => AddLines(Input.ToArray());
        public int AddLines(Line[] lines)
        {
            var matrix = new Matrix<int>(lines.Max(l => l.MaxX) + 1, lines.Max(l => l.MaxY) + 1);
            foreach (var line in lines)
                AddLine(matrix, line);
            
            return matrix.GetAll().Count(t => t > 1);
        }
        private void AddLine(Matrix<int> matrix, Line line)
        {
            var start = line.Start;
            var stop = line.End;
            if(!line.IsDiagonal)
            {
                var startX = new int[] {start.X, stop.X}.Min();
                var stopX = new int[] {start.X, stop.X}.Max();
                var startY = new int[] {start.Y, stop.Y}.Min();
                var stopY = new int[] {start.Y, stop.Y}.Max();
            
                for (int x = startX; x <= stopX; x++)
                    for (int y = startY; y <= stopY; y++)
                        matrix.Grid[x,y]++;
            }
            else
            {
                var steps = Math.Abs(stop.Y - start.Y);
                var x = start.X;
                var y = start.Y;
                for (int i = 0; i <= steps; i++)
                {
                    matrix.Grid[x, y]++;
                    if(start.X < stop.X) x++; else x--;
                    if(start.Y < stop.Y) y++; else y--;
                }
            }
        }
    }
}