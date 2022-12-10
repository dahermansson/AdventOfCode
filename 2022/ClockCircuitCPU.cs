public class ClockCircuitCPU
{
    public int X { get; set; } = 1;
    private Instruction? cycle;
    private void UpdateX(int x)
    {
        X = x;
    }

    public List<int> XValues = new List<int>();
    public List<int> XCycles = new List<int>() { 20, 60, 100, 140, 180, 220 };
    public Matrix<char> CRT = new Matrix<char>(6, 40, false);
    private int DrawPixelPosition(int cycle) => (cycle - 1) % 40;

    private int DrawPixelRow(int cycle)
    {
        if (cycle > 1 && cycle % 40 == 0)
            return (cycle - 1) / 40;
        else
            return cycle / 40;
    }
    private void PutPixel(int cycle)
    {
        if (Math.Abs(DrawPixelPosition(cycle) - X) < 2)
            CRT.Update(DrawPixelRow(cycle), DrawPixelPosition(cycle), t => t = '#');
        else
            CRT.Update(DrawPixelRow(cycle), DrawPixelPosition(cycle), t => t = '.');
    }

    public void Run(StreamReader sr)
    {
        string? instruction;
        int cycles = 1;
        while ((instruction = sr.ReadLine()) != null)
        {
            PutPixel(cycles);
            cycle = CreateInstruction(instruction);
            cycle.DoFirst(instruction);
            if (XCycles.Contains(cycles))
                XValues.Add(X);
            cycles++;
            if (cycle is ITwoCycles)
            {
                PutPixel(cycles);
                if (XCycles.Contains(cycles))
                    XValues.Add(X);
                ((ITwoCycles)cycle).DoSecond(X, UpdateX);
                cycles++;
            }
        }
    }

    private Instruction CreateInstruction(string instruction)
    {
        if (instruction.StartsWith("addx"))
            return new AddX();
        return new Noop();
    }
}

public class Noop : Instruction
{
    public void DoFirst(string instruction) { }
}

public class AddX : Instruction, ITwoCycles
{
    private string Instruction = string.Empty;
    public void DoFirst(string instruction)
    {
        Instruction = instruction;
    }

    public void DoSecond(int x, Action<int> callback)
    {
        var i = int.Parse(Instruction.Split(" ")[1]);
        callback(x + i);
    }
}

public interface ITwoCycles
{
    void DoSecond(int x, Action<int> callback);
}

public interface Instruction
{
    void DoFirst(string i);
}