using System.Linq;
using AoC.Utils;

namespace AoC2020
{
    public class Dag8 : IDay
    {
        private string[] Input = InputReader.GetInputLines("dag8.txt");
        public int Star1()
        {
            var cpu = new CPU(Input);
            cpu.Run();
            return cpu.Accumulator;
        }

        public int Star2()
        {
            for (int i = 0; i < Input.Count(); i++)
            {
                Input = InputReader.GetInputLines("dag8.txt");
                if(Input[i].StartsWith("jmp"))
                {
                    Input[i] = Input[i].Replace("jmp", "nop");
                    var cpu = new CPU(Input);
                    if(cpu.Run())
                        return cpu.Accumulator;
                }
            }
            return -1;
        }
        public string Output => throw new System.NotImplementedException();
    }
}