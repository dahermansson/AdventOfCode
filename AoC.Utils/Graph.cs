using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public class MatrixPathFinding<T>
    {
        private Dictionary<MatrixPoint<T>, int> Cost{ get; set; } = new Dictionary<MatrixPoint<T>, int>();
        private Dictionary<MatrixPoint<T>, MatrixPoint<T>> previous { get; set; } = new Dictionary<MatrixPoint<T>, MatrixPoint<T>>();
        private HashSet<MatrixPoint<T>> Visited { get; set; } = new HashSet<MatrixPoint<T>>();
        private HashSet<MatrixPoint<T>> UnVisited { get; set; } = new HashSet<MatrixPoint<T>>();
        private Matrix<T> _graph { get; set; }
        public MatrixPathFinding(Matrix<T> graph)
        {
            _graph = graph;
            foreach (var node in _graph.GetAllPositions())
            {
                Cost.Add(node, 1000000);
                UnVisited.Add(node);
            }
        }

        public void Dijkstra(MatrixPoint<T> start, MatrixPoint<T> end)
        {
            if(start == null)
                throw new ArgumentNullException("start");
            Cost[start] = 0;
            
            while (UnVisited.Any())
            {
                MatrixPoint<T> current = UnVisited.OrderBy(t => Cost[t]).First();
                Visited.Add(current);
                UnVisited.Remove(current);
                foreach (var node in _graph.GetCrossNeighbours(current).Where(t => UnVisited.Contains(t)))
                {
                    var cost = Cost[current] + Convert.ToInt32(node.Value);
                    if(cost < Cost[node])
                    { 
                        Cost[node] = cost;
                        previous[node] = current;
                    }
                }
            }
        }

        public IEnumerable<MatrixPoint<T>> GetShortestPath(MatrixPoint<T> start, MatrixPoint<T> end)
        {
            var current = end;
            while (current != start)
            {
                yield return current;
                current = previous[current];
            }
            yield return current;
        }

        public int GetCost(MatrixPoint<T> node) => Cost[node];
    }
}