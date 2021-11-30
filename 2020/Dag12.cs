using System;
using System.Linq;
using System.Collections.Generic;
using AoC.Utils;

namespace AoC2020
{
    public class Dag12 : IDag
    {
        public string Output => throw new System.NotImplementedException();

        private string[] input = InputReader.GetInputLines("dag12.txt");

        int _shipPointing = 0;
        //East, South, West, North
        int[] travel = new int[4];
        public int Star1()
        {
            
            var inst = input.Select(t => new {dir = t[0], dist = int.Parse(t.Remove(0, 1))}).ToList();
           foreach (var ins in inst)
               MoveShip(ins.dir, ins.dist);

            return Utils.ManhattanDistance(travel[0] - travel[2], travel[3] - travel[1]);
        }

        private void MoveShip(char dir, int dist)
        {
            var dirs = new char[]{'E', 'S', 'W', 'N'}.ToList();
            switch (dir)
            {
                case 'R':
                    _shipPointing = (_shipPointing + DegreeToIndex(dist, false)) %4;
                    break;
                case 'L':
                    _shipPointing = (_shipPointing + DegreeToIndex(dist, true)) % 4;
                    break;
                case 'F': 
                    travel[_shipPointing]+=dist;
                    break;
                default:
                    travel[dirs.IndexOf(dir)]+=dist;
                    break;
            }
        }
        
        void MoveWaypoint(char dir, int dist)
        {
            var dirs = new char[]{'E', 'S', 'W', 'N'}.ToList();
            switch (dir)
            {
                case 'R':
                    _shipPointing = (_shipPointing + DegreeToIndex(dist, false)) %4;
                    break;
                case 'L':
                    _shipPointing = (_shipPointing + DegreeToIndex(dist, true)) % 4;
                    break;
                case 'F': 
                    travel[_shipPointing]+=dist;
                    break;
                default:
                    travel[dirs.IndexOf(dir)]+=dist;
                    break;
            }
        }

        private int DegreeToIndex(int degree, bool neg)
        {
            if(degree == 90)
            {
                if(neg)
                    return 3;
                return 1;
            }
            if(degree == 180)
                return 2;
            if(degree ==270)
            {
                if(neg)
                    return 1;
                return 3;
            }
            return 0;
        }


        public int Star2()
        {
            throw new System.NotImplementedException();
        }
    }
}