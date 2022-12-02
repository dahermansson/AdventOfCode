
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
    public int Star2()
    {
        throw new NotImplementedException();
    }
}