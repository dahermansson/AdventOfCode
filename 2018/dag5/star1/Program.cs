using System;
using System.IO;
namespace star1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllText("input.txt");
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

            Console.WriteLine(input.Length);
        }

        static bool RemovePair(char a, char b)
        {
            return Math.Abs(a-b) == 32;
        }
    }
}
