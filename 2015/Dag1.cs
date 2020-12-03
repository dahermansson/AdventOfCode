using System.Linq;
namespace AoC2015
{
    public class Dag1 : IDag
    {
          private static string InputLines = InputReader.GetInputLine("dag1.txt");

        public int Star1() => InputLines.Count(t => t == '(') - InputLines.Count(t => t == ')');
        
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