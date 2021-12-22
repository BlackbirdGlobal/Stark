using System;

namespace Blackbird.Stark.Graphs.Search
{
    public struct SearchResult<TKey, TData> where TKey : IEquatable<TKey>
    {
        public bool Found { get; set; }

        public GraphNode<TKey, TData> Result { get; set; }
    }
}
