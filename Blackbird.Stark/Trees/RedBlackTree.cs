using System;
using Blackbird.Stark.Trees.Nodes;

namespace Blackbird.Stark.Trees;

public class RedBlackTree<TK, TV>: ITree<TK, TV> where TK:IComparable<TK>
{
    private RbNode<TK, TV> _root;
    public void Add(TK key, TV value)
    {
        if (_root == null)
            _root = new RbNode<TK, TV>(key, value);
    }

    public TV Get(TK key)
    {
        throw new NotImplementedException();
    }

    public bool Remove(TK key)
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public int Count { get; }
    public bool ContainsKey(TK key)
    {
        throw new NotImplementedException();
    }
}