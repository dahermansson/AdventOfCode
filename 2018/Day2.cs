public class Day2 : IDay
{
    public string Output => _output;

    private string[] Input = InputReader.GetInputLines("2.txt");
    private string _output = string.Empty;

    public int Star1()
    {
        int two = 0;
        int three = 0;
        foreach (var line in Input)
        {
            var count = line.GroupBy(t => t).ToDictionary(k => k.Key, t => t.Count());
            bool doneTwo = false;
            bool doneThree = false;
            foreach (var c in count.Where(t => t.Value == 2 || t.Value == 3))
            {
                if (!doneTwo && c.Value == 2)
                {
                    two++;
                    doneTwo = true;
                }
                if (!doneThree && c.Value == 3)
                {
                    three++;
                    doneThree = true;
                }
            }
        }
        return two * three;
    }

    public int Star2()
    {
        foreach (var line in Input)
        {
            foreach (var compline in Input)
            {
                char[] res = new char[line.Length];
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != compline[i])
                        res[i] = ' ';
                    else
                        res[i] = line[i];
                }
                var strRes = new string(res).Replace(" ", string.Empty);
                if (Math.Abs(strRes.Length - line.Length) == 1)
                {
                    _output = strRes;
                    return -1;
                }
            }

        }
        return -1;
    }

}
