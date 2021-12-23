using System;

namespace Blackbird.Stark.Graphs.Search
{
    public delegate void OnNodeDiscoveredDelegate<TKey, TData>(GraphNode<TKey, TData> node, TKey val) where TKey:IEquatable<TKey>;
}