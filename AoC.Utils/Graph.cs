using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AoC.Utils
{

/*
    public record Node2
    {
        public string NodeId { get; set; }
        public int Cost { get; set; }
        public int VisitedPath {get; set;} = -1;
        public Node2(string nodeId, int cost = 0)
        {
            NodeId = nodeId;
            Cost = cost;
        }
    }

    public class Graph2
    {
        public Dictionary<Node, HashSet<Node>> Nodes;
        public Graph2()
        {
            Nodes = new Dictionary<Node, HashSet<Node>>();
        }

        public void AddNode(Node node)
        {
            if(!Nodes.ContainsKey(node))
                Nodes.Add(node, new HashSet<Node>());
        }

        public void AddEdges(Node node, IEnumerable<Node> edges)
        {
            foreach (var edge in edges)
            {
                Nodes[node].Add(edge);
                if(!Nodes.ContainsKey(edge))
                    Nodes.Add(edge, new HashSet<Node>());
                Nodes[edge].Add(node);            
            }
        }

        public IEnumerable<Node> GetChildren(Node node)
        {
            foreach (var edge in Nodes[node])
                yield return edge;            
        }

        public Node GetNode(string nodeid)
        {
            return Nodes.Single(t => t.Key == new Node(nodeid)).Key;
        }



    }
    */
}