using System;

namespace Blackbird.Stark.Graphs.Builders
{
    public interface IGraphBuilder<in TSource, TKey, TData> where TKey : IEquatable<TKey>
    {
        GraphNode<TKey, TData> Build(TSource s);
    }
}
