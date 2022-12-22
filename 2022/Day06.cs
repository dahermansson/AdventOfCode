public class Day6 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1() => FindMarker(InputReader.GetInput("Day6.txt"), 4);

    public int Star2() => FindMarker(InputReader.GetInput("Day6.txt"), 14);

    public int FindMarker(string input, int markerLength)
    {
        for (int i = markerLength-1; i < input.Length; i++)
            if (input[(i - markerLength+1)..(i+1)].Distinct().Count() == markerLength)
                return i+1;
        return 0;
    }

}
