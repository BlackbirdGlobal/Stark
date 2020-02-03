using System;
using System.Collections.Generic;

namespace Blackbird.Stark.Trees
{
    public class AvlTree<TK,TV>: ITree<TK,TV> where TK : IComparable<TK>
    {
        private AvlNode<TK, TV> _root;

        public void Add(TK key, TV value)
        {
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var node = new AvlNode<TK,TV>(key,value);
            if (_root == null)
            {
                _root = node;
                Count = 1;
            }
            else
            {
                var parent = FindParentToInsert(node.Key);
                node.Parent = parent;
                if (node.Key.CompareTo(parent.Key) == -1)
                {
                    parent.Left = node;
                }
                else
                {
                    parent.Right = node;
                }
                Count++;
                var balance = parent.Balance;

                switch (balance)
                {
                    case int b when b > 1 && node.Key.CompareTo(parent.Left.Key) == -1:
                        RightRotate(parent);
                        break;
                    case int b when b > 1 && node.Key.CompareTo(parent.Left.Key) == 1:
                        parent.Left = LeftRotate(parent.Left);
                        RightRotate(parent);
                        break;
                    case int b when b < -1 && node.Key.CompareTo(parent.Right.Key) == 1:
                        LeftRotate(parent);
                        break;
                    case int b when b < -1 && node.Key.CompareTo(parent.Right.Key) == -1:
                        parent.Right = RightRotate(parent.Right);
                        LeftRotate(parent);
                        break;
                    case int b when b < -1 && node.Key.CompareTo(node.Right.Key) == 1:
                        LeftRotate(parent);
                        break;
                }
            }
        }

        private AvlNode<TK, TV> LeftRotate(AvlNode<TK, TV> x)
        {
            var y = x.Right;  
            var t2 = y.Left;

            if (x.IsRoot)
                _root = y;
            else
            {
                if (x.Parent.Left == x)
                    x.Parent.Left = y;
                else
                    x.Parent.Right = y;
            }
            // Perform rotation  
            y.Left = x;
            y.Parent = x.Parent;
            
            x.Parent = y;
            x.Right = t2;
            t2.Parent = x;
            
            // Update heights  
            x.RefreshHeight();
            y.RefreshHeight();
  
            // Return new root  
            return y;
        }

        private AvlNode<TK,TV> RightRotate(AvlNode<TK, TV> y)
        {
            var x = y.Left;  
            var t2 = x.Right;

            if (y.IsRoot)
            {
                _root = x;
            }
            else
            {
                if (y.Parent.Left == y)
                {
                    y.Parent.Left = x;
                }
                else
                {
                    y.Parent.Right = x;
                }
            }
            // Perform rotation  
            x.Right = y;
            x.Parent = y.Parent;

            y.Parent = x;
            y.Left = t2;
            t2.Parent = y;
  
            // Update heights  
            y.RefreshHeight();  
            x.RefreshHeight();  
  
            // Return new root  
            return x; 
        }

        private AvlNode<TK, TV> FindParentToInsert(TK key)
        {
            if (_root == null)
                return null;
            var result = _root;
            while (result.HasChildren)
            {
                switch (key.CompareTo(result.Key))
                {
                    case int cmp when cmp == -1 && result.HasLeftChild:
                        result = result.Left;
                        break;
                    case int cmp when cmp == 1 && result.HasRightChild:
                        result = result.Right;
                        break;
                    default:
                        return result;
                }
            }
            return result;
        }

        public TV Get(TK key)
        {
            var it = _root;
            while (it != null)
            {
                switch (it.Key.CompareTo(key))
                {
                    case 0:
                        return it.Value;
                    case 1:
                        it = it.Left;
                        break;
                    case -1:
                        it = it.Right;
                        break;
                }
            }
            return default;
        }

        public bool Remove(TK key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            _root = null;
        }

        public int Count { get; private set; } = 0;

        public bool ContainsKey(TK key)
        {
            var it = _root;
            while (it != null)
            {
                switch (it.Key.CompareTo(key))
                {
                    case 0:
                        return true;
                    case 1:
                        it = it.Left;
                        break;
                    case -1:
                        it = it.Right;
                        break;
                }
            }

            return false;
        }
    }
}