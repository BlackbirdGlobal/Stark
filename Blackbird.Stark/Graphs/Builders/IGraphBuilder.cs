using System;

namespace Blackbird.Stark.Graphs.Builders
{
    public interface IGraphBuilder<M, T, TD> where T : IEquatable<T>
    {
        GraphNode<T, TD> Build(M s);
    }
}
