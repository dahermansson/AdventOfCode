using AoC.Utils;

namespace AoC2020
{
    public class Dag1 : IDay
    {
        private static int[] InputLines = InputReader.GetInputLines<int>("dag1.txt");

        public int Star1()
        {
            foreach (int i in InputLines)
                foreach (int c in InputLines)
                    if(i + c == 2020)
                        return i * c;
            return -1;
        }

        public int Star2()
        {
            foreach (int i in InputLines)
                foreach (int c in InputLines)
                    foreach (int t in InputLines)
                        if(i + c + t== 2020)
                            return i * c * t;
            return -1;
        }
        public string Output => throw new System.NotImplementedException();
    }
}