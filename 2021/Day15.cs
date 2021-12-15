using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day15 : IDay
    {
        public string Output => throw new NotImplementedException();

        public Matrix<int> Matrix = new Matrix<int>(InputReader.GetInputLines("15.txt"), false);

        public int Star1()
        {
            MatrixPathFinding<int> graph = new MatrixPathFinding<int>(Matrix);
            var start = Matrix.Get(0,0);
            var end = Matrix.Get(Matrix.Rows - 1, Matrix.Columns - 1);
            graph.Dijkstra(start, end);
            return graph.GetCost(end);
        }

        public int Star2()
        {
            throw new NotImplementedException();
        }
    }
}