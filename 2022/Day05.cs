public class Day5 : IDay
{
    public string Output => _output;
    private string _output = string.Empty;
    Dictionary<int, Stack<char>> Stacks;
    private const int STACKS = 9;

    public Day5()
    {
        Stacks = InitStacks();
    }
    
    private Dictionary<int, Stack<char>> InitStacks()
    {
        var stacks = new Dictionary<int, Stack<char>>();
        for (int i = 1; i <= STACKS; i++)
            stacks.Add(i, new Stack<char>());

        var stackLines = InputReader.GetInputLines("Day5.txt").Take(8).Reverse();
        foreach (var line in stackLines)
        {
            var readIndex = 0;
            for (int i = 1; i <= STACKS; i++)
            {
                var value = line.Substring(readIndex, i == STACKS ? 3 : 4)[1];
                if(char.IsLetter(value))
                    stacks[i].Push(value);
                readIndex+=4;
            }
        }
        return stacks;
    }

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
            var reverseChunkStack = new Stack<char>();
            for (int i = 0; i < instruction.Move; i++)
                reverseChunkStack.Push(Stacks[instruction.Fr].Pop());
            for (int i = 0; i < instruction.Move; i++)
                Stacks[instruction.To].Push(reverseChunkStack.Pop());
        }
        _output = string.Join("", Stacks.Select(t => t.Value.Peek()));
        return -1;
    }
}