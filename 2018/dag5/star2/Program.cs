using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace star2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = string.Empty;

            var res = new List<Tuple<int, int>>();
            for (int c = 65; c < 91; c++)
            {
                input = File.ReadAllText("../star1/input.txt");
                input = input.Replace(((char)c).ToString(), "").Replace(((char)(c + 32)).ToString(), "");;
                bool thingsRemoved = false;
                do 
                {
                    thingsRemoved = false;
                    for (int i = 0; i < input.Length-1; i++)
                    {
                        if(RemovePair(input[i], input[i+1]))
                        {
                            input = input.Remove(i, 2);
                            thingsRemoved = true;
                        }
                    }
                }while (thingsRemoved);

                res.Add(new Tuple<int, int>(c, input.Length));
            }
            Console.WriteLine(res.Min(t => t.Item2));
        }

        static bool RemovePair(char a, char b)
        {
            return Math.Abs(a-b) == 32;
        }
    }
}
