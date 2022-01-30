using System;

namespace Blackbird.Stark.Trees.Nodes;

internal class RbNode<TK,TV>: BinaryNode<TK,TV> where TK:IComparable<TK>
{
    public RbColor Color { get; set; }

    public new RbNode<TK, TV> Parent
    {
        get => base.Parent as RbNode<TK, TV>; 
        set => base.Parent = value;
    }

    public new RbNode<TK, TV> Left
    {
        get => base.Left as RbNode<TK,TV>; 
        set => base.Left = value;
    }

    public new RbNode<TK, TV> Right
    {
        get => base.Right as RbNode<TK,TV>; 
        set => base.Right = value;
    }

    public RbNode(TK key, TV value)
    {
        Key = key;
        Value = value;
        Color = RbColor.Black;
    }
}