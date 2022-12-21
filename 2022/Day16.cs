public class Day16 : IDay
{
    
    public string Output => throw new NotImplementedException();

    public int Star1()
    {
        var valves = InputReader.GetInputLines("Day16.txt").Select(t => new Valve(t.Split(";")[0])).ToList();
        var nextValves = InputReader.GetInputLines("Day16.txt").Select(t => t.Split(";")[1]).Select(t => t.Split("valve")[1].TrimStart('s', ' ').RemoveWhiteSpaces());
        var valvesAndNeigbours = new Dictionary<string, (Valve Valve, List<Valve> neighbours)>();
        foreach(var a in Enumerable.Zip(valves, nextValves))
            valvesAndNeigbours.Add(a.First.Id, (a.First, new List<Valve>(a.Second.Split(",").Where(t => valves.Any(v => v.Id == t)).Select(v => valves.Single(p => p.Id == v)).ToArray())));
        
    
        var current = valvesAndNeigbours["AA"].Valve;

        return MaxFlow(current, "", 30, valvesAndNeigbours, "");
    }

    private static Dictionary<(string current, string opend, int timeLeft), int> FlowCache = new Dictionary<(string current, string opend, int timeLeft), int>();

    private int MaxFlow(Valve current, string opend, int timeLeft, Dictionary<string, (Valve valve, List<Valve> Neighbours)> valves, string previus)
    {

        if(FlowCache.ContainsKey((current.Id, opend, timeLeft)))
            return FlowCache[(current.Id, opend, timeLeft)];
        if(timeLeft <= 0)
            return 0;
        int best = 0;
        if(!opend.Contains(current.OpendId))
        {
            int val = current.Flowrate * (timeLeft - 1);
            var currentOpend = opend + current.OpendId;

            foreach (var neighbour in valves[current.Id].Neighbours.OrderByDescending(t => t.Flowrate))
            {
                if(current.Flowrate > 0)
                {
                    best =  new int[]{best, (val + MaxFlow(neighbour, currentOpend, timeLeft - 2, valves, current.Id))}.Max();
                }
                best = new int[]{best, MaxFlow(neighbour, opend, timeLeft -1, valves, current.Id)}.Max();
            }
            FlowCache[(current.Id, opend, timeLeft)] = best;
        }
        return best;
    }


    private record Valve
    {
        public Valve(string s)
        {
            Id = s.Split(" ")[1];
            Flowrate = Utils.ExtraxtInteger(s);
        }

        public string Id { get; set; }
        public string OpendId => Id + ",";
        public int Flowrate { get; set; }
    }

    public int Star2()
    {
        throw new NotImplementedException();
    }
}