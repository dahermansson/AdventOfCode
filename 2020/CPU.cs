using System.Collections.Generic;
using System.Linq;
namespace AoC2020
{
    public class CPU
    {
        private List<Instruction> _program;
        private int _pointer = 0;
        public int Accumulator { get; set; }

        public CPU(string[] instructions)
        {
            _program = instructions.Select(t => t.ToInstruction()).ToList();
        }
        public bool Run()
        {
            foreach (var instruction in GetNext())
            {
                if(instruction.Executed)
                    return false; //Kör inte evighetsloopar
                ExecuteInstruction(instruction);
            }
            return true; //Kört alla instruktioner korrekt
        }

        private void ExecuteInstruction(Instruction i)
        {
            switch (i.Type)
            {
                case Instructions.acc:
                    Accumulator+= i.Arg;
                    _pointer++;
                    break;
                case Instructions.jmp:
                    _pointer += i.Arg;
                    break;
                default:
                    _pointer++;
                    break;
            }
            i.Executed = true;
        }

        private IEnumerable<Instruction> GetNext()
        {
            while(_pointer < _program.Count) 
                yield return _program[_pointer];
        }
    }
    public class Instruction
    {
        public int Arg { get; set; }
        public Instructions Type { get; set; }
        public bool Executed {get; set;}
    }

    public enum Instructions
    {
        acc = 0,
        jmp = 1,
        nop = 2
    }
}