using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC.Utils
{
    //X ->
    //0 1 2 3
    //1 Y
    //2 |
    //3 V

    public class MatrixPoint<T>
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public T? Value { get; set; }
        public MatrixPoint(int row, int col, T value)
        {
            Row = row;
            Column = col;
            Value = value;
        }
        public string PosToString { get {return $"{Row}:{Column}";}}
    }

    public class Matrix<T>
    {
        private T[,] _matrix;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string[] Input { get; private set; }
        public bool SpaceSeparator { get; set; }
        public T[,] Grid { get {return _matrix;} }


        public Matrix(int rows, int cols, bool square = true)
        {
            Rows = square ? new int[]{ rows , cols}.Max() : rows;
            Columns = square ? Rows : cols;
            _matrix = new T[Rows, Columns];
            Input = new string[1];
        }

        public Matrix(string[] input, bool spaceSeperator)
        {
            Input = input;
            SpaceSeparator = spaceSeperator;
            Rows = input.Length;
            Columns = spaceSeperator ? input[0].Split(" ").Length : input[0].Length;

            _matrix = new T[Rows, Columns];
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    _matrix[row, col] = (T) Convert.ChangeType(GetValueFromInput(row, col), typeof(T));
        }

        private string GetValueFromInput(int row, int col) => SpaceSeparator ? Input[row].Split(" ")[col] : Input[row][col].ToString();

        public string GetPrintable()
        {
            var res = new StringBuilder();
            foreach (var row in GetAllRows())
            {
                foreach (var col in row)
                    res.Append(col?.ToString());
                res.AppendLine();
            }
            return res.ToString();
        }

        public MatrixPoint<T> Get(int row, int col)
        {
            return new MatrixPoint<T>(row, col, _matrix[row, col]);
        }
        public IEnumerable<T> GetAllValues()
        {
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    yield return _matrix[row,col];
        }

        public IEnumerable<string> GetStrings()
        {
            foreach (var row in GetAllRows())
                yield return string.Join("", row);
        }

        public void UpdateAll(Func<T, T> update)
        {
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    _matrix[row, col] = update(_matrix[row, col]);
        }

        public void Update(int row, int col, Func<T, T> update)
        {
            _matrix[row, col] = update(_matrix[row, col]);
        }

        public IEnumerable<MatrixPoint<T>> GetAllPositions()
        {
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    yield return new MatrixPoint<T>(row, col, _matrix[row,col]);
        }
        
        public IEnumerable<T> GetRow(int row)
        {
             for (int i = 0; i < Columns; i++)
                yield return _matrix[row, i];
        }

        public IEnumerable<MatrixPoint<T>> GetRowPositions(int row)
        {
             for (int i = 0; i < Columns; i++)
                yield return new MatrixPoint<T>(row, i, _matrix[row, i]);
        }
        public IEnumerable<IEnumerable<T>> GetAllRows()
        {
            for (int row = 0; row < Rows; row++)
                yield return GetRow(row);
        }
        public IEnumerable<IEnumerable<T>> GetAllColumns()
        {
            for (int col = 0; col < Columns; col++)
                yield return GetColumn(col);
        }

        public IEnumerable<T> GetColumn(int col)
        {
             for (int i = 0; i < Rows; i++)
                yield return _matrix[i, col];
        }

        public IEnumerable<MatrixPoint<T>> GetColumnPositions(int col)
        {
             for (int i = 0; i < Rows; i++)
                yield return new MatrixPoint<T>(i, col, _matrix[i, col]);
        }

        public IEnumerable<Tuple<int, int>> NeighboursDef = new List<Tuple<int, int>>()
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

        private IEnumerable<Tuple<int, int>> CrossNeighboursDef = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(-1, 0),
            new Tuple<int, int>(0, -1),
            new Tuple<int, int>(0, 1),
            new Tuple<int, int>(1, 0),
        };

        private IEnumerable<Tuple<int, int>> XNeighboursDef = new List<Tuple<int, int>>()
        {
            new Tuple<int, int>(-1, -1),
            new Tuple<int, int>(-1, 1),
            new Tuple<int, int>(1, -1),
            new Tuple<int, int>(1, 1),
        };

        public IEnumerable<MatrixPoint<T>> GetNeighbours(int row, int col)
        {
            var inMatrix = NeighboursDef.Where(t => row + t.Item1 >= 0 && row + t.Item1 < _matrix.GetLength(0) && col + t.Item2 >= 0 && col + t.Item2 < _matrix.GetLength(1)).ToList();
            return inMatrix.Select(t => new MatrixPoint<T>(row + t.Item1, col + t.Item2, _matrix[row + t.Item1, col +t.Item2]));
        }

        public IEnumerable<MatrixPoint<T>> GetCrossNeighbours(int row, int col)
        {
            var inMatrix = CrossNeighboursDef.Where(t => row + t.Item1 >= 0 && row + t.Item1 < _matrix.GetLength(0) && col + t.Item2 >= 0 && col + t.Item2 < _matrix.GetLength(1)).ToList();
            return inMatrix.Select(t => new MatrixPoint<T>(row + t.Item1, col + t.Item2, _matrix[row + t.Item1, col +t.Item2]));
        }

        public IEnumerable<MatrixPoint<T>> GetXNeighbours(int row, int col)
        {
            var inMatrix = XNeighboursDef.Where(t => row + t.Item1 >= 0 && row + t.Item1 < _matrix.GetLength(0) && col + t.Item2 >= 0 && col + t.Item2 < _matrix.GetLength(1)).ToList();
            return inMatrix.Select(t => new MatrixPoint<T>(row + t.Item1, col + t.Item2, _matrix[row + t.Item1, col +t.Item2]));
        }

        public IEnumerable<MatrixPoint<T>> GetInDirection(Tuple<int, int> dir, int row, int col, Func<T, bool> predicate)
        {
            var iRow = row + dir.Item1;
            var iCol = col + dir.Item2;
            while(iRow < Rows && iCol < Columns && iRow > -1 && iCol > -1)
            {
                var mxPoint = Get(iRow, iCol);
                yield return mxPoint;
                if(mxPoint.Value != null && predicate(mxPoint.Value))
                    break;
                iRow+=dir.Item1;
                iCol +=dir.Item2;
            }
        }
        public IEnumerable<MatrixPoint<T>> GetInDirection(Tuple<int, int> dir, int row, int col)
        {
            var iRow = row + dir.Item1;
            var iCol = col + dir.Item2;
            while(iRow < Rows && iCol < Columns && iRow > -1 && iCol > -1)
            {
                yield return Get(iRow, iCol);
                iRow+=dir.Item1;
                iCol +=dir.Item2;
            }
        }
    }
}