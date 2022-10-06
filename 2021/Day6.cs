public class Day6 : IDay
{
    public string Output => res.ToString();
    private List<int> Input = InputReader.GetIntArrayFromSingleLine("6.txt").ToList();
    private long res;
    public int Star1() => Simulate(80);
    public int Star2() => Simulate(256);
    private int Simulate(int days)
    {
        long[] lanternsLUT = new long[9];
        Input.ForEach(t => { lanternsLUT[t]++; });
        for (int day = 0; day < days; day++)
        {
            var spawning = lanternsLUT[0];
            for (int i = 1; i < lanternsLUT.Length; i++)
                lanternsLUT[i - 1] = lanternsLUT[i];
            lanternsLUT[6] += spawning;
            lanternsLUT[8] = spawning;
        }
        res = lanternsLUT.Sum();
        return -1;
    }
}
