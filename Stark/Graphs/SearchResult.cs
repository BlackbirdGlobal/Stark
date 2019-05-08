using Stark.Graphs;
using System;

namespace Stark.Graphs
{
    public struct SearchResult<T, D> where T : IEquatable<T>
    {
        public bool Found { get; set; }

        public GraphNode<T, D> Result { get; set; }
    }
}
