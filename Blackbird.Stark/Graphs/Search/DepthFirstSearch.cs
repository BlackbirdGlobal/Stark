using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Blackbird.Stark.Graphs.Search
{
    public class DepthFirstSearch<TKey, TData> : IGraphSearch<TKey, TData> where TKey : IEquatable<TKey>
    {
        private Stack<GraphNode<TKey, TData>> _stack = new Stack<GraphNode<TKey, TData>>();

        public DepthFirstSearch(GraphNode<TKey, TData> graph)
        {
            Graph = graph;
        }

        public GraphNode<TKey, TData> Graph { get; private set; }

        public SearchResult<TKey, TData> Search(TKey val, Action<GraphNode<TKey, TData>, TKey> f = null)
        {
            _stack.Clear();
            _stack.Push(Graph);

            while (_stack.Count > 0)
            {
                var vertex = _stack.Pop();
                if (vertex.Status != DiscoveryStatus.Discovered)
                {
                    f?.Invoke(vertex, val);
                    if (vertex.Value.Equals(val))
                    {
                        return new SearchResult<TKey, TData>() { Found = true, Result = vertex };
                    }
                }

                vertex.Status = DiscoveryStatus.Visited;

                foreach (var c in vertex.Children.Where(x => x.Status == DiscoveryStatus.Undiscovered))
                {
                    _stack.Push(c);
                }
            }

            return new SearchResult<TKey, TData>() { Found = false };
        }
    }
}
