using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;

namespace AdventOfCode2019
{
    public class Intcoder
    {
        public BigInteger[] IntCode { get; set; }
        public int Pointer { get; private set; }
        public bool Running { get; private set; }
        public bool Done { get; private set; }
        public List<BigInteger> Outputs { get; private set; }
        public OutputMode OutputMode { get; private set; }
        private string InputIntCode { get; set; }
        private int RelativeBase { get; set;}
        public Intcoder(string intcode, OutputMode outputMode)
        {
            InputIntCode = intcode;
            OutputMode = outputMode;
            var tempArray = InputIntCode.Split(',').Select(t => BigInteger.Parse(t)).ToArray();
            IntCode = new BigInteger[tempArray.Length*1000];
            Array.Copy(tempArray, IntCode, tempArray.Length);
            Done = false;
            if(OutputMode == OutputMode.OutputAndRunToEnd)
                Outputs = new List<BigInteger>();
            RelativeBase = 0;
        }
        public void Init(int position, BigInteger value)
        {
            IntCode[position] = value;
            Pointer = 0;
        }
        public BigInteger PositionZero { get { return IntCode[0];}}

        public void Reset()
        {
            Pointer = 0;
            IntCode = InputIntCode.Split(',').Select(t => BigInteger.Parse(t)).ToArray();
            Outputs.Clear();
        }
        private OpCode GetOpCode(string instruction)
        {
            return (OpCode)Enum.Parse(typeof(OpCode), instruction.Substring(instruction.Length -2, 2));
        }

        private ParameterMode GetParameterMode(string instruction, int parameter)
        {
            return (ParameterMode)Enum.Parse(typeof(ParameterMode), instruction.Substring(instruction.Length - (parameter+2), 1));
        }

        public BigInteger Exec()
        {
            return Exec(new Queue<BigInteger>(), false);
        }
        public BigInteger Exec(Queue<BigInteger> inputQueue)
        {
            return Exec(inputQueue, false);
        }

        public BigInteger Exec(Queue<BigInteger> inputQueue, bool reset)
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
                
                var param1Mode = GetParameterMode(instruction, 1);
                var param2Mode = GetParameterMode(instruction, 2);
                var param3Mode = GetParameterMode(instruction, 3);

                switch (opCode)
                {
                    case OpCode.Add:
                        SetParameterValue(param3, GetParamaterValue(param1, param1Mode) + GetParamaterValue(param2, param2Mode), param3Mode);
                        Pointer += 4;
                        break;
                    case OpCode.Multiply:
                        SetParameterValue(param3, GetParamaterValue(param1, param1Mode) * GetParamaterValue(param2, param2Mode), param3Mode);
                        Pointer += 4;
                        break;
                    case OpCode.Input:
                        SetParameterValue(param1, inputQueue.Dequeue(), param1Mode);
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
                            Pointer = (int)GetParamaterValue(param2, param2Mode);
                        else
                            Pointer += 3;
                        break;
                    case OpCode.JumpFalse:
                        if(GetParamaterValue(param1, param1Mode) == 0)
                            Pointer = (int)GetParamaterValue(param2, param2Mode);
                        else
                            Pointer += 3;
                        break;
                    case OpCode.LessThen:
                        if(GetParamaterValue(param1, param1Mode)< GetParamaterValue(param2, param2Mode))
                            SetParameterValue(param3, 1, param3Mode);
                        else
                            SetParameterValue(param3, 0, param3Mode);
                        Pointer += 4;
                        break;
                    case OpCode.Equals:
                        if(GetParamaterValue(param1, param1Mode) == GetParamaterValue(param2, param2Mode))
                            SetParameterValue(param3, 1, param3Mode);
                        else
                            SetParameterValue(param3, 0, param3Mode);
                        Pointer += 4;
                        break;
                    case OpCode.AdjustRelativBase:
                        RelativeBase += (int)GetParamaterValue(param1, param1Mode);
                        Pointer += 2;
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
        private void SetParameterValue(int Pointer, BigInteger value, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Position:
                    IntCode[(int)IntCode[Pointer]] = value;
                    break;
                case ParameterMode.Immediate:
                    throw new Exception("Not supportet ParameterMode");
                case ParameterMode.Relative:
                    IntCode[(int)(RelativeBase + IntCode[Pointer])] = value;
                    break;
                default:
                    throw new Exception("Invalid ParameterMode");
            }
        }
        private BigInteger GetParamaterValue(int Pointer, ParameterMode mode)
        {
            switch (mode)
            {
                case ParameterMode.Position:
                    return IntCode[(int)IntCode[Pointer]];
                case ParameterMode.Immediate:
                    return IntCode[Pointer];
                case ParameterMode.Relative:
                    return IntCode[(int)(RelativeBase + IntCode[Pointer])];
                default:
                    throw new Exception("Invalid ParameterMode");
            }
        }
        private enum OpCode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4, 
            JumpTrue = 5,
            JumpFalse = 6, 
            LessThen = 7, 
            Equals = 8,
            AdjustRelativBase = 9,
            Terminate = 99
        }
        private enum ParameterMode
        {
            Position = 0,
            Immediate = 1,
            Relative = 2
        }
    }
    public enum OutputMode
    {
        ReturnAndHalt,
        OutputAndRunToEnd
    }
    
}