using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{
    public class Graph<T> where T: notnull
    {
        public Dictionary<T, HashSet<Edge<T>>> Nodes {get; set;} = new Dictionary<T, HashSet<Edge<T>>>();
        public Graph(IEnumerable<T> nodes, IEnumerable<Edge<T>> edges)
        {
            foreach (var node in nodes)
                Nodes[node] = new HashSet<Edge<T>>();

            foreach (var edge in edges)
            {
                Nodes[edge.Start].Add(edge);
                Nodes[edge.End].Add(new Edge<T>(edge.End, edge.Start));
            }
        }
    }

    public class Edge<T>
    {
        public Edge(T start, T end)
        {
            Start = start;
            End = end;
        }
        public T Start { get; set; }
        public T End { get; set; }
    }
}