using System;

namespace dag4
{
    class Program
    {
        static void Main(string[] args)
        {
            int min = 278384;
            int max = 824795;

            Star1(min, max);
            Star2(min, max);
        }

        static void Star1(int min, int max)
        {
            int antal=0;
            for (int i = min; i <= max; i++)
                if(Adj(i) && NoDecrease(i))
                    antal++;
            Console.WriteLine($"Star 1: {antal}");
        }

        static void Star2(int min, int max)
        {
            int antal=0;
            for (int i = min; i <= max; i++)
                if(AdjNoLargeGroup(i) && NoDecrease(i))
                    antal++;
            Console.WriteLine($"Star 2: {antal}");
        }

        static bool Adj(int number)
        {
            var sNumber = number.ToString();
            for (int i = 0; i < sNumber.Length-1; i++)
                if(sNumber[i] == sNumber[i+1])
                    return true;
            return false;
        }

        static bool AdjNoLargeGroup(int number)
        {
            var sNumber = number.ToString();
            for (int i = 0; i < sNumber.Length-1; i++)
                if(sNumber[i] == sNumber[i+1] && sNumber.Split(sNumber[i]).Length == 3)
                    return true;
            return false;
        }

        static bool NoDecrease(int number)
        {
            var cNumber = number.ToString().ToCharArray();
            for (int i = 0; i < cNumber.Length-1; i++)
                if(cNumber[i] > cNumber[i+1])
                    return false;
            return true;
        }
    }
}
