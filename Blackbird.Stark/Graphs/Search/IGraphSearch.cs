using System;

namespace Blackbird.Stark.Graphs.Search
{
    public interface IGraphSearch<TKey, TData> where TKey : IEquatable<TKey>
    {
        GraphNode<TKey, TData> Graph { get; }

        SearchResult<TKey, TData> Search(TKey val);
    }
}
