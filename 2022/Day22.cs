public class Day22 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        var input = InputReader.GetInputLines("Day22.txt");
        int mapWidth = input.First().Length;
        var map = new Matrix<char>(input.SkipLast(2).Select(t => t.PadRight(mapWidth)).ToArray(), false);
        var instructions = input.TakeLast(1).Single().Replace("L", " L ").Replace("R", " R ").Split(" ").ToArray();

        var current = map.FindFirst('.');
        var currentDirection = MatrixDirection.Rigth;

        foreach (var instruction in instructions)
            if (int.TryParse(instruction, out int steps))
                current = map.GetStepsInDirectionWrap(currentDirection, current, steps, t => t == '#', t => t != ' ').LastOrDefault(t => t.Value == '.') ?? current;
            else
                currentDirection = GetNewDirection(instruction, currentDirection);

        return 1000 * (current.Row + 1) + 4 * (current.Column + 1) + FacingScore(currentDirection);
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }

    private int FacingScore(MatrixDirection m) => m switch
    {
        MatrixDirection.Rigth => 0,
        MatrixDirection.Down => 1,
        MatrixDirection.Left => 2,
        _ => 3
    };

    private MatrixDirection GetNewDirection(string direction, MatrixDirection current) => direction switch
    {
        "R" when current == MatrixDirection.Up => MatrixDirection.Rigth,
        "R" when current == MatrixDirection.Rigth => MatrixDirection.Down,
        "R" when current == MatrixDirection.Down => MatrixDirection.Left,
        "R" when current == MatrixDirection.Left => MatrixDirection.Up,
        "L" when current == MatrixDirection.Up => MatrixDirection.Left,
        "L" when current == MatrixDirection.Rigth => MatrixDirection.Up,
        "L" when current == MatrixDirection.Down => MatrixDirection.Rigth,
        "L" when current == MatrixDirection.Left => MatrixDirection.Down,
        _ => MatrixDirection.Rigth
    };
}
