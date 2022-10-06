public class Day7 : IDay
{
    public string Output => throw new NotImplementedException();
    private int[] Input = InputReader.GetIntArrayFromSingleLine("7.txt");
    public int Star1()
    {
        int[] posIndex = new int[Input.Max() + 1];
        for (int position = 0; position < posIndex.Length; position++)
        {
            var totalFuel = 0;
            foreach (var crab in Input)
                totalFuel += Math.Abs(crab - position);
            posIndex[position] = totalFuel;
        }
        return posIndex.Min();
    }

    public int Star2()
    {
        int[] posIndex = new int[Input.Max() + 1];
        for (int position = 0; position < posIndex.Length; position++)
        {
            var totalFuel = 0;
            foreach (var crab in Input)
                totalFuel += Enumerable.Range(1, Math.Abs(crab - position)).ToArray().Sum();
            posIndex[position] = totalFuel;
        }
        return posIndex.Min();
    }
}