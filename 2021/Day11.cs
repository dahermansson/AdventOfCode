public class Day11 : IDay
{
    public string Output => throw new NotImplementedException();

    private Matrix<int> DumboOctopusys = new Matrix<int>(InputReader.GetInputLines("11.txt"), false);

    public int Star1()
    {
        int flashes = 0;
        for (int i = 0; i < 100; i++)
        {
            DumboOctopusys.UpdateAll(t => t += 1);
            flashes += Flash();
        }
        return flashes;
    }

    public int Flash()
    {
        HashSet<string> flashes = new HashSet<string>();
        var dumboOctopusys = DumboOctopusys.GetAllPositions().Where(t => t.Value > 9).ToList();
        for (int i = 0; i < dumboOctopusys.Count; i++)
        {
            if (!flashes.Contains(dumboOctopusys[i].PosToString))
            {
                flashes.Add(dumboOctopusys[i].PosToString);
                DumboOctopusys.Update(dumboOctopusys[i].Row, dumboOctopusys[i].Column, t => t = 0);
                foreach (var dumbo in DumboOctopusys.GetNeighbours(dumboOctopusys[i].Row, dumboOctopusys[i].Column).Where(t => t.Value != 0).ToList())
                {
                    DumboOctopusys.Update(dumbo.Row, dumbo.Column, t => t += 1);
                    if (DumboOctopusys.Get(dumbo.Row, dumbo.Column).Value > 9)
                        dumboOctopusys.Add(dumbo);
                }
            }
        }
        return flashes.Count;
    }

    public int Star2()
    {
        int steps = 0;
        var flashes = 0;
        while (flashes != 100)
        {
            DumboOctopusys.UpdateAll(t => t += 1);
            flashes = Flash();
            steps++;
        }
        return 100 + steps;
    }
}