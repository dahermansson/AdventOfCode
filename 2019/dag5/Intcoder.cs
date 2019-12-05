using System.Linq;
namespace AdventOfCode2019
{
    public class Intcoder
    {
        public int[] IntCode { get; set; }
        public Intcoder(string intcode)
        {
            IntCode = intcode.Split(',').Select(t => int.Parse(t)).ToArray();
        }
        public void Init(int position, int value)
        {
            IntCode[position] = value;
        }
        public int PositionZero { 
            get
            {
                return IntCode[0];
            }
        }
        public int Exec(int pointer)
        {
            if(IntCode[pointer] == 1)
                IntCode[IntCode[pointer+3]] = IntCode[IntCode[pointer+1]] + IntCode[IntCode[pointer+2]];
            if(IntCode[pointer] == 2)
                IntCode[IntCode[pointer+3]] = IntCode[IntCode[pointer+1]] * IntCode[IntCode[pointer+2]];
            if(IntCode[pointer] == 99)
                return -1;
            return pointer + 4;
        }
    }
}