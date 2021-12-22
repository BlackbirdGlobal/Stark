using System;

namespace Blackbird.Stark.Graphs.Search
{
    public struct SearchResult<T, TD> where T : IEquatable<T>
    {
        public bool Found { get; set; }

        public GraphNode<T, TD> Result { get; set; }
    }
}
