using System;

namespace Blackbird.Stark.Graphs
{
    public interface IGraphSearcher<T, TD> where T : IEquatable<T>
    {
        GraphNode<T, TD> Graph { get; set; }

        SearchResult<T, TD> Search(T val, Action<GraphNode<T, TD>, T> f);
    }
}
