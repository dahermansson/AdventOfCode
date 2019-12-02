using System;
using System.IO;
using System.Linq;

namespace dag2
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
            Star2();
        }

        static void Star2()
        {
            int target = 19690720;
            int result = 0;
            int noun = 0;
            int verb = 0;
            while (target != result)
            {
                noun++;
                if(noun == 100)
                {
                    noun = 0;
                    verb++;
                }
                var intcode = File.ReadAllText("input.txt").Split(',').Select(t => int.Parse(t)).ToArray();
                intcode[1] = noun;
                intcode[2] = verb;
                int exec = 0;
                while (Exec(intcode, exec, intcode[exec+1], intcode[exec+2], intcode[exec+3]) == 0)
                {
                    exec+=4;
                }
                result = intcode[0];
            }
            Console.WriteLine($"Star 2: {100 * noun + verb}");
        }

        static void Star1()
        {
            var intcode = File.ReadAllText("input.txt").Split(',').Select(t => int.Parse(t)).ToArray();
            intcode[1] = 12;
            intcode[2] = 2;
            int exec = 0;
            while (Exec(intcode, exec, intcode[exec+1], intcode[exec+2], intcode[exec+3]) == 0)
            {
                exec+=4;
            }
            Console.WriteLine($"Star 1: {intcode[0]}");
        }

        static int Exec(int[] intcode, int instruction, int param1, int param2, int output)
        {
            if(intcode[instruction] == 1)
                intcode[output] = intcode[param1] + intcode[param2];
            if(intcode[instruction] == 2)
                intcode[output] = intcode[param1] * intcode[param2];
            if(intcode[instruction] == 99)
                return -1;
            return 0;
        }

    }
}
