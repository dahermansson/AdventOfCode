public class Day22 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        var matrix = new Matrix<char>(InputReader.GetInputLines("Day22.txt"), false);

        Console.WriteLine(matrix.GetPrintable());

        var startPoint = matrix.FindFirst('.');
        

        
        return 1;
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }
}
