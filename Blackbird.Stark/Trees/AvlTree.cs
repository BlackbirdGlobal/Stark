using System;
using System.Collections;
using System.Collections.Generic;
using Blackbird.Stark.Trees.Enumerators;
using Blackbird.Stark.Trees.Nodes;

namespace Blackbird.Stark.Trees
{
    public sealed class AvlTree<TK, TV> : ITree<TK, TV>, IEnumerable<KeyValuePair<TK,TV>> where TK : IComparable<TK>
    {
        internal AvlNode<TK, TV> _root;
        private readonly object _lock = new object();

        public void Add(TK key, TV value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            lock (_lock)
            {
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
                    if (node.Key.CompareTo(parent.Key) < 0)
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

                        if (balance > 1 && node.Key.CompareTo(parent.Left.Key) < 0)
                        {
                            parent = RightRotate(parent);
                        }
                        else if (balance > 1 && node.Key.CompareTo(parent.Left.Key) > 0)
                        {
                            parent.Left = LeftRotate(parent.Left);
                            parent = RightRotate(parent);
                        }
                        else if (balance < -1 && node.Key.CompareTo(parent.Right.Key) > 0)
                        {
                            parent = LeftRotate(parent);
                        }
                        else if (balance < -1 && node.Key.CompareTo(parent.Right.Key) < 0)
                        {
                            parent.Right = RightRotate(parent.Right);
                            parent = LeftRotate(parent);
                        }

                        _root = parent.IsRoot ? parent : _root;

                        parent = parent.Parent;
                    }
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
                var cmp = key.CompareTo(result.Key);
                if (cmp < 0 && result.HasLeftChild)
                    result = result.Left;
                else if (cmp > 0 && result.HasRightChild)
                    result = result.Right;
                else
                    return result;
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
                var cmp = it.Key.CompareTo(key);
                if (cmp == 0)
                    return it;
                it = cmp > 0 ? it.Left : it.Right;
            }

            return default;
        }

        public bool Remove(TK key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            lock (_lock)
            {
                var node = GetNode(key);
                if (node == null)
                    return false;

                var rebalanceFrom = DetachNodeAndReturnParent(node);
                Count--;
                RebalanceUpward(rebalanceFrom);
                return true;
            }
        }

        // Standard BST deletion. When the target has two children it adopts
        // the in-order successor's key/value and then removes that successor,
        // which by construction has at most one child. Returns the parent
        // where height/balance fixup needs to start (or null if the tree
        // just became empty).
        private AvlNode<TK, TV> DetachNodeAndReturnParent(AvlNode<TK, TV> node)
        {
            if (node.HasBothChildren)
            {
                var successor = MinOf(node.Right);
                node.Key = successor.Key;
                node.Value = successor.Value;
                return DetachNodeAndReturnParent(successor);
            }

            var child = node.Left ?? node.Right;
            var parent = node.Parent;

            if (child != null)
                child.Parent = parent;

            if (parent == null)
            {
                _root = child;
            }
            else if (parent.Left == node)
            {
                parent.Left = child;
            }
            else
            {
                parent.Right = child;
            }

            return parent;
        }

        // Walk from the given node up to the root, refreshing heights and
        // rotating whenever the AVL invariant is violated.
        private void RebalanceUpward(AvlNode<TK, TV> node)
        {
            while (node != null)
            {
                node.RefreshHeight();
                var balance = node.Balance;
                var rotated = node;

                if (balance > 1)
                {
                    if (node.Left.Balance < 0)
                        node.Left = LeftRotate(node.Left);
                    rotated = RightRotate(node);
                }
                else if (balance < -1)
                {
                    if (node.Right.Balance > 0)
                        node.Right = RightRotate(node.Right);
                    rotated = LeftRotate(node);
                }

                if (rotated.Parent == null)
                    _root = rotated;

                node = rotated.Parent;
            }
        }

        private static AvlNode<TK, TV> MinOf(AvlNode<TK, TV> node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        public void Clear()
        {
            lock (_lock)
            {
                _root = null;
                Count = 0;
            }
        }

        public int Count { get; private set; } = 0;

        public bool ContainsKey(TK key)
        {
            var it = _root;
            while (it != null)
            {
                var cmp = it.Key.CompareTo(key);
                if (cmp == 0)
                    return true;
                it = cmp > 0 ? it.Left : it.Right;
            }

            return false;
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
}