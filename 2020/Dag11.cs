using System.Collections.Generic;
using System.Linq;
using System;
using AoC.Utils;

namespace AoC2020
{
    public class Dag11 : IDay
    {
        private string[][] Input = InputReader.GetInputLinesMatrix("dag11.txt");
        public string Output => throw new System.NotImplementedException();
        public int Star1()
        {
            var _matrix = new Position[Input.Length, Input[0].Length];
            for (int i = 0; i < _matrix.GetLength(0); i++)
                for (int c = 0; c < _matrix.GetLength(1); c++)
                    _matrix[i,c] = new Position(i, c, ref _matrix, Input[i][c][0]);

            List<Position> change = new List<Position>();
            do
            {
                change.Clear();
                for (int i = 0; i < _matrix.GetLength(0); i++)
                    for (int c = 0; c < _matrix.GetLength(1); c++)
                        if(((_matrix[i,c].Value == 'L' && _matrix[i,c].GetNeighbours().All(t => t.Value != '#'))
                        || 
                         (_matrix[i,c].Value == '#' && _matrix[i,c].GetNeighbours().Count(t => t.Value == '#') > 3)))
                            change.Add(_matrix[i,c]);

                change.ForEach( c => 
                {
                    c.Value = c.Value == '#' ? 'L' : '#';
                });
            }
            while(change.Count > 0);

            return _matrix.LoopMatrix(_matrix.GetLength(0), _matrix.GetLength(1)).Count(t => t.Value == '#');
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