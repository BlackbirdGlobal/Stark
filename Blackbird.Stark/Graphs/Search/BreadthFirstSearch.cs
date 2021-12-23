using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackbird.Stark.Graphs.Search
{
    public class BreadthFirstSearch<TKey, TData> : IGraphSearch<TKey, TData> where TKey : IEquatable<TKey>
    {
        public GraphNode<TKey, TData> Graph { get; }
        private readonly Queue<GraphNode<TKey, TData>> _queue = new Queue<GraphNode<TKey, TData>>();
        public event OnNodeDiscoveredDelegate<TKey, TData> OnNodeDiscovered; 
        public BreadthFirstSearch(GraphNode<TKey, TData> root)
        {
            Graph = root;
        }
        public SearchResult<TKey, TData> Search(TKey val)
        {
            _queue.Clear();
            _queue.Enqueue(Graph);
            while (_queue.Count > 0)
            {
                var node = _queue.Dequeue();
                node.Status = DiscoveryStatus.Discovered;

                OnNodeDiscovered?.Invoke(node, val);

                if (node.Value.Equals(val))
                {
                    return new SearchResult<TKey, TData>() { Found = true, Result = node };
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
                            _queue.Enqueue(c);
                        }
                    }
                }
            }
            return new SearchResult<TKey, TData>() { Found = false };
        }
    }
}
