namespace AoC2020
{
    public class Dag3 : IDag
    {
        private static string[][] InputLines = InputReader.GetInputLinesMatrix("dag3.txt");
        public int Star1()
        {
            return TobogganRun((1, 3));
        }

        public int Star2()
        {
            return TobogganRun((1,1)) * TobogganRun((1,3)) * TobogganRun((1,5)) * TobogganRun((1,7)) * TobogganRun((2,1));
        }

        private int TobogganRun((int x, int y) slope)
        {
            //var slope = (x: 1,y: 3);
            var position = (x: 0, y: 0);
            var treeCount = 0;
            while (position.x < InputLines.Length)
            {
                if(InputLines[position.x][position.y] == "#")
                    treeCount++;
                position.x += slope.x;
                position.y += slope.y;
                if(position.y >= InputLines[0].Length)
                    position.y -= InputLines[0].Length;
            }
            return treeCount;
        }
    }
}