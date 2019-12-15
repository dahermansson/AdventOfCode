using System;
using System.Linq;
namespace AdventOfCode2019Dag5
{
    public class Intcoder
    {
        public int[] IntCode { get; set; }
        public int[] inputs {get; set;}
        private int inputsIndex = 0;
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
            int optCode;
            var instruction = IntCode[pointer].ToString();
            
            if(instruction.Length > 1)
                optCode = int.Parse(instruction.Substring(instruction.Length -2, 2));
            else
                optCode = int.Parse(instruction);
            
            var param1 = IntCode.Length >= pointer + 1 ? pointer + 1 : 0;
            var param2 = IntCode.Length >= pointer + 2 ? pointer + 2 : 0;
            var param3 = IntCode.Length >= pointer + 3 ? pointer + 3 : 0;
            
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
                SetParameterValue(param3, GetParamaterValue(param1, param1Mode) + GetParamaterValue(param2, param2Mode));
            if(optCode == 2)
                SetParameterValue(param3, GetParamaterValue(param1, param1Mode) * GetParamaterValue(param2, param2Mode));
            if(optCode == 3)
            {
                SetParameterValue(param1, inputs[inputsIndex++]);
                return pointer + 2;
            }
            if(optCode == 4)
            {
                Console.WriteLine(GetParamaterValue(param1, param1Mode));
                return pointer + 2;
            }
            if(optCode == 5)
            {
                if(GetParamaterValue(param1, param1Mode) != 0)
                    return GetParamaterValue(param2, param2Mode);
                return pointer + 3;
            }
            if(optCode == 6)
            {
                if(GetParamaterValue(param1, param1Mode) == 0)
                    return GetParamaterValue(param2, param2Mode);
                return pointer + 3;
            }
            if(optCode == 7)
            {
                if(GetParamaterValue(param1, param1Mode)< GetParamaterValue(param2, param2Mode))
                    SetParameterValue(param3, 1);
                else
                    SetParameterValue(param3, 0);
            }
            if(optCode == 8)
            {
                if(GetParamaterValue(param1, param1Mode) == GetParamaterValue(param2, param2Mode))
                    SetParameterValue(param3, 1);
                else
                    SetParameterValue(param3, 0);
            }
            if(optCode == 99)
                return -1;
            return pointer + 4;
        }

        private void SetParameterValue(int pointer, int value)
        {
            IntCode[IntCode[pointer]] = value;   
        }

        private int GetParamaterValue(int pointer, int mode)
        {
            if(mode == 0)
                return IntCode[IntCode[pointer]];
            else 
                return IntCode[pointer];
        }
    }
}