using System.Security.Cryptography;
public class Day4 : IDay
{
    public string Output => throw new NotImplementedException();
    public int Star1() => FindHash(5);
    public int Star2() => FindHash(6);
    private int FindHash(int zeros)
    {
        var md5 = MD5.Create();
        int number = 0;
        string hash;
        do
        {
            var inputBytes = System.Text.Encoding.ASCII.GetBytes($"yzbqklnj{number++}");
            var hashBytes = md5.ComputeHash(inputBytes);
            hash = Convert.ToHexString(hashBytes);
        } while( hash.Take(zeros).Any(t => t != '0'));
        return number-1;
    }
}
