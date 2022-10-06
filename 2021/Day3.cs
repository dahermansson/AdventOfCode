using System.Collections;

public class Day3 : IDay
{
    public string Output => throw new NotImplementedException();
    private string[] Input = InputReader.GetInputLines("3.txt");
    public int Star1()
    {
        int nBits = Input[0].Length;
        var bits = new BitArray(nBits);
        for (int i = 0; i < nBits; i++)
            bits[i] = GetColumn(Input, i).Count(t => t == '1') > Input.Length / 2;
        return bits.ToIntRev() * bits.Not().ToIntRev();
    }

    public int Star2()
    {
        var numbers = Input;
        var index = 0;
        while (numbers.Length > 1)
            numbers = GetMostInPosition(numbers, index++, '1');
        var oxygen = numbers[0].ToBitArray().ToInt();

        numbers = Input;
        index = 0;
        while (numbers.Length > 1)
            numbers = GetFewestInPosition(numbers, index++, '0');

        return oxygen * numbers[0].ToBitArray().ToInt();
    }

    private string[] GetMostInPosition(string[] source, int index, char value)
    {
        var keepValue = GetColumn(source, index).Count(t => t == value) >= GetColumn(source, index).Count(t => t != value);
        if (keepValue)
            return source.Where(t => t[index] == value).ToArray();
        else
            return source.Where(t => t[index] != value).ToArray();
    }

    private string[] GetFewestInPosition(string[] source, int index, char value)
    {
        var keepValue = GetColumn(source, index).Count(t => t == value) <= GetColumn(source, index).Count(t => t != value);
        if (keepValue)
            return source.Where(t => t[index] == value).ToArray();
        else
            return source.Where(t => t[index] != value).ToArray();
    }

    private IEnumerable<char> GetColumn(string[] source, int index)
    {
        foreach (var row in source)
            yield return row[index];
    }
}