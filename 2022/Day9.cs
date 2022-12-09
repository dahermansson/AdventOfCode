public class Day9 : IDay
{
    public string Output => throw new NotImplementedException();
    public int Star1() => ModelRope(2);
    public int Star2() => ModelRope(10);
    public int ModelRope(int length)
    {
        var tailPositions = new List<(int row, int col)>();
        var knots = new MatrixPoint<int>[length];
        for (int i = 0; i < length; i++)
            knots[i] = new MatrixPoint<int>(0, 0, i + 1);
        var moves = InputReader.GetInputLines("Day9.txt");
        foreach (var move in moves)
        {
            var dir = MatrixDirectionValues.LetterToDirection(move.Split(" ")[0][0]);
            var steps = int.Parse(move.Split(" ")[1]);
            for (int i = 0; i < steps; i++)
            {
                knots[0].Move(dir);
                for (int k = 1; k < knots.Length; k++)
                    knots[k].Move(GetMatrixDirectionValuesForTail(knots[k - 1], knots[k]));
                tailPositions.Add(new(knots.Last().Row, knots.Last().Column));
            }
        }
        return tailPositions.Distinct().Count();
    }

    private MatrixDirection GetMatrixDirectionValuesForTail(MatrixPoint<int> head, MatrixPoint<int> tail)
    {
        if (Math.Abs(head.Row - tail.Row) < 2 && Math.Abs(head.Column - tail.Column) < 2)
            return MatrixDirection.None;
        if (head.Row == tail.Row)
        {
            if (head.Column > tail.Column)
                return MatrixDirection.Rigth;
            return MatrixDirection.Left;
        }
        if (head.Column == tail.Column)
        {
            if (head.Row < tail.Row)
                return MatrixDirection.Up;
            return MatrixDirection.Down;
        }

        if (head.Row < tail.Row && head.Column > tail.Column)
            return MatrixDirection.UpRigth;
        if (head.Row < tail.Row && head.Column < tail.Column)
            return MatrixDirection.UpLeft;

        if (head.Row > tail.Row && head.Column > tail.Column)
            return MatrixDirection.DownRigth;
        if (head.Row > tail.Row && head.Column < tail.Column)
            return MatrixDirection.DownLeft;
        return MatrixDirection.None;
    }
}
