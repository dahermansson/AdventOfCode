using System;
using System.Linq;
using System.Collections.Generic;
namespace AdventOfCode2019
{
    public class Intcoder
    {
        public int Pointer { get; private set; }
        public int[] IntCode { get; set; }

        public Queue<int> Inputs {get; set; }
        public Queue<int> Outputs {get; set; }
        public Intcoder(string intcode)
        {
            IntCode = intcode.Split(',').Select(t => int.Parse(t)).ToArray();
            Inputs = new Queue<int>();
            Outputs = new Queue<int>();
        }
        public void Init(int position, int value)
        {
            IntCode[position] = value;
            Pointer = 0;
        }
        public int PositionZero { 
            get
            {
                return IntCode[0];
            }
        }

        public void Reset(string intcode, bool clearInputOutput)
        {
            IntCode = intcode.Split(',').Select(t => int.Parse(t)).ToArray();
            if(clearInputOutput)
                Inputs.Clear();
        }
        public IEnumerable<int> Exec()
        {
            bool running = true;
            while(running)
            {
                int optCode;
                var instruction = IntCode[Pointer].ToString();
                
                if(instruction.Length > 1)
                    optCode = int.Parse(instruction.Substring(instruction.Length -2, 2));
                else
                    optCode = int.Parse(instruction);
                
                var param1 = IntCode.Length >= Pointer + 1 ? Pointer + 1 : 0;
                var param2 = IntCode.Length >= Pointer + 2 ? Pointer + 2 : 0;
                var param3 = IntCode.Length >= Pointer + 3 ? Pointer + 3 : 0;
                
                var param1Mode = 0;
                var param2Mode = 0;
                var param3Mode = 0;
                if(instruction.Length > 2)
                    param1Mode = int.Parse(instruction.Substring(instruction.Length - 3, 1));
                if(instruction.Length > 3)
                    param2Mode = int.Parse(instruction.Substring(instruction.Length - 4, 1));
                if(instruction.Length > 4)
                    param3Mode = int.Parse(instruction.Substring(instruction.Length - 5, 1));

                if(optCode == 1)
                {
                    SetParameterValue(param3, GetParamaterValue(param1, param1Mode) + GetParamaterValue(param2, param2Mode));
                    Pointer += 4;
                }

                if(optCode == 2)
                {
                    SetParameterValue(param3, GetParamaterValue(param1, param1Mode) * GetParamaterValue(param2, param2Mode));
                       Pointer += 4;
                }
                if(optCode == 3)
                {
                    SetParameterValue(param1, Inputs.Dequeue());
                    Pointer += 2;
                }
                if(optCode == 4)
                {
                    yield return (GetParamaterValue(param1, param1Mode));
                    Pointer += 2;
                }
                if(optCode == 5)
                {
                    if(GetParamaterValue(param1, param1Mode) != 0)
                        Pointer = GetParamaterValue(param2, param2Mode);
                    else
                        Pointer += 3;
                }
                if(optCode == 6)
                {
                    if(GetParamaterValue(param1, param1Mode) == 0)
                        Pointer = GetParamaterValue(param2, param2Mode);
                    else
                        Pointer += 3;
                }
                if(optCode == 7)
                {
                    if(GetParamaterValue(param1, param1Mode)< GetParamaterValue(param2, param2Mode))
                        SetParameterValue(param3, 1);
                    else
                        SetParameterValue(param3, 0);
                    Pointer += 4;
                }
                if(optCode == 8)
                {
                    if(GetParamaterValue(param1, param1Mode) == GetParamaterValue(param2, param2Mode))
                        SetParameterValue(param3, 1);
                    else
                        SetParameterValue(param3, 0);
                    Pointer += 4;
                }
                if(optCode == 99)
                    running = false;
                if(!new int[]{1,2,3,4,5,6,7,8,99}.Any(t => t == optCode))
                    throw new Exception();
                
            }
        }

        private void SetParameterValue(int Pointer, int value)
        {
            IntCode[IntCode[Pointer]] = value;   
        }

        private int GetParamaterValue(int Pointer, int mode)
        {
            if(mode == 0)
                return IntCode[IntCode[Pointer]];
            else 
                return IntCode[Pointer];
        }
    }
}