public class Day13 : IDay
{
    public string Output => throw new NotImplementedException();

    private string[] Input = InputReader.GetInputLines("13.txt");
    public int Star1()
    {
        var dots = Input.TakeWhile(t => t != string.Empty).Select(s => s.Split(",")).Select(s => new { row = int.Parse(s[1]), col = int.Parse(s[0]) });
        var paper = new Matrix<char>(dots.Max(t => t.row + 1), dots.Max(t => t.col + 1), false);
        foreach (var dot in dots)
            paper.Grid[dot.row, dot.col] = '#';
        var folds = Input.Reverse().TakeWhile(t => t != string.Empty).Reverse().ToArray();
        var fold = ParsFold(folds.First());
        if (fold.direction == "up")
            FoldUp(paper, fold.fold);
        else
            FoldLeft(paper, fold.fold);
        return paper.GetAllValues().Count(t => t == '#');
    }

    public int Star2()
    {
        var dots = Input.TakeWhile(t => t != string.Empty).Select(s => s.Split(",")).Select(s => new { row = int.Parse(s[1]), col = int.Parse(s[0]) });
        var paper = new Matrix<char>(dots.Max(t => t.row + 1), dots.Max(t => t.col + 1), false);
        foreach (var dot in dots)
            paper.Grid[dot.row, dot.col] = '#';
        var folds = Input.Reverse().TakeWhile(t => t != string.Empty).Reverse().ToArray();
        foreach (var fold in folds.Select(t => ParsFold(t)))
            if (fold.direction == "up")
                FoldUp(paper, fold.fold);
            else
                FoldLeft(paper, fold.fold);

        ImageHandler.CreatImageFromMatrix<char>(paper, "13.jpg", '#');
        return 1;
    }

    private (string direction, int fold) ParsFold(string s)
    {
        var foldDirection = s.Contains('x') ? "left" : "up";
        var fold = int.Parse(s.Split("=")[1]);
        return new(foldDirection, fold);
    }

    private void FoldUp(Matrix<char> matrix, int fold)
    {
        int newRow = fold - 1;
        for (int row = fold + 1; row < matrix.Rows; row++)
        {
            foreach (var dot in matrix.GetRowPositions(row).Where(t => t.Value == '#'))
            {
                matrix.Update(dot.Row, dot.Column, t => t = '\0');
                matrix.Update(newRow, dot.Column, t => t = '#');
            }
            newRow -= 1;
        }
    }

    private void FoldLeft(Matrix<char> matrix, int fold)
    {
        int newCol = fold - 1;
        for (int col = fold + 1; col < matrix.Columns; col++)
        {
            foreach (var dot in matrix.GetColumnPositions(col).Where(t => t.Value == '#'))
            {
                matrix.Update(dot.Row, dot.Column, t => t = '\0');
                matrix.Update(dot.Row, newCol, t => t = '#');
            }
            newCol -= 1;
        }
    }
}
