public class Before : IDay
{
    public string Output => _output ?? string.Empty;
    private string? _output;
    public int Star1()
    {
        _output = InputReader.GetInput("Before.txt");
        return -1;
    }

    public int Star2() => Star1();
}
