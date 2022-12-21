public class Day21 : IDay
{
    public string Output => _output;
    private Dictionary<string, string> Monkeys = InputReader.GetInputLines("Day21.txt").ToDictionary(key => key.Split(":")[0], value => value.Split(":")[1].Trim());
    private string _output = "";
    public int Star1()
    {
        _output = GetNumber("root").ToString();
        return -1;
    }

    public int Star2()
    {
        long humn = 0;
        bool eq = false;
        long increment = 10000000000;
        bool previusPositivDiff = false;
        while (!eq)
        {
            humn += increment;
            Monkeys["humn"] = humn.ToString();
            var res = MonkeysEqualNumbers("root");
            eq = res.equal;
            if (res.diff < 0 && !previusPositivDiff)
            {
                increment = increment / 10;
                increment *= -1;
                previusPositivDiff = true;
            }
            if (res.diff > 0 && previusPositivDiff)
            {
                increment = increment / 10;
                increment *= -1;
                previusPositivDiff = false;
            }
        }

        _output = humn.ToString();
        return -1;
    }

    private (bool equal, long diff) MonkeysEqualNumbers(string monkey)
    {
        var splits = Monkeys[monkey].Split(" ");
        var first = GetNumber(splits[0]);
        var second = GetNumber(splits[2]);
        return (first == second, first - second);
    }

    private long GetNumber(string monkey) => long.TryParse(Monkeys[monkey], out long yellNumber) ? yellNumber : Monkeys[monkey].Split(" ")[1] switch 
    {
        "+" => GetNumber(Monkeys[monkey].Split(" ")[0]) + GetNumber(Monkeys[monkey].Split(" ")[2]),
        "-" => GetNumber(Monkeys[monkey].Split(" ")[0]) - GetNumber(Monkeys[monkey].Split(" ")[2]),
        "*" => GetNumber(Monkeys[monkey].Split(" ")[0]) * GetNumber(Monkeys[monkey].Split(" ")[2]),
        "/" => GetNumber(Monkeys[monkey].Split(" ")[0]) / GetNumber(Monkeys[monkey].Split(" ")[2]),
        _ => 0
    };
}
