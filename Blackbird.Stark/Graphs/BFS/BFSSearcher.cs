using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackbird.Stark.Graphs.BFS
{
    class BfsSearcher<T, D> : IGraphSearcher<T, D> where T : IEquatable<T>
    {
        public GraphNode<T, D> Graph { get; set; }
        private Queue<GraphNode<T, D>> Q { get; set; }

        public BfsSearcher(GraphNode<T, D> root)
        {
            Graph = root;
            Q = new Queue<GraphNode<T, D>>();
        }

        public SearchResult<T, D> Search(T val, Action<GraphNode<T, D>, T> f)
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
                    return new SearchResult<T, D>() { Found = true, Result = node };
                }
                if (node.Children == null || node.Children.All(x => x.Status == DiscoveryStatus.Vizited))
                {
                    node.Status = DiscoveryStatus.Vizited;
                }
                else
                {
                    foreach (var c in node.Children)
                    {
                        if (c.Status != DiscoveryStatus.Vizited)
                        {
                            Q.Enqueue(c);
                        }
                    }
                }
            }
            return new SearchResult<T, D>() { Found = false };
        }
    }
}
