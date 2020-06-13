using System;
using System.Collections.Generic;

namespace Blackbird.Stark.Trees
{
    public class AvlTree<TK, TV> : ITree<TK, TV> where TK : IComparable<TK>
    {
        internal AvlNode<TK, TV> _root;

        public void Add(TK key, TV value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var node = new AvlNode<TK, TV>(key, value);
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

                //node.RefreshHeight();
                while (parent != null)
                {
                    //balance tree
                    parent.RefreshHeight();
                    var balance = parent.Balance;

                    switch (balance)
                    {
                        case int b when b > 1 && node.Key.CompareTo(parent.Left.Key) == -1:
                            parent = RightRotate(parent);
                            break;
                        case int b when b > 1 && node.Key.CompareTo(parent.Left.Key) == 1:
                            parent.Left = LeftRotate(parent.Left);
                            parent = RightRotate(parent);
                            break;
                        case int b when b < -1 && node.Key.CompareTo(parent.Right.Key) == 1:
                            parent = LeftRotate(parent);
                            break;
                        case int b when b < -1 && node.Key.CompareTo(parent.Right.Key) == -1:
                            parent.Right = RightRotate(parent.Right);
                            parent = LeftRotate(parent);
                            break;
                    }

                    _root = parent.IsRoot ? parent : _root;

                    parent = parent.Parent;
                }
            }
        }

        private AvlNode<TK, TV> LeftRotate(AvlNode<TK, TV> x)
        {
            var y = x.Right;
            var t2 = y.Left;

            if (!x.IsRoot)
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
            if(t2 != null)
                t2.Parent = x;

            // Update heights  
            x.RefreshHeight();
            y.RefreshHeight();

            // Return new root  
            return y;
        }

        private AvlNode<TK, TV> RightRotate(AvlNode<TK, TV> y)
        {
            var x = y.Left;
            var t2 = x.Right;

            if (!y.IsRoot)
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
            if(t2 != null)
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
            var node = GetNode(key);
            return node != null ? node.Value : default;
        }

        private AvlNode<TK, TV> GetNode(TK key)
        {
            var it = _root;
            while (it != null)
            {
                switch (it.Key.CompareTo(key))
                {
                    case 0:
                        return it;
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
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var node = GetNode(key);
            if (node == null)
                return false;
            if (node.HasChildren)
            {
                if (node.HasBothChildren)
                {
                    var successor = FindClosestSmallerValue(node);
                    if (!successor.Parent.IsRoot)
                        successor.Parent.Right = successor.Left;
                    if (successor.HasLeftChild)
                        successor.Left.Parent = successor.Parent;
                    successor.Left = node.Left == successor ? null : node.Left;
                    successor.Right = node.Right;
                    successor.Parent = node.Parent;
                    if (successor.HasRightChild) successor.Right.Parent = successor;
                    if (successor.HasLeftChild) successor.Left.Parent = successor;

                    if (node.Parent != null)
                    {
                        node.Parent.Left = node.Parent.Left == node ? successor : node.Parent.Left;
                        node.Parent.Right = node.Parent.Right == node ? successor : node.Parent.Right;
                    }
                    else
                    {
                        _root = successor;
                    }
                }
                else
                {
                    var child = node.Left ?? node.Right;
                    child.Parent = node.Parent;
                    if (node.Parent != null)
                    {
                        node.Parent.Left = node.Parent.Left == node ? child : node.Parent.Left;
                        node.Parent.Right = node.Parent.Right == node ? child : node.Parent.Right;
                        node.Parent = node.Left = node.Right = null;
                    }
                    else
                    {
                        node.Left = node.Right = null;
                        _root = child;
                    }
                }
            }
            else
            {
                if (node.Parent != null)
                {
                    node.Parent.Left = node.Parent.Left == node ? null : node.Parent.Left;
                    node.Parent.Right = node.Parent.Right == node ? null : node.Parent.Right;
                    node.Parent = null;
                }
                else
                {
                    _root = null;
                }
            }
            Count--;
            if (_root == null)
                return true;
            
            var balance = _root.Balance;
            
            // If this node becomes unbalanced,  
            // then there are 4 cases  
            // Left Left Case  
            if (balance > 1 && _root.Left.Balance >= 0)  
                _root = RightRotate(_root);  
  
            // Left Right Case  
            if (balance > 1 && _root.Left.Balance < 0)  
            {  
                _root.Left = LeftRotate(_root.Left);  
                _root = RightRotate(_root);  
            }  
  
            // Right Right Case  
            if (balance < -1 && _root.Right.Balance <= 0)  
                _root = LeftRotate(_root);  
  
            // Right Left Case  
            if (balance < -1 && _root.Right.Balance > 0)  
            {  
                _root.Right = RightRotate(_root.Right);  
                _root = LeftRotate(_root);  
            }  
            
            return true;
        }

        private static AvlNode<TK, TV> FindClosestSmallerValue(AvlNode<TK, TV> node)
        {
            var tmp = node.Left;
            while (tmp.Right != null)
            {
                tmp = tmp.Right;
            }

            return tmp;
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