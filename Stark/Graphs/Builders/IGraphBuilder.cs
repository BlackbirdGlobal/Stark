using Stark.Graphs;
using System;

namespace Sbx
{
    public interface IGraphBuilder<M,T, D> where T: IEquatable<T>
    {
        GraphNode<T, D> Build(M s);
    }
}
