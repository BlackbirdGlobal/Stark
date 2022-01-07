using System;

namespace Blackbird.Stark.Trees.Nodes;

public enum RbColor
{
    Red,
    Black
}

public class RbNode<TK,TV> where TK:IComparable<TK>
{
    public TK Key { get; set; }
    public TV Value { get; set; }
    public RbColor Color { get; set; }
    
    public RbNode<TK, TV> Parent { get; set; }
    
    public RbNode<TK, TV> Left { get; set; }
    public RbNode<TK, TV> Right { get; set; }

    public RbNode(TK key, TV value)
    {
        Key = key;
        Value = value;
        Color = RbColor.Black;
    }
}