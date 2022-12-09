public class Day8 : IDay
{
    public string Output => throw new NotImplementedException();
    private Matrix<int> trees = new Matrix<int>(InputReader.GetInputLines("Day8.txt"), false);
    public int Star1() => trees.GetAllPositions().Count(tree => IsVisible(tree));
    public int Star2() => trees.GetAllPositions().Select(tree => GetScenicScore(tree)).Max();
    
    private bool IsVisible(MatrixPoint<int> tree)
    {
        if(tree.Column == 0 || tree.Column == trees.Columns-1 || tree.Row == 0 || tree.Row == trees.Rows-1)
            return true;
        if(trees.GetColumn(tree.Column).ToArray()[..tree.Row].All(t => t < tree.Value))
            return true;
        if(trees.GetColumn(tree.Column).ToArray()[(tree.Row+1)..].All(t => t < tree.Value))
            return true;
        if(trees.GetRow(tree.Row).ToArray()[..tree.Column].All(t => t < tree.Value))
            return true;
        if(trees.GetRow(tree.Row).ToArray()[(tree.Column+1)..].All(t => t < tree.Value))
            return true;
        return false;
    }

    private int GetVisableTrees(IEnumerable<MatrixPoint<int>> treesInLine, MatrixPoint<int> tree)
    {
        int count = 0;
        foreach (var t in treesInLine)
            if(t.Value < tree.Value)
                count++;
            else            
                return ++count;
        return count;
    }

    private int GetScenicScore(MatrixPoint<int> tree) => GetVisableTrees(trees.GetInDirection(MatrixDirectionValues.Up, tree.Row, tree.Column), tree) * 
        GetVisableTrees(trees.GetInDirection(MatrixDirectionValues.Down, tree.Row, tree.Column), tree) *
        GetVisableTrees(trees.GetInDirection(MatrixDirectionValues.Left, tree.Row, tree.Column), tree) *
        GetVisableTrees(trees.GetInDirection(MatrixDirectionValues.Rigth, tree.Row, tree.Column), tree);
}
