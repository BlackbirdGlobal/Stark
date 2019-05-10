using System;

namespace Stark.Graphs.Builders
{
    public interface IGraphBuilder<M, T, D> where T : IEquatable<T>
    {
        GraphNode<T, D> Build(M s);
    }
}
