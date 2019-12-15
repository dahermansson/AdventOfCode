using System;
using System.Linq;
using System.Collections.Generic;
namespace AdventOfCode2019
{
    public class Intcoder
    {
        public int[] IntCode { get; set; }
        public int Pointer { get; private set; }
        public bool Running { get; private set; }
        public bool Done { get; private set; }
        public List<int> Outputs { get; private set; }
        public OutputMode OutputMode { get; private set; }
        private string InputIntCode { get; set; }
        public Intcoder(string intcode, OutputMode outputMode)
        {
            InputIntCode = intcode;
            OutputMode = outputMode;
            IntCode = InputIntCode.Split(',').Select(t => int.Parse(t)).ToArray();
            Done = false;
            if(OutputMode == OutputMode.OutputAndRunToEnd)
                Outputs = new List<int>();
        }
        public void Init(int position, int value)
        {
            IntCode[position] = value;
            Pointer = 0;
        }
        public int PositionZero { get { return IntCode[0];}}

        public void Reset()
        {
            Pointer = 0;
            IntCode = InputIntCode.Split(',').Select(t => int.Parse(t)).ToArray();
            Outputs.Clear();
        }
        private OpCode GetOpCode(string instruction)
        {
            return (OpCode)Enum.Parse(typeof(OpCode), instruction.Substring(instruction.Length -2, 2));
        }

        public int Exec(Queue<int> inputQueue)
        {
            return Exec(inputQueue, false);
        }

        public int Exec(Queue<int> inputQueue, bool reset)
        {
            if(reset)
                Reset();
            Running = true;
            Done = false;
            while(Running && !Done)
            {
                var instruction = IntCode[Pointer].ToString().PadLeft(5, '0');
                var opCode = GetOpCode(instruction);
                var param1 = IntCode.Length >= Pointer + 1 ? Pointer + 1 : 0;
                var param2 = IntCode.Length >= Pointer + 2 ? Pointer + 2 : 0;
                var param3 = IntCode.Length >= Pointer + 3 ? Pointer + 3 : 0;
                
                var param1Mode = int.Parse(instruction.Substring(instruction.Length - 3, 1));
                var param2Mode = int.Parse(instruction.Substring(instruction.Length - 4, 1));
                var param3Mode = int.Parse(instruction.Substring(instruction.Length - 5, 1));

                switch (opCode)
                {
                    case OpCode.Add:
                        SetParameterValue(param3, GetParamaterValue(param1, param1Mode) + GetParamaterValue(param2, param2Mode));
                        Pointer += 4;
                        break;
                    case OpCode.Multiply:
                        SetParameterValue(param3, GetParamaterValue(param1, param1Mode) * GetParamaterValue(param2, param2Mode));
                        Pointer += 4;
                        break;
                    case OpCode.Input:
                        SetParameterValue(param1, inputQueue.Dequeue());
                        Pointer += 2;
                        break;
                    case OpCode.Output:
                        var value = GetParamaterValue(param1, param1Mode);
                        Pointer += 2;
                        if(OutputMode == OutputMode.ReturnAndHalt)
                        {
                            Running = false;
                            return value;
                        }
                        else
                            Outputs.Add(value);
                        break;
                    case OpCode.JumpTrue:
                        if(GetParamaterValue(param1, param1Mode) != 0)
                            Pointer = GetParamaterValue(param2, param2Mode);
                        else
                            Pointer += 3;
                        break;
                    case OpCode.JumpFalse:
                        if(GetParamaterValue(param1, param1Mode) == 0)
                            Pointer = GetParamaterValue(param2, param2Mode);
                        else
                            Pointer += 3;
                        break;
                    case OpCode.LessThen:
                        if(GetParamaterValue(param1, param1Mode)< GetParamaterValue(param2, param2Mode))
                            SetParameterValue(param3, 1);
                        else
                            SetParameterValue(param3, 0);
                        Pointer += 4;
                        break;
                    case OpCode.Equals:
                        if(GetParamaterValue(param1, param1Mode) == GetParamaterValue(param2, param2Mode))
                            SetParameterValue(param3, 1);
                        else
                            SetParameterValue(param3, 0);
                        Pointer += 4;
                        break;
                    case OpCode.Terminate:
                        Running = false;
                        Done = true;
                        break;
                    default:
                        throw new Exception("Invalid optCode");
                }
            }
            return -1;
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

        enum OpCode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4, 
            JumpTrue = 5,
            JumpFalse = 6, 
            LessThen = 7, 
            Equals = 8, 

            Terminate = 99
        }

    }
    public enum OutputMode
    {
        ReturnAndHalt,
        OutputAndRunToEnd
    }
    
}