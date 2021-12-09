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
                        if(((matrix.Grid[row, col] == 'L' && matrix.GetNeighbours(row,col).All(t => t.Value != '#'))
                        || 
                         (matrix.Grid[row, col] == '#' && matrix.GetNeighbours(row, col).Count(t => t.Value == '#') > 3)))
                            change.Add((row, col , matrix.Grid[row, col]));

                change.ForEach( seat => 
                {
                    matrix.Grid[seat.row, seat.col] = matrix.Grid[seat.row, seat.col] == '#' ? 'L' : '#';
                });
            }
            while(change.Count > 0);

            return matrix.GetAllValues().Count(t => t == '#');
        }

        public int Star2()
        {
            var matrix = new Matrix<char>(Input, false);
            var change = new List<MatrixPoint<char>>();
            do
            {
                change.Clear();
                for (int row = 0; row < matrix.Rows; row++)
                    for (int col = 0; col < matrix.Columns; col++)
                        if((matrix.Grid[row, col] == 'L' && 
                            matrix.NeighboursDef.Select(t => matrix.GetInDirection(t, row, col, p => p == '#' || p == 'L')).SelectMany(t => t).All(t => t.Value != '#'))
                            || 
                          (matrix.Grid[row, col] == '#' && 
                            matrix.NeighboursDef.Select(t => matrix.GetInDirection(t, row, col, p => p == '#' || p == 'L')).SelectMany(t => t).Count(t => t.Value == '#') > 4))
                                change.Add(matrix.Get(row, col));
                change.ForEach( seat => 
                {
                    matrix.Grid[seat.Row, seat.Column] = matrix.Grid[seat.Row, seat.Column] == '#' ? 'L' : '#';
                });
            }
            while(change.Count > 0);
            
            return matrix.GetAllValues().Count(t => t == '#');
        }
    }

}