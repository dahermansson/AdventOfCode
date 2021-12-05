using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    //X ->
    //0 1 2 3
    //1 Y
    //2 |
    //3 V

    public class Matrix<T>
    {
        private T[,] _matrix;
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string[] Input { get; private set; }
        public bool SpaceSeparator { get; set; }
        public T[,] Grid { get {return _matrix;} }


        public Matrix(int width, int heigth, bool square = true)
        {
            Rows = square ? new int[]{ width , heigth}.Max() : heigth;
            Columns = square ? Rows : width;
            _matrix = new T[Columns, Rows];
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

        public IEnumerable<T> GetAll()
        {
            for (int row = 0; row < Rows; row++)
                for (int col = 0; col < Columns; col++)
                    yield return _matrix[row,col];
        }
        
        public IEnumerable<T> GetRow(int row)
        {
             for (int i = 0; i < Columns; i++)
                yield return _matrix[row, i];
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

    }
}