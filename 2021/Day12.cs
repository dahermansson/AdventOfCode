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

        private string[] Input = InputReader.GetInputLines("12test.txt");

        public int Star1()
        {
            var graph = new Graph();
            foreach (var n in Input.Select(t => new {start = t.Split("-")[0], end = t.Split("-")[1].Trim('-')}).GroupBy(t => t.start))
            {
                var node = new Node(n.Key);
                graph.AddNode(node);
                graph.AddEdges(node, n.Select(t => new Node(t.end)).ToArray());
            }
            return FindPaths(graph, "start", "end");
        }

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


        public int Star2()
        {
            throw new NotImplementedException();
        }
    }
}