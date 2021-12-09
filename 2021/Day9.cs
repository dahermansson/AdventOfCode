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

        public int Star1() => Input.GetAllPositions().Where(p => Input.GetCrossNeighbours(p.Row, p.Col).All(v => v.Value > p.Value)).Sum(t => t.Value +1);
        

        public int Star2()
        {
            var lowPoints = Input.GetAllPositions().Where(p => Input.GetCrossNeighbours(p.Row, p.Col).All(v => v.Value > p.Value)).ToList();
            var basinsSize = new List<int>();
            var posInBasin = new List<(int Row, int Col)>();
            foreach (var lowpoint in lowPoints.OrderBy(t => t.Row).ThenBy(t => t.Col))
            {
                posInBasin.Add(new (lowpoint.Row, lowpoint.Col));
                var b = new List<(int Row, int Col, int Value)> { new(lowpoint.Row, lowpoint.Col, lowpoint.Value)};
                for (int i = 0; i < b.Count; i++)
                {
                    var basins = GetBasin(b[i]);
                    foreach (var basin in basins)
                    {
                        if(!posInBasin.Any(t => t.Row == basin.Row && t.Col == basin.Col))
                        {
                            posInBasin.Add(new (basin.Row, basin.Col));
                            b.Add(basin);
                        }
                    }
                }
                basinsSize.Add(b.Count);  
            }
            var bigThree = basinsSize.OrderByDescending(t => t).Take(3).ToArray();
            return bigThree[0] * bigThree[1] * bigThree[2];
        }

        private List<(int Row, int Col, int Value)>GetBasin((int Row, int Col, int Value)basin)
        {
            return Input.GetCrossNeighbours(basin.Row, basin.Col).Where(t => t.Value != 9 && t.Value > basin.Value).ToList();
        }
    }
}