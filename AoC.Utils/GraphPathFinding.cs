using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public class GraphPathFinding<T> where T : notnull
    {
        public delegate bool IsAllowedDelegate(List<T> visited, T node);
        public int BreadthFirst (Graph<T> graph, T start, T end, IsAllowedDelegate IsAllowed )
        {
            var path = new List<T>(){start};
            var queue = new Queue<List<T>>();
            queue.Enqueue(path);
            int paths = 0;
            while (queue.Any())
            {
                var current = queue.Dequeue();
                if(current.Last().Equals(end))
                {
                    paths++;
                    continue;
                }
                foreach (var node in graph.Nodes[current.Last()])
                {
                    if(IsAllowed(current, node.End))
                    {
                        var newPath = new List<T>(current);
                        newPath.Add(node.End);
                        queue.Enqueue(newPath);
                    }
                }
            }
            return paths;
        }
    }
}