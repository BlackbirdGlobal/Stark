using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using Blackbird.Stark.Trees.Enumerators;
using Blackbird.Stark.Trees.Nodes;

namespace Blackbird.Stark.Trees;

public sealed class RbTree<TK, TV> : ITree<TK, TV>, IEnumerable<KeyValuePair<TK,TV>> where TK:IComparable<TK>
{
    internal RbNode<TK, TV> _root;

    public void Add(TK key, TV value)
    {
        var node = new RbNode<TK, TV>(key, value);
        Insert(node);
        Count++;
    }

    public TV Get(TK key)
    {
        var node = Find(key);
        return node.Value;
    }

    public bool Remove(TK key)
    {
        try
        {
            var node = Find(key);
            Delete(node);
            Count--;
            return true;
        }
        catch
        {
            return false;
        }
    }

    public void Clear()
    {
        _root = null;
    }

    public int Count { get; private set; }

    public bool ContainsKey(TK key)
    {
        var x = _root;
        while (x != null)
        {
            var cmp = key.CompareTo(x.Key);
            switch (cmp)
            {
                case 0:
                    return true;
                case 1:
                    x = x.Right;
                    break;
                case -1:
                    x = x.Left;
                    break;
            }
        }

        return false;
    }

    private void LeftRotate(RbNode<TK, TV> x)
    {
        var y = x.Right;
        x.Right = y.Left;

        if (y.Left != null)
            y.Left.Parent = x;
        y.Parent = x.Parent;
        if (x.Parent == null)
            _root = y;
        else
        {
            if (x == x.Parent.Left)
                x.Parent.Left = y;
            else
                x.Parent.Right = y;
        }

        y.Left = x;
        x.Parent = y;
    }

    private void RightRotate(RbNode<TK, TV> x)
    {
        var y = x.Left;
        x.Left = y.Right;

        if (y.Right != null)
            y.Right.Parent = x;
        y.Parent = x.Parent;
        if (x.Parent == null)
            _root = y;
        else
        {
            if (x == x.Parent.Right)
                x.Parent.Right = y;
            else
                x.Parent.Left = y;
        }

        y.Right = x;
        x.Parent = y;
    }

    private void Insert(RbNode<TK, TV> z)
    {
        var y = default(RbNode<TK, TV>);
        var x = _root;
        while (x != null)
        {
            y = x;
            //z.Key < x.Key?
            x = z.Key.CompareTo(x.Key) == -1 ? x.Left : x.Right;
        }

        z.Parent = y;
        if (y == null)
        {
            _root = z;
        }
        else
        {
            //z.Key < y.Key
            if (z.Key.CompareTo(y.Key) == -1)
                y.Left = z;
            else
                y.Right = z;
        }

        z.Left = null;
        z.Right = null;
        z.Color = RbColor.Red;
        RbInsertFixup(z);
    }

    private void RbInsertFixup(RbNode<TK, TV> z)
    {
        while (z.Parent is { Color: RbColor.Red })
        {
            if (z.Parent == z.Parent.Parent.Left)
            {
                var y = z.Parent.Parent.Right;
                if (y is { Color: RbColor.Red })
                {
                    z.Parent.Color = RbColor.Black;
                    y.Color = RbColor.Black;
                    z.Parent.Parent.Color = RbColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Right)
                    {
                        z = z.Parent;
                        LeftRotate(z);
                    }

                    z.Parent.Color = RbColor.Black;
                    z.Parent.Parent.Color = RbColor.Red;
                    RightRotate(z.Parent.Parent);
                }
            }
            else
            {
                var y = z.Parent.Parent.Left;
                if (y is { Color: RbColor.Red })
                {
                    z.Parent.Color = RbColor.Black;
                    y.Color = RbColor.Black;
                    z.Parent.Parent.Color = RbColor.Red;
                    z = z.Parent.Parent;
                }
                else
                {
                    if (z == z.Parent.Left)
                    {
                        z = z.Parent;
                        RightRotate(z);
                    }

                    z.Parent.Color = RbColor.Black;
                    z.Parent.Parent.Color = RbColor.Red;
                    LeftRotate(z.Parent.Parent);
                }
            }
        }
        _root.Color = RbColor.Black;
    }

    private void Transplant(RbNode<TK, TV> u, RbNode<TK, TV> v)
    {
        if (u.Parent == null)
            _root = v;
        else if (u == u.Parent.Left)
            u.Parent.Left = v;
        else
            u.Parent.Right = v;
        if(v != null)
            v.Parent = u.Parent;
    }

    private void Delete(RbNode<TK, TV> z)
    {
        var y = z;
        var yOriginalColor = y.Color;
        var x = default(RbNode<TK, TV>);
        if (z.Left == null)
        {
            x = z.Right;
            Transplant(z, z.Right);
        } else if (z.Right == null)
        {
            x = z.Left;
            Transplant(z, z.Left);
        }
        else
        {
            y = Min(z.Right);
            yOriginalColor = y.Color;
            x = y.Right;
            if (y.Parent == z)
            {
                x.Parent = y;
            }
            else
            {
                Transplant(y, y.Right);
                y.Right = z.Right;
                y.Right.Parent = y;
            }
            Transplant(z, y);
            y.Left = z.Left;
            y.Left.Parent = y;
            y.Color = z.Color;
        }

        if (yOriginalColor == RbColor.Black)
            RbDeleteFixup(x);
    }

    private void RbDeleteFixup(RbNode<TK, TV> x)
    {
        while (x != _root && x?.Color == RbColor.Black)
        {
            if (x == x.Parent.Left)
            {
                var w = x.Parent.Right;
                if (w.Color == RbColor.Red)
                {
                    w.Color = RbColor.Black;
                    x.Parent.Color = RbColor.Red;
                    LeftRotate(x.Parent);
                    w = x.Parent.Right;
                }

                if (w.Left.Color == RbColor.Black && w.Right.Color == RbColor.Black)
                {
                    w.Color = RbColor.Red;
                    x = x.Parent;
                }
                else
                {
                    if (w.Right.Color == RbColor.Black)
                    {
                        w.Left.Color = RbColor.Black;
                        w.Color = RbColor.Red;
                        RightRotate(w);
                        w = x.Parent.Right;
                    }

                    w.Color = x.Parent.Color;
                    x.Parent.Color = RbColor.Black;
                    w.Right.Color = RbColor.Black;
                    LeftRotate(x.Parent);
                    x = _root;
                }
            }
            else
            {
                var w = x.Parent.Left;
                if (w.Color == RbColor.Red)
                {
                    w.Color = RbColor.Black;
                    x.Parent.Color = RbColor.Red;
                    RightRotate(x.Parent);
                    w = x.Parent.Left;
                }

                if (w.Right.Color == RbColor.Black && w.Left.Color == RbColor.Black)
                {
                    w.Color = RbColor.Red;
                    x = x.Parent;
                }
                else
                {
                    if (w.Left.Color == RbColor.Black)
                    {
                        w.Right.Color = RbColor.Black;
                        w.Color = RbColor.Red;
                        LeftRotate(w);
                        w = x.Parent.Left;
                    }

                    w.Color = x.Parent.Color;
                    x.Parent.Color = RbColor.Black;
                    w.Left.Color = RbColor.Black;
                    RightRotate(x.Parent);
                    x = _root;
                }
            }
        }

        if (x != null) x.Color = RbColor.Black;
    }

    private RbNode<TK, TV> Min(RbNode<TK, TV> node)
    {
        var x = node;
        while (x.Left != null)
            x = x.Left;
        return x;
    }
    
    private RbNode<TK, TV> Max(RbNode<TK, TV> node)
    {
        var x = node;
        while (x.Right != null)
            x = x.Right;
        return x;
    }

    private RbNode<TK, TV> Find(TK key)
    {
        var x = _root;
        while (x != null)
        {
            var cmp = key.CompareTo(x.Key);
            switch (cmp)
            {
                case 0:
                    return x;
                case 1:
                    x = x.Right;
                    break;
                case -1:
                    x = x.Left;
                    break;
            }
        }

        throw new KeyNotFoundException();
    }

    public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
    {
        return new BinaryTreeEnumerator<TK, TV>(_root);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new BinaryTreeEnumerator<TK,TV>(_root);
    }
}