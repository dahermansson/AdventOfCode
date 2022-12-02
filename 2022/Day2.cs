
public class Day2 : IDay
{
    public string Output => throw new NotImplementedException();
    public int Star1() => InputReader.GetInputLines("Day2.txt").Select(t => PointsFromRound(t[0] - (t[2] -23),(char)(t[2]-23))).Sum();
    private int PointsFromRound(int i, char you) => i switch
    {
         0 when you == 'A' => 4,
         0 when you == 'B' => 5,
         0 when you == 'C' => 6,
        -1 or 2 when you == 'A' => 7,
        -1 or 2 when you == 'B' => 8,
        -1 or 2 when you == 'C' => 9,
        _ when you == 'A' => 1,
        _ when you == 'B' => 2,
        _ when you == 'C' => 3,
        _ => 0
    };
    public int Star2() => InputReader.GetInputLines("Day2.txt").Select(t => PointsFromRound2(t[0], (t[2]))).Sum();

    private int PointsFromRound2(char o, char r) => r switch
    {
        'X' when o == 'A' => 3,
        'X' when o == 'B' => 1,
        'X' when o == 'C' => 2,
        'Y' when o == 'A' => 4,
        'Y' when o == 'B' => 5,
        'Y' when o == 'C' => 6,
        'Z' when o == 'A' => 8,
        'Z' when o == 'B' => 9,
        'Z' when o == 'C' => 7,
        _ => 0
    };
}