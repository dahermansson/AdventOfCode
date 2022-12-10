public class Day10 : IDay
{
    public string Output => _output;
    private string _output = "";

    public int Star1()
    {
        ClockCircuitCPU cpu = new ClockCircuitCPU();
        cpu.Run(InputReader.GetInputStreamReader("Day10.txt"));
        return cpu.XValues.Select((x, i) => x * cpu.XCycles[i]).Sum();
    }

    public int Star2()
    {
        ClockCircuitCPU cpu = new ClockCircuitCPU();
        cpu.Run(InputReader.GetInputStreamReader("Day10.txt"));
        _output = cpu.CRT.GetPrintable();
        return -1;
    }
}
