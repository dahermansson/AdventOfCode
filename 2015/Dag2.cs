using System.Linq;
namespace AoC2015
{
    public class Dag2 : IDag
    {
          private static string[] InputLines = InputReader.GetInputLines("dag2.txt");

        public int Star1()
        {
            return InputLines.Sum(t => PackageWrapperCalculator(t.Split("x").Select(int.Parse).ToArray()));
        }

        private int PackageWrapperCalculator(int[] d)
        {
            var lw = d[0]*d[1];
            var wh = d[1] * d[2];
            var hl = d[2] * d[0];
            var sides = new int[] {lw, wh, hl};
            return 2 * lw + 2 * wh + 2 * hl + sides.Min();
        }

        private int PackageRibbonCalculator(int[] d)
        {
            var sides = d.OrderBy(t => t).ToArray();
            return sides[0] + sides[0] + sides[1] + sides[1] + sides[0]* sides[1] * sides[2];
        }
        public int Star2()
        {
            return InputLines.Sum(t => PackageRibbonCalculator(t.Split("x").Select(int.Parse).ToArray()));
        }
    }
}