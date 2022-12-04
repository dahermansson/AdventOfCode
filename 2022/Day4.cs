public class Day4 : IDay
{
    public string Output => throw new NotImplementedException();

    public int Star1() => InputReader.GetInputLines("Day4.txt").Select(s => new Pair(s)).Count(p => p.HasRangeOverlapp());
    public int Star2() => InputReader.GetInputLines("Day4.txt").Select(s => new Pair(s)).Count(p => p.HasAnyOverlapp());

    private record Pair
    {
        public Pair(string s)
        {
            var splits = s.Split(',');
            var StartElf1 = int.Parse(splits[0].Split('-')[0]);
            var EndElf1 = int.Parse(splits[0].Split('-')[1]);
            var StartElf2 = int.Parse(splits[1].Split('-')[0]);
            var EndElf2 = int.Parse(splits[1].Split('-')[1]);
            Elf1 = Enumerable.Range(StartElf1, EndElf1 - StartElf1 + 1);
            Elf2 = Enumerable.Range(StartElf2, EndElf2 - StartElf2 + 1);
        }
        public IEnumerable<int> Elf1 { get; set; }
        public IEnumerable<int> Elf2 { get; set; }

        public bool Elf1ContainsElf2() => Elf1.All(f => Elf2.Contains(f));
        public bool Elf2ContainsElf1() => Elf2.All(s => Elf1.Contains(s));

        public bool AnyElf1InElf2() => Elf1.Any(f => Elf2.Contains(f));
        public bool EnyElf2InElf1() => Elf2.Any(s => Elf1.Contains(s));

        public bool HasRangeOverlapp() => Elf1ContainsElf2() || Elf2ContainsElf1();
        public bool HasAnyOverlapp() => AnyElf1InElf2() || EnyElf2InElf1();
    }
}



