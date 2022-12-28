using System.Collections;

public class Day17 : IDay
{
    public static readonly int GAMEWIDTH = 7; //Zerobased game 
    public static SortedList<int, BitArray> RestingRocks = new SortedList<int, BitArray>()
    {
        {0, new BitArray(GAMEWIDTH, true)}
    };
    public string Output => _output;
    private string _output = "";
    //private string Moves = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
    private string Moves = InputReader.GetInput("Day17.txt");
    private int MoveXIndex = 0;
    private int ShapeIndex = 0;
    private int StackHeigth => RestingRocks.Keys.Max();
    private int GetNextShapeIndex() => ShapeIndex++ % Shapes.Length;
    private int GetNextXMove() => Moves[MoveXIndex++ % Moves.Length] == '>' ? 1 : -1;
    //Y 4
    //  3
    //  2
    //  1
    //Y 0 1 2 3 4 5
    //X ->
    private static Shape[] Shapes = new Shape[]{
        new Shape(new Point[]{
            new Point(0, 0), new Point(1, 0), new Point(2, 0), new Point(3, 0)
        }),
        new Shape(new Point[]{
                new Point(1, 2),
            new Point(0, 1), new Point(1,1), new Point(2,1),
                new Point(1, 0)
        }),
        new Shape(new Point[]{
                                      new Point(2, 2),
                                      new Point(2, 1),
    new Point(0, 0), new Point(1, 0), new Point(2, 0),
        }),
        new Shape(new Point[]{
            new Point(0, 3),
            new Point(0, 2),
            new Point(0, 1),
            new Point(0, 0),
        }),
        new Shape(new Point[]{
            new Point(0,1), new Point(1,1),
            new Point(0,0), new Point(1,0)
        })
    };

    public void PrintGameBoard(Shape? shape = null)
    {
        var temp = new Dictionary<int, BitArray>();

        if (shape != null)
        {
            foreach (var b in shape.Block.OrderByDescending(t => t.Y))
            {
                if (!temp.ContainsKey(b.Y))
                    temp[b.Y] = new BitArray(GAMEWIDTH, false);
                temp[b.Y][b.X] = true;
            }
            for (int i = temp.Keys.Max(); i >= temp.Keys.Min(); i--)
            {
                foreach (bool b in temp[i])
                    Console.Write(b ? "@" : ".");
                Console.WriteLine();
            }

            if (temp.Keys.Min() > RestingRocks.Keys.Max())
            {
                Enumerable.Range(0, temp.Keys.Min() - 1 - RestingRocks.Keys.Max()).ToList().ForEach(t => Console.WriteLine(new string('.', 7)));
            }
        }
        for (int i = RestingRocks.Count - 1; i > 0; i--)
        {
            foreach (bool b in RestingRocks[i])
                Console.Write(b ? "#" : ".");
            Console.WriteLine();
        }
        Console.WriteLine(new string('-', 7));
        Console.WriteLine();
    }


    public int DropRocks(int numbersOfRockToDropp)
    {
        var blocksToFall = numbersOfRockToDropp;
        for (int blocks = 0; blocks < blocksToFall; blocks++)
        {
            var shape = Shapes[GetNextShapeIndex()].Spawn(StackHeigth);
            do
            {
                var nextMove = GetNextXMove();
                int moveX = shape.MoveX(nextMove);
                if (moveX == -1)
                    break;
            } while (shape.MoveY());

            foreach (var b in shape.Block)
            {
                if (!RestingRocks.ContainsKey(b.Y))
                    RestingRocks[b.Y] = new BitArray(GAMEWIDTH, false);
                RestingRocks[b.Y][b.X] = true;
            }
            //PrintGameBoard();
        }
        return StackHeigth;
    }


    public int Star1()
    {
        return DropRocks(2022);
    }

    public int Star2()
    {
        var cycle = FindCycle(); //Done two cycles
        RestingRocks.Clear();
        RestingRocks.Add(0, new BitArray(GAMEWIDTH, true));
        ShapeIndex = 0;
        MoveXIndex = 0;
        long cyclesToDo = (1000000000000 - cycle.blocksDropped) / cycle.blocksInCycle;
        long remaningBlockToDrop = (1000000000000 - cycle.blocksDropped) % cycle.blocksInCycle;
        _output = ((cycle.cycleHeigth * cyclesToDo) + DropRocks(cycle.blocksDropped + (int)remaningBlockToDrop)).ToString();
        return -1;
    }
    public (int stackHeigth, int blocksDropped, int blocksInCycle, int cycleHeigth) FindCycle()
    {
        RestingRocks = new SortedList<int, BitArray>() {{0, new BitArray(GAMEWIDTH, true)}};
        ShapeIndex = 0;
        MoveXIndex = 0;
        Dictionary<(string cavestate, int move, int shape), (int rocksInCycle, int cycleHeigth, int countHits)> states = new();
        int blocksDropped = 0;
        var nextMove = 0;
        var nextShape = 0;
        (string cavestate, int move, int shape) state = GetState(MoveXIndex % Moves.Length, ShapeIndex % Shapes.Length);;
        while (blocksDropped < 100000) //Cycle must be before this!!!
        {
            state = state = GetState(MoveXIndex % Moves.Length, ShapeIndex % Shapes.Length);
            if(states.ContainsKey(state))
            {
                var prevState = states[state];
                var cycleHeigth = StackHeigth - prevState.cycleHeigth;
                var blocksDroppedInCycle = blocksDropped - prevState.rocksInCycle;
                if(prevState.countHits == 2) //Find same cycle two times to be sure it's i repeating cycle
                    break;
                states[state] = new (blocksDroppedInCycle, cycleHeigth, prevState.countHits + 1);
            }
            else
                states.Add(state, (blocksDropped, StackHeigth, 1));
                
            var keep = RestingRocks.Max(t => t.Key) - 1000;
            var min = RestingRocks.Min(t => t.Key);
            for (int i = keep; i >= min; i--)
                RestingRocks.Remove(i);

            nextShape = GetNextShapeIndex();
            var shape = Shapes[nextShape].Spawn(StackHeigth);
            do
            {
                nextMove = GetNextXMove();
                int moveX = shape.MoveX(nextMove);
                if (moveX == -1)
                    break;
            } while (shape.MoveY());

            foreach (var b in shape.Block)
            {
                if (!RestingRocks.ContainsKey(b.Y))
                    RestingRocks[b.Y] = new BitArray(GAMEWIDTH, false);
                RestingRocks[b.Y][b.X] = true;
            }
            blocksDropped++;
        }
        return (StackHeigth, blocksDropped, states[state].rocksInCycle, states[state].cycleHeigth);
    }

    private (string cavestate, int move, int shape) GetState(int move, int shape)
    {
        List<string> temp = new List<string>();
        var block = RestingRocks.Reverse().Take(20).ToArray();
        foreach (var r in block)
        {
            var s = new string[7];
            for (int i = 0; i < 7; i++)
                s[i] = r.Value[i].ToString();
            temp.Add(string.Join("", s));
        }   
        return (string.Join(";", temp), move, shape);
    }
    public class Shape
    {
        public Shape(Point[] block)
        {
            Block = block;
        }
        private Shape Clone() => new Shape(this.Block.Select(t => new Point(t.X, t.Y)).ToArray());

        public Point[] Block { get; set; }

        public int MaxX => Block.Max(p => p.X);
        public int MaxY => Block.Max(p => p.Y);
        public int MinX => Block.Min(p => p.X);
        public int MinY => Block.Min(p => p.Y);
        public int RockHeigth => (MaxY - MinY) + 1;

        public int MoveX(int move)
        {
            if (move == -1 && MinX == 0)
                return 0;
            if (move == 1 && MaxX == GAMEWIDTH - 1)
                return 0;

            if (Block.Any(b => RestingRocks.ContainsKey(b.Y) && RestingRocks[b.Y][b.X + move]))
                return 0;

            for (int i = 0; i < Block.Length; i++)
                Block[i].X += move;
            return 1;
        }

        public Shape Spawn(int stackHeigth)
        {
            var newRock = this.Clone();
            for (int i = 0; i < newRock.Block.Length; i++)
                newRock.Block[i].X += 2;
            for (int i = 0; i < newRock.Block.Length; i++)
                newRock.Block[i].Y += (4 + stackHeigth);
            return newRock;
        }

        public bool MoveY()
        {
            if (Block.Any(b => RestingRocks.ContainsKey(b.Y - 1) && RestingRocks[b.Y - 1][b.X]))
                return false;
            for (int i = 0; i < Block.Length; i++)
                Block[i].Y--;
            return true;
        }
    }
}