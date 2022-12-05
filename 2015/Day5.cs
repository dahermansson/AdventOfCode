public class Day5 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1() => InputReader.GetInputLines("Day5.txt").Count(t => IsValid(t));

    private bool IsValid(string s) => s.Count(t => "aeiou".Contains(t)) > 2 && HasDoubleLetter(s) && !Containsabcdpqxy(s);
    

    private bool HasDoubleLetter(string s)
    {
        for (int i = 0; i < s.Length-1; i++)
            if(s[i] == s[i+1])
                return true;
        return false;
    }

    private bool Containsabcdpqxy(string s) => new []{"ab", "cd", "pq", "xy"}.Any(t => s.Contains(t));
    

    public int Star2()
    {
        throw new NotImplementedException();
    }
}
