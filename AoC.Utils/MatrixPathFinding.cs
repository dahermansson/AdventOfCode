using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public class MatrixPathFinding<T>
    {
        private Dictionary<MatrixPoint<T>, int> Cost{ get; set; } = new Dictionary<MatrixPoint<T>, int>();
        private SortedList<int, MatrixPoint<T>> Candidates { get; set; } = new SortedList<int, MatrixPoint<T>>(new DuplicateKeyComparer());
        private Dictionary<MatrixPoint<T>, MatrixPoint<T>> Previous { get; set; } = new Dictionary<MatrixPoint<T>, MatrixPoint<T>>();
        private HashSet<MatrixPoint<T>> Visited { get; set; } = new HashSet<MatrixPoint<T>>();
        private Matrix<T> _graph { get; set; }
        public MatrixPathFinding(Matrix<T> graph)
        {
            _graph = graph;
            foreach (var node in _graph.GetAllPositions())
                Cost.Add(node, 100000);
        }

        public void Dijkstra(MatrixPoint<T> start, MatrixPoint<T> end, Func<MatrixPoint<T>, MatrixPoint<T>, int> costFunc)
        {
            if(start == null)
                throw new ArgumentNullException("start");
            Cost[start] = 0;
            Candidates.Add(0,start);
            
            while (Candidates.Any())
            {
                MatrixPoint<T> current = Candidates.ElementAt(0).Value;
                if(current == end)
                    break;
                Visited.Add(current);
                Candidates.RemoveAt(Candidates.IndexOfValue(current));
                foreach (var node in _graph.GetCrossNeighbours(current).Where(t => (Convert.ToInt32(t.Value) <= Convert.ToInt32(current.Value) || Convert.ToInt32(t.Value) - 1 == Convert.ToInt32(current.Value) ) && !Visited.Contains(t)).ToArray())
                {
                    //var cost = costFunc(current, node);
                    var cost = Cost[current] + Convert.ToInt32(node.Value);
                    if(cost < Cost[node])
                    { 
                        Cost[node] = cost;
                        Previous[node] = current;
                        Candidates.Add(cost, node);
                    }
                }
            }
        }

        public IEnumerable<MatrixPoint<T>> GetShortestPath(MatrixPoint<T> start, MatrixPoint<T> end)
        {
            var current = end;
            do 
            {
                yield return current;
                if(Previous.ContainsKey(current))
                    current = Previous[current];
                else
                    break;
            } while (current != start);
        }
        public int GetCost(MatrixPoint<T> node) => Cost[node];
    }

    public class DuplicateKeyComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            int result = x.CompareTo(y);
            if (result == 0)
                return 1;
            else
                return result;
        }

    }
}