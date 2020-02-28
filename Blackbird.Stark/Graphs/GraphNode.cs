using System;
using System.Collections.Generic;

namespace Blackbird.Stark.Graphs
{
    public class GraphNode<T, TD> where T : IEquatable<T>
    {
        public T Value { get; set; }
        public TD AdditionalData { get; set; }
        public DiscoveryStatus Status { get; set; }
        public GraphNode<T, TD> Parent { get; set; }
        public List<GraphNode<T, TD>> Children { get; set; } = new List<GraphNode<T, TD>>();
    }
}
