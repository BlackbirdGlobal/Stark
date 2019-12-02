using System;
using System.Collections.Generic;

namespace Blackbird.Stark.Graphs
{
    public class GraphNode<T, D> where T : IEquatable<T>
    {
        public T Value { get; set; }
        public D AdditionalData { get; set; }
        public DiscoveryStatus Status { get; set; }
        public GraphNode<T, D> Parent { get; set; }
        public List<GraphNode<T, D>> Children { get; set; } = new List<GraphNode<T, D>>();
    }
}
