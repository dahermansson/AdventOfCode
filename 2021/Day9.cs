using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day9 : IDay
    {
        public string Output => throw new NotImplementedException();

        private Matrix<int> Input = new Matrix<int>(InputReader.GetInputLines("9.txt"), false);
        public int Star1() => Input.GetAllPositions().Where(p => Input.GetCrossNeighbours(p.Row, p.Column).All(v => v.Value > p.Value)).Sum(t => t.Value +1);

        public int Star2()
        {
            var lowPoints = Input.GetAllPositions().Where(p => Input.GetCrossNeighbours(p.Row, p.Column).All(v => v.Value > p.Value)).ToList();
            var basinsSize = new List<int>();
            var posInBasin = new HashSet<string>();
            foreach (var lowPoint in lowPoints)
            {
                posInBasin.Add(lowPoint.PosToString);
                var basinPositions = new List<MatrixPoint<int>> { lowPoint };
                for (int i = 0; i < basinPositions.Count; i++)
                {
                    var basins = GetBasin(basinPositions[i]);
                    foreach (var basin in basins)
                        if(!posInBasin.Contains(basin.PosToString))
                        {
                            posInBasin.Add(basin.PosToString);
                            basinPositions.Add(basin);
                        }
                }
                basinsSize.Add(basinPositions.Count);  
            }
            return basinsSize.OrderByDescending(t => t).Take(3).Aggregate((n, next) => n *= next);
        }

        private IEnumerable<MatrixPoint<int>>GetBasin(MatrixPoint<int> pos)
        {
            return Input.GetCrossNeighbours(pos.Row, pos.Column).Where(t => t.Value != 9 && t.Value > pos.Value).ToList();
        }
    }
}