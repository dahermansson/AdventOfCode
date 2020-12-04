using System.Linq;
namespace AoC2015
{
    public class Dag2 : IDag
    {
          private static string[] InputLines = InputReader.GetInputLines("dag2.txt");

        public int Star1()
        {
            return PackageWrapperCalculator("2x3x4".Split("x").Select(int.Parse).ToArray());
        }

        private int PackageWrapperCalculator(int[] d)
        {
            return d[0]*
        }
        public int Star2()
        {
            int floor = 0, index = 0;
            while (floor >= 0)
            {
                if (InputLines[index++]== '(')
                    floor++;
                else
                    floor--;
            }
            return index;
        }
    }
}