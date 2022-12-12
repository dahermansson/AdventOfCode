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

        public void Dijkstra(MatrixPoint<T> start, Func<int, MatrixPoint<T>, int> costFunc)
        {
            if(start == null)
                throw new ArgumentNullException("start");
            Cost[start] = 0;
            Candidates.Add(0,start);
            
            while (Candidates.Any())
            {
                MatrixPoint<T> current = Candidates.ElementAt(0).Value;
                Candidates.RemoveAt(Candidates.IndexOfValue(current));
                foreach (var node in _graph.GetCrossNeighbours(current).Where(t => !Visited.Contains(t) && (Convert.ToInt32(t.Value) <= Convert.ToInt32(current.Value) || Convert.ToInt32(t.Value) - 1 == Convert.ToInt32(current.Value))))
                {
                    var cost = costFunc(Cost[current], node);
                    if(cost < Cost[node])
                    { 
                        Cost[node] = cost;
                        Previous[node] = current;
                        Candidates.Add(cost, node);
                    }
                }
                Visited.Add(current);
            }
        }

        public IEnumerable<MatrixPoint<T>> GetShortestPath(MatrixPoint<T> start, MatrixPoint<T> end)
        {
            var res = new List<MatrixPoint<T>>();
            var current = end;
            do 
            {
                res.Add(current);
                if(!Previous.ContainsKey(current))
                    return new List<MatrixPoint<T>>();
                current = Previous[current];
            } while (!current.Equals(start));
            return res;
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