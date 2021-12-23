using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackbird.Stark.Graphs.Search
{
    public class BreadthFirstSearch<TKey, TData> : IGraphSearch<TKey, TData> where TKey : IEquatable<TKey>
    {
        public event OnNodeDiscoveredDelegate<TKey, TData> OnNodeDiscovery; 
        public GraphNode<TKey, TData> Graph { get; set; }
        private Queue<GraphNode<TKey, TData>> _queue { get; set; }

        public BreadthFirstSearch(GraphNode<TKey, TData> root)
        {
            Graph = root;
            _queue = new Queue<GraphNode<TKey, TData>>();
        }

        public SearchResult<TKey, TData> Search(TKey val)
        {
            _queue.Clear();
            _queue.Enqueue(Graph);
            while (_queue.Count > 0)
            {
                var node = _queue.Dequeue();
                node.Status = DiscoveryStatus.Discovered;

                OnNodeDiscovery?.Invoke(node, val);

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
