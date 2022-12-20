public class Day20 : IDay
{
    public string Output => _output;
    private string _output = "";

    public int Star1()
    {
        LinkedList<(int Index, int Value)> items = new();
        var input = InputReader.GetInputLines<int>("Day20.txt");
        for (var i = 0; i < input.Length; i++)
            items.AddLast((i, input[i]));
        var moved = new HashSet<int>();

        while (moved.Count < items.Count)
        {
            var move = items.Find(items.First(t => !moved.Contains(t.Index)));
            moved.Add(move.Value.Index);
            if (move.Value.Value > 0)
            {
                var after = move.Next ?? items.First;
                for (int i = 0; i < move.Value.Value; i++)
                {
                    after = after.Next ?? items.First;
                    if (move.Value.Index == after.Value.Index)
                        after = after.Next ?? items.First;
                }

                if (move.Value.Value == 0)
                    continue;
                items.Remove(move);
                items.AddBefore(after, move);
            }
            if (move.Value.Value < 0)
            {
                var previus = move.Previous ?? items.Last;
                for (int i = 0; i < Math.Abs(move.Value.Value); i++)
                {
                    previus = previus.Previous ?? items.Last;

                    if (move.Value.Index == previus.Value.Index)
                        previus = previus.Previous ?? items.Last;
                }

                if (move.Value.Value == 0)
                    continue;
                items.Remove(move);
                items.AddAfter(previus, move);
            }
        }

        var list = items.ToList();
        var countzeros = list.Where(t => t.Value == 0).ToArray();
        var zeroNumber = list.IndexOf(list.First(t => t.Value == 0));

        var _1000 = (1000 + zeroNumber) % list.Count;
        var _2000 = (2000 + zeroNumber) % list.Count;
        var _3000 = (3000 + zeroNumber) % list.Count;

        return new[] { list[_1000].Value, list[_2000].Value, list[_3000].Value }.Sum();
    }

    public int Star2()
    {
        Int64 decrpytionkey = 811589153;
        LinkedList<(int Index, Int64 Value)> items = new();
        var input = InputReader.GetInputLines<int>("Day20.txt");
        //var input = new []{1, 2, -3, 3, -2, 0, 4};
        for (var i = 0; i < input.Length; i++)
            items.AddLast((i, input[i] * decrpytionkey));


        for (int loop = 0; loop < 10; loop++)
        {
            for (int indexToMove = 0; indexToMove < items.Count; indexToMove++)
            {
                var move = items.Find(items.First(t => t.Index == indexToMove));
                if (move.Value.Value > 0)
                {
                    var after = move.Next ?? items.First;
                    for (int i = 0; i < move.Value.Value % (items.Count - 1); i++)
                    {
                        after = after.Next ?? items.First;

                        if (move.Value.Index == after.Value.Index)
                            after = after.Next ?? items.First;
                    }


                    if (move.Value.Value == 0)
                        continue;
                    items.Remove(move);
                    items.AddBefore(after, move);
                }
                if (move.Value.Value < 0)
                {
                    var previus = move.Previous ?? items.Last;
                    for (int i = 0; i < Math.Abs(move.Value.Value) % (items.Count - 1); i++)
                    {
                        previus = previus.Previous ?? items.Last;

                        if (move.Value.Index == previus.Value.Index)
                            previus = previus.Previous ?? items.Last;
                    }

                    if (move.Value.Value == 0)
                        continue;
                    items.Remove(move);
                    items.AddAfter(previus, move);
                }
            }
        }

        var list = items.ToList();
        var countzeros = list.Where(t => t.Value == 0).ToArray();
        var zeroNumber = list.IndexOf(list.First(t => t.Value == 0));

        var _1000 = (1000 + zeroNumber) % list.Count;
        var _2000 = (2000 + zeroNumber) % list.Count;
        var _3000 = (3000 + zeroNumber) % list.Count;

        _output = new[] { list[_1000].Value, list[_2000].Value, list[_3000].Value }.Sum().ToString();
        return -1;
    }
}
