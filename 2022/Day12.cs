public class Day12 : IDay
{
    public string Output => throw new NotImplementedException();


    public int Star1()
    {
        var matrix = new Matrix<char>(InputReader.GetInputLines("Day12.txt"), false);

        var start = matrix.GetAllPositions().Single(t => t.Value == 'S');
        var end = matrix.GetAllPositions().Single(t => t.Value == 'E');
        matrix.Update(start.Row, start.Column, a => a = 'a');
        matrix.Update(end.Row, end.Column, a => a = 'z');
        start = matrix.Get(start.Row, start.Column);
        end = matrix.Get(end.Row, end.Column);

        var pathFinding = new MatrixPathFinding<char>(matrix);
        pathFinding.Dijkstra(start, (currentCost, node) => currentCost + node.Value);
        var path = pathFinding.GetShortestPath(start, end).ToArray();

        return path.Length;
    }

    public int Star2()
    {
        int lowest = int.MaxValue;
        var matrix = new Matrix<char>(InputReader.GetInputLines("Day12.txt"), false);
        var end = matrix.GetAllPositions().Single(t => t.Value == 'E');
        matrix.Update(end.Row, end.Column, a => a = 'z');
        end = matrix.Get(end.Row, end.Column);
        var possibleStarts = matrix.GetAllPositions().Where(t => t.Value == 'a' && matrix.GetCrossNeighbours(t).Any(n => n.Value <= t.Value+1)).ToArray();
        foreach (var possibleStart in possibleStarts)
        {
            var pathFinding = new MatrixPathFinding<char>(matrix);
            pathFinding.Dijkstra(possibleStart, (currentCost, node) => currentCost + node.Value);

            var steps = pathFinding.GetShortestPath(possibleStart, end).Count();
            if (steps != 0 && steps < lowest)
                lowest = steps;
        }
        return lowest;
    }
}
