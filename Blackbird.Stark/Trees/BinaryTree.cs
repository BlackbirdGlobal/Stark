using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Blackbird.Stark.UnitTests")]
namespace Blackbird.Stark.Trees
{
    public class BinaryTree<TK,TV>: ITree<TK,TV>, IEnumerable<KeyValuePair<TK,TV>> where TK:IComparable<TK>
    {
        internal BinaryNode<TK, TV> _root;
        
        public void Add(TK key, TV value)
        {
            var node = new BinaryNode<TK, TV> {Key = key, Value = value};
            Count++;
            if (_root == null)
            {
                _root = node;
                return;
            }

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
        }

        public TV Get(TK key)
        {
            var node = GetNode(key);
            return node != null? node.Value : default;
        }

        private BinaryNode<TK, TV> GetNode(TK key)
        {
            if (_root == null)
                return null;

            var result = _root;

            while (result.HasChildren || result.Key.CompareTo(key) == 0)
            {
                switch (key.CompareTo(result.Key))
                {
                    case int cmp when cmp == -1 && result.HasLeftChild:
                        result = result.Left;
                        break;
                    case int cmp when cmp == 1 && result.HasRightChild:
                        result = result.Right;
                        break;
                    case int cmp when cmp == 0:
                        return result;
                }
            }
            return null;
        }

        public bool Remove(TK key)
        {
            var node = GetNode(key);
            if(node == null)
                return false;
            if(node.HasChildren)
            {
                if(node.HasBothChildren)
                {
                    var successor = FindClosestSmallerValue(node);
                    if(!successor.Parent.IsRoot)
                        successor.Parent.Right = successor.Left;
                    if(successor.HasLeftChild)
                        successor.Left.Parent = successor.Parent;
                    successor.Left = node.Left == successor? null: node.Left;
                    successor.Right = node.Right;
                    successor.Parent = node.Parent;
                    if(successor.HasRightChild) successor.Right.Parent = successor;
                    if(successor.HasLeftChild) successor.Left.Parent = successor;

                    if (node.Parent != null)
                    {
                        node.Parent.Left = node.Parent.Left == node? successor : node.Parent.Left;
                        node.Parent.Right = node.Parent.Right == node? successor : node.Parent.Right;
                    } else {
                        _root = successor;
                    }
                } else {
                    var child = node.Left ?? node.Right;
                    child.Parent = node.Parent;
                    if(node.Parent != null)
                    {
                        node.Parent.Left = node.Parent.Left == node ? child : node.Parent.Left;
                        node.Parent.Right = node.Parent.Right == node ? child : node.Parent.Right;
                        node.Parent = node.Left = node.Right = null;
                    } else {
                        node.Left = node.Right = null;
                        _root = child;
                    }
                }
            } else {
                if(node.Parent != null){
                    node.Parent.Left = node.Parent.Left == node ? null : node.Parent.Left;
                    node.Parent.Right = node.Parent.Right == node ? null : node.Parent.Right;
                    node.Parent = null;
                } else {
                    _root = null;
                }
            }
            Count--;
            return true;
        }

        public void Clear()
        {
            _root = null;
        }

        public int Count { get; private set; } = 0;

        private BinaryNode<TK, TV> FindParentToInsert(TK key)
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
    
        private static BinaryNode<TK, TV> FindClosestSmallerValue(BinaryNode<TK,TV> node)
        {
            var tmp = node.Left;
            while(tmp.Right != null){
                tmp = tmp.Right;
            }
            return tmp;
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator()
        {
            return new BinaryTreeEnumerator<TK,TV>(_root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BinaryTreeEnumerator<TK,TV>(_root);
        }

        public bool ContainsKey(TK key)
        {
            var node = GetNode(key);
            return node != null;
        }
    }
}