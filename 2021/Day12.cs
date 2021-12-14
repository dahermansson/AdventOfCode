using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AoC.Utils;

namespace AoC2021
{
    public class Day12 : IDay
    {
        public string Output => throw new NotImplementedException();

        private string[] Input = InputReader.GetInputLines("12.txt");

        private Graph<string> _graph {get; set;}
        public Day12()
        {
            var nodes = Input.Select(s => s.Split("-")).SelectMany(t => t).Distinct();
            var edges = Input.Select(s => s.Split("-")).Select(s => new Edge<string>( s[0], s[1]));
            _graph = new Graph<string>(nodes, edges);
        }

        public int Star1() => FindPaths(_graph, "start", "end", IsAllowedStar1);
        public int Star2() => FindPaths(_graph, "start", "end", IsAllowedStar2);
        private delegate bool IsAllowedDelegate(List<string> visited, string node);

        private int FindPaths(Graph<string> graph, string start, string end, IsAllowedDelegate IsAllowed )
        {
            var path = new List<string>(){start};
            var queue = new Queue<List<string>>();
            queue.Enqueue(path);
            int paths = 0;
            while (queue.Any())
            {
                var current = queue.Dequeue();
                if(current.Last() == end)
                {
                    paths++;
                    continue;
                }
                foreach (var node in graph.Nodes[current.Last()])
                {
                    if(IsAllowed(current, node.End))
                    {
                        var newPath = new List<string>(current);
                        newPath.Add(node.End);
                        queue.Enqueue(newPath);
                    }
                }
            }
            return paths;
        }

        private bool IsAllowedStar1(List<string> visited, string node)
        {
            if(!node.All(char.IsLower))
                return true;
            return !visited.Contains(node);
        }

        private bool IsAllowedStar2(List<string> visited, string node)
        {
            if((node == "start" || node == "end") && visited.Contains(node))
                return false;
            if(node.All(char.IsLower) && visited.Count(n => n == node) >= 2)
                return false;
            if(!node.All(char.IsLower))
                return true;
            if(!visited.Contains(node))
                return true;
            return !visited.Where(l => l.All(char.IsLower)).GroupBy(g => g).Any(g => g.Count() > 1);
        }


        /*
                private int FindPaths(Graph graph, string start, string end)
                {
                    Node s = graph.GetNode(start);
                    Node e = graph.GetNode(end);
                    var visited = new HashSet<Node>();
                    var nodes = new Stack<Node>();
                    nodes.Push(s);
                    int paths = 0;
                    while (nodes.Any())
                    {
                        var current = nodes.Pop();
                        foreach (Node child in graph.GetChildren(current).Where(t => IsAllowed(t, paths)))
                        {
                            if(child == e)
                            {
                                s.VisitedPath = paths++;
                                break;
                            }
                            else
                            {
                                child.VisitedPath = paths;
                                nodes.Push(child);
                            }
                        }
                    }
                    return paths;

                }



                private bool IsAllowed(Node node, int path)
                {
                    return !(node.NodeId.All(char.IsLower) && node.VisitedPath == path && node.NodeId == "start");
                }

        */
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

        private class Graph<T> where T: notnull
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
    }
}