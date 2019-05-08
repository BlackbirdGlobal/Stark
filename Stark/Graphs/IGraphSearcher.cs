using System;

namespace Stark.Graphs
{
    interface IGraphSearcher<T, D> where T : IEquatable<T>
    {
        GraphNode<T, D> Graph { get; set; }

        SearchResult<T, D> Search(T val, Action<GraphNode<T, D>, T> f);
    }
}
