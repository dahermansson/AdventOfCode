using System;
using System.IO;
using System.Linq;
namespace star2
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var line in File.ReadAllLines("input.txt"))
            {
                foreach (var compline in File.ReadAllLines("input.txt"))
                {
                    char[] res = new char[line.Length];
                    for (int i = 0; i < line.Length; i++)
                    {
                        if(line[i] != compline[i])
                            res[i] = ' ';
                        else
                            res[i] = line[i];   
                    }
                    var strRes = new string(res).Replace(" ", string.Empty);
                    if(Math.Abs(strRes.Length - line.Length) == 1)
                    {
                        Console.WriteLine(strRes);
                        return;
                    }
                }
            }
        }
    }
}
