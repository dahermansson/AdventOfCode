using System.Collections.Generic;
using System.Linq;
using System;
using AoC.Utils;

namespace AoC2020
{
    public class Dag11 : IDay
    {
        private string[] Input = InputReader.GetInputLines("dag11.txt");
        public string Output => throw new System.NotImplementedException();
        public int Star1()
        {
            var matrix = new Matrix<char>(Input, false);
            var change = new List<(int row, int col, char value)>();
            do
            {
                change.Clear();
                for (int row = 0; row < matrix.Rows; row++)
                    for (int col = 0; col < matrix.Columns; col++)
                        if(((matrix.Grid[row, col] == 'L' && matrix.GetNeighbours(row,col).All(t => t.value != '#'))
                        || 
                         (matrix.Grid[row, col] == '#' && matrix.GetNeighbours(row, col).Count(t => t.value == '#') > 3)))
                            change.Add((row, col , matrix.Grid[row, col]));

                change.ForEach( seat => 
                {
                    matrix.Grid[seat.row, seat.col] = matrix.Grid[seat.row, seat.col] == '#' ? 'L' : '#';
                });
            }
            while(change.Count > 0);

            return matrix.GetAll().Count(t => t == '#');
        }

        public int Star2()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Position : IPosition
    {
        private Position[,] _matrix;
        public int X { get; set; }
        public int Y { get; set; }
        public char Value {get; set;}

        public Position(int x, int y, ref Position[,] matrix, char value)
        {
            _matrix = matrix;
            X = x;
            Y = y;
            Value = value;
        }

        private IEnumerable<Tuple<int, int>> NeighboursDef = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(-1, -1),
            new Tuple<int, int>(-1, 0),
            new Tuple<int, int>(-1, 1),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, -1),
            new Tuple<int, int>(1, 0),
            new Tuple<int, int>(1, 1)
        };

        public IEnumerable<Position> GetNeighbours()
        {
            var inMatrix = NeighboursDef.Where(t => X + t.Item1 >= 0 && X + t.Item1 < _matrix.GetLength(0) && Y + t.Item2 >= 0 && Y + t.Item2 < _matrix.GetLength(1)).ToList();
            return inMatrix.Select(t => _matrix[X + t.Item1, Y +t.Item2]);
        }
    }

    public interface IPosition
    {
        public int X {get; set;}
        public int Y {get; set;}
        public IEnumerable<Position> GetNeighbours();
    }
}