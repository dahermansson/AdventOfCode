public class Day5 : IDay
{
    public string Output => _output;
    private string _output;
    Dictionary<int, Stack<string>> Stacks;
    public Day5()
    {
        Stacks = InitStacks();
        _output = string.Empty;
    }

    //Don't do this!!! Read input from file :(
    private Dictionary<int, Stack<string>> InitStacks() =>
        new Dictionary<int, Stack<string>>{
            {1, new Stack<string>(new string[]{"W", "D", "G", "B", "H", "R", "V"})},
            {2, new Stack<string>(new string[]{"J", "N", "G", "C", "R", "F"})},
            {3, new Stack<string>(new string[]{"L", "S", "F","H","D","N","J"})},
            {4, new Stack<string>(new string[]{"J", "D", "S", "V"})},
            {5, new Stack<string>(new string[]{"S","H","D","R","Q","W","N","V"})},
            {6, new Stack<string>(new string[]{"P", "G", "H", "C", "M"})},
            {7, new Stack<string>(new string[]{"F", "J", "B", "G", "L", "Z","H", "C"})},
            {8, new Stack<string>(new string[]{"S", "J", "R"})},
            {9, new Stack<string>(new string[]{"L", "G", "S", "R", "B", "N","V", "M"})}
        };

    public int Star1()
    {
        foreach (var instruction in InputReader.GetInputLines("Day5.txt").Skip(10).Select(t => (Move: int.Parse(t.Split(" ")[1]), Fr: int.Parse(t.Split(" ")[3]), To: int.Parse(t.Split(" ")[5]))))
            for (int i = 0; i < instruction.Move; i++)
                Stacks[instruction.To].Push(Stacks[instruction.Fr].Pop());
        _output = string.Join("", Stacks.Select(t => t.Value.Peek()));
        return -1;
    }

    public int Star2()
    {
        Stacks = InitStacks();
        foreach (var instruction in InputReader.GetInputLines("Day5.txt").Skip(10).Select(t => (Move: int.Parse(t.Split(" ")[1]), Fr: int.Parse(t.Split(" ")[3]), To: int.Parse(t.Split(" ")[5]))))
        {
            var reverseChunkStack = new Stack<string>();
            for (int i = 0; i < instruction.Move; i++)
                reverseChunkStack.Push(Stacks[instruction.Fr].Pop());
            for (int i = 0; i < instruction.Move; i++)
                Stacks[instruction.To].Push(reverseChunkStack.Pop());
        }
        _output = string.Join("", Stacks.Select(t => t.Value.Peek()));
        return -1;
    }
}