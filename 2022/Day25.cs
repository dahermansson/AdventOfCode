public class Day25 : IDay
{
    public string Output => _output;
    private string _output = "";
    private string BaseSNAFU = "012-=";

    public int Star1()
    {
        _output = IntToSNAFU(InputReader.GetInputLines("Day25.txt").Select(t => SNAFUToInt(t)).Sum());
        return -1;
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }

    private long SNAFUToInt(string snafu)
    {
        int pos = snafu.Length - 1;
        int n = BaseSNAFU.Length;
        long result = 0;
        for (int i = 0; i < snafu.Length; i++)
        {
            var x = BaseSNAFU.IndexOf(snafu[i]);
            if (x == 3)
                result += (-1 * (long)Math.Pow(n, pos));
            else if (x == 4)
                result += (-2 * (long)Math.Pow(n, pos));
            else
                result += x * (long)Math.Pow(n, pos);
            pos -= 1;
        }
        return result;
    }

    private string IntToSNAFU(long value)
    {
        var buffer = new List<char>();
        int reminder = 0;
        do
        {
            long val = (value % BaseSNAFU.Length) + reminder;
            if (val == 3)
            {
                reminder = 1;
                buffer.Insert(0, '=');
            }
            else if (val == 4)
            {
                reminder = 1;
                buffer.Insert(0, '-');
            }
            else if( val == 5)
            {
                reminder = 1;
                buffer.Insert(0, '0');
            }
            else
            {
                buffer.Insert(0, BaseSNAFU[(int)val]);
                reminder = 0;
            }
            
            value = value / BaseSNAFU.Length;
        } while (value > 0 || reminder != 0);
        return string.Join("", buffer);
    }
}
