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
        pathFinding.Dijkstra(start, end, (a, b) =>
        {
            int aHeigth = a.Value;
            int bHeigth = b.Value;
            if (a.Value == 'S')
                aHeigth = 'a';
            if (b.Value == 'E')
                bHeigth = 'z';

            if (bHeigth <= aHeigth)
                return aHeigth;
            if (bHeigth - aHeigth == 1)
                return aHeigth + bHeigth;
            return 100000;
        });
        var path = pathFinding.GetShortestPath(start, end).ToArray();

        return path.Length;
    }

    public int Star2()
    {
        int lowest = int.MaxValue;
        var possibleStarts = new Matrix<char>(InputReader.GetInputLines("Day12.txt"), false).GetAllPositions().Where(t => t.Value == 'a').ToArray();
        foreach (var possibleStart in possibleStarts.Skip(1))
        {
        var matrix = new Matrix<char>(InputReader.GetInputLines("Day12.txt"), false);
        
        var end = matrix.GetAllPositions().Single(t => t.Value == 'E');
        matrix.Update(end.Row, end.Column, a => a = 'z');
        end = matrix.Get(end.Row, end.Column);
        
        
            var start = possibleStart;
            var pathFinding = new MatrixPathFinding<char>(matrix);
            pathFinding.Dijkstra(start, end, (a, b) => 1);

            var steps = pathFinding.GetShortestPath(start, end).ToArray().Count();
            if (steps > 10 && steps < lowest)
                lowest = steps;
        }
        return lowest;
    }
}
