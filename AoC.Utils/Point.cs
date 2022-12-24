using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public record Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public Point(string input, string separator = ",")
        {
            X = int.Parse(input.Split(separator)[0]);
            Y = int.Parse(input.Split(separator)[1]);
        }
        
        //aoeaoeu x=12, y=123
        public Point(string input)
        {
            X = input.Split(',')[0].ExtraxtInteger();
            Y = input.Split(',')[1].ExtraxtInteger();
        }

        public Point GetNextInDir(Direction dir)
        {
            var direction = Directions.DIRECTIONS[(int)dir];
            return new Point(X + direction.X, Y + direction.Y);
        }
    }

    public static class Directions
    {
        public static readonly List<Point> DIRECTIONS = new List<Point>{
            new Point(-1, -1),
            new Point(0, -1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1),
            new Point(0, 1),
            new Point(-1, 1),
            new Point(-1, 0)
        };

        public static Point GetDirection(Direction dir) => dir switch
        {
            Direction.NorthWest => DIRECTIONS[0],
            Direction.North => DIRECTIONS[1],
            Direction.NorthEast => DIRECTIONS[2],
            Direction.East => DIRECTIONS[3],
            Direction.SouthEast => DIRECTIONS[4],
            Direction.South => DIRECTIONS[5],
            Direction.SouthWest => DIRECTIONS[6],
            Direction.West => DIRECTIONS[7],
            _ => DIRECTIONS[0]
        };
    }

    public enum Direction
    {
        NorthWest = 0,
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West
    }
}