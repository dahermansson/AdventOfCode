﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using AdventOfCode2019;
using System.Drawing;

namespace dag11
{
    class Program
    {
        static void Main(string[] args)
        {
            Star1();
            Star2();
        }
        private static void Star2()
        {
            Intcoder intcoder = new Intcoder(Utils.ReadAllInput("input.txt"), OutputMode.ReturnAndHalt);
            var robot = new Robot();
            robot.PaintedPanels.Add(new Tuple<int, int>(100,100), new Panel(){CurrentColor = Color.White});
            var input = new Queue<System.Numerics.BigInteger>();
            var nextPosition = new Tuple<int,int>(100,100);
            while (!intcoder.Done)
            {
                input.Enqueue((int)robot.ColorInPosition(nextPosition));
                var paintedColor = intcoder.Exec(input) == 0 ? Color.Black : Color.White;
                var directionTurned = intcoder.Exec() == 0 ? Turn.Left : Turn.Right;
                nextPosition = robot.GoToNextPosition(paintedColor, directionTurned);
            }
            var width = Math.Abs(robot.PaintedPanels.Max(t => t.Key.Item1));
            var height = Math.Abs(robot.PaintedPanels.Max(t => t.Key.Item2));
            var bitmap = new Bitmap(height + 1, width + 1);
            foreach (var panel in robot.PaintedPanels)
            {
                bitmap.SetPixel(Math.Abs(panel.Key.Item2), Math.Abs(panel.Key.Item1), panel.Value.CurrentColor == dag11.Color.Black ? System.Drawing.Color.Black : System.Drawing.Color.White);
                bitmap.Save("dag11Star2.png");     
            }
        }
        private static void Star1()
        {
            Intcoder intcoder = new Intcoder(Utils.ReadAllInput("input.txt"), OutputMode.ReturnAndHalt);
            var robot = new Robot();
            var input = new Queue<System.Numerics.BigInteger>();
            var nextPosition = new Tuple<int,int>(100,100);
            while (!intcoder.Done)
            {
                input.Enqueue((int)robot.ColorInPosition(nextPosition));
                var paintedColor = intcoder.Exec(input) == 0 ? Color.Black : Color.White;
                var directionTurned = intcoder.Exec() == 0 ? Turn.Left : Turn.Right;
                nextPosition = robot.GoToNextPosition(paintedColor, directionTurned);
            }
            Console.WriteLine($"Star 1: {robot.PaintedPanels.Count - 1}");
        }
    }

    public class Robot
    {
        public Robot()
        {
            CurrentDirection = Direction.Up;
            CurrentPosition = new Tuple<int, int>(100,100);
            PaintedPanels = new Dictionary<Tuple<int, int>, Panel>();
        }
        public Tuple<int, int> CurrentPosition { get; set; }
        public Direction CurrentDirection { get; set; }
        public Dictionary<Tuple<int, int>, Panel> PaintedPanels { get; set; }
        public Tuple<int, int> GoToNextPosition(Color color, Turn turn)
        {
            var pos = new Tuple<int, int>(CurrentPosition.Item1, CurrentPosition.Item2);
            if(!PaintedPanels.ContainsKey(pos))
                PaintedPanels.Add(pos, new Panel());
            PaintedPanels[pos].Painted++;
            PaintedPanels[pos].CurrentColor = color;
            CurrentDirection = GetNextDirection(turn);
            var nyPos = GetNextPosition(CurrentDirection);
            CurrentPosition = nyPos;
            return CurrentPosition;
        }

        public Tuple<int, int> GetNextPosition(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up: 
                    return new Tuple<int, int>(CurrentPosition.Item1, CurrentPosition.Item2 +1 );
                case Direction.Right: 
                    return new Tuple<int, int>(CurrentPosition.Item1 + 1, CurrentPosition.Item2);
                case Direction.Down: 
                    return new Tuple<int, int>(CurrentPosition.Item1, CurrentPosition.Item2 - 1);
                case Direction.Left: 
                    return new Tuple<int, int>(CurrentPosition.Item1 - 1, CurrentPosition.Item2);
                default:
                    return null;
            }
        }

        public Color ColorInPosition(Tuple<int, int> position)
        {
            return PaintedPanels.ContainsKey(position) ? PaintedPanels[position].CurrentColor : Color.Black;
        }

        private Direction GetNextDirection(Turn turn)
        {
            switch (CurrentDirection)
            {
                case Direction.Up: 
                    return turn == Turn.Left ? Direction.Left : Direction.Right;
                case Direction.Right: 
                    return turn == Turn.Left ? Direction.Up : Direction.Down;
                case Direction.Down: 
                    return turn == Turn.Left ? Direction.Right : Direction.Left;
                case Direction.Left: 
                    return turn == Turn.Left ? Direction.Down : Direction.Up;
                default:
                    return Direction.Up;
            }
        }
    }

    public class Panel
    {
        public Panel()
        {
            CurrentColor = Color.Black;
        }
        public Color CurrentColor { get; set; }
        public int Painted { get; set; }

    }
    public enum Color
    {
        Black = 0,
        White = 1
    }
    public enum Turn
    {
        Left = 0,
        Right = 1
    }

    public enum Direction
    {
        Up = 0, 
        Right = 1,
        Down = 2, 
        Left = 3
    }
}
