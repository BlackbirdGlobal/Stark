using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackbird.Stark.Graphs.BFS
{
    public class BfsSearcher<T, TD> : IGraphSearcher<T, TD> where T : IEquatable<T>
    {
        public GraphNode<T, TD> Graph { get; set; }
        private Queue<GraphNode<T, TD>> Q { get; set; }

        public BfsSearcher(GraphNode<T, TD> root)
        {
            Graph = root;
            Q = new Queue<GraphNode<T, TD>>();
        }

        public SearchResult<T, TD> Search(T val, Action<GraphNode<T, TD>, T> f)
        {
            if (Q.Count == 0)
            {
                Q.Enqueue(Graph);
            }
            while (Q.Count > 0)
            {
                var node = Q.Dequeue();
                node.Status = DiscoveryStatus.Discovered;
                f(node, val);
                if (node.Value.Equals(val))
                {
                    return new SearchResult<T, TD>() { Found = true, Result = node };
                }
                if (node.Children == null || node.Children.All(x => x.Status == DiscoveryStatus.Visited))
                {
                    node.Status = DiscoveryStatus.Visited;
                }
                else
                {
                    foreach (var c in node.Children)
                    {
                        if (c.Status != DiscoveryStatus.Visited)
                        {
                            Q.Enqueue(c);
                        }
                    }
                }
            }
            return new SearchResult<T, TD>() { Found = false };
        }
    }
}
