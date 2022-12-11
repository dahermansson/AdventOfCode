using System.Numerics;

public class Day11 : IDay
{
    public string Output => _ouput;
    private string _ouput = "";
    public int Star1()
    {
        List<Monkey> monkeys = InputReader.GetGroupsOfLines("Day11.txt", 7).Select(t => new Monkey(t.Value)).ToList();
        for (int i = 0; i < 20; i++)
            foreach (var monkey in monkeys)
                while (monkey.Items.TryDequeue(out var item))
                {
                    var newWorryLevel = monkey.Operation.Result(item) / 3;
                    monkeys[newWorryLevel % monkey.Test == 0 ? monkey.TrueMonkey : monkey.FalseMonkey].Items.Enqueue(newWorryLevel);
                    monkey.Inpections++;
                }
        return monkeys.OrderBy(m => m.Inpections).TakeLast(2).Select(t => t.Inpections).Aggregate((p, m) => p * m);
    }

    public int Star2()
    {
        List<Monkey> monkeys = InputReader.GetGroupsOfLines("Day11.txt", 7).Select(t => new Monkey(t.Value)).ToList();
        var modulo = monkeys.Select(m => m.Test).Aggregate((p, m) => p * m);
        for (int i = 0; i < 10000; i++)
            foreach (var monkey in monkeys)
                while (monkey.Items.TryDequeue(out var item))
                {
                    var newWorryLevel = monkey.Operation.Result(item) % modulo;
                    monkeys[newWorryLevel % monkey.Test == 0 ? monkey.TrueMonkey : monkey.FalseMonkey].Items.Enqueue(newWorryLevel);
                    monkey.Inpections++;
                }
        var res = monkeys.OrderBy(m => m.Inpections).TakeLast(2).Select(t => (BigInteger)t.Inpections).Aggregate((p, m) => p*m);
        _ouput = res.ToString();
        return -1;
    }

    private class Monkey
    {
        public Queue<BigInteger> Items { get; set; }
        public Operation Operation { get; set; }
        public int Test { get; set; }
        public int TrueMonkey { get; set; }
        public int FalseMonkey { get; set; }
        public int Inpections { get; set; }
        public Monkey(string[] input)
        {
            Items = new Queue<BigInteger>(input[1].Trim().Replace("Starting items: ", "").Split(", ").Select(BigInteger.Parse));
            Operation = new Operation(input[2].Trim());
            Test = int.Parse(input[3].Split(" ").Last());
            TrueMonkey = int.Parse(input[4].Split(" ").Last());
            FalseMonkey = int.Parse(input[5].Split(" ").Last());
        }
    }
    private class Operation
    {
        public string First { get; set; }
        public string Second { get; set; }
        public Arithmetic ArithmeticOp { get; set; }
        public Operation(string op)
        {
            var parts = op.Split(" ");
            First = parts[3];
            if (parts[4] == "+")
                ArithmeticOp = Arithmetic.Add;
            else
                ArithmeticOp = Arithmetic.Multiply;
            Second = parts[5];
        }
        public BigInteger Result(BigInteger old)
        {
            var first = First == "old" ? old : BigInteger.Parse(First);
            var second = Second == "old" ? old : BigInteger.Parse(Second);
            return ArithmeticOp switch
            {
                Arithmetic.Add => first + second,
                Arithmetic.Multiply => first * second,
                _ => 0
            };
        }

    }
    private enum Arithmetic
    {
        Add,
        Multiply
    }
}
