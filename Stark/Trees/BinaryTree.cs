using System;
using System.Collections;
using System.Collections.Generic;

namespace Stark.Trees
{
    public class BinaryTree<K,V>: ITree<K,V>, IEnumerable<KeyValuePair<K,V>> where K:IComparable<K>
    {
        private int _count = 0;
        private BinaryNode<K, V> _root;
        
        public void Add(K key, V value)
        {
            var node = new BinaryNode<K, V> {Key = key, Value = value};
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
            _count++;
        }

        public V Get(K key)
        {
            var node = GetNode(key);
            return node == null? default(V) : node.Value;
        }

        private BinaryNode<K, V> GetNode(K key)
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

        public bool Remove(K key)
        {
            var node = GetNode(key);
            if(node == null)
                return false;
            if(node.HasChildren){
                if(node.HasBothChildren){
                    var successor = FindClosestSmallerValue(node);
                    successor.Parent.Right = successor.Left;
                    if(successor.HasLeftChild)
                        successor.Left.Parent = successor.Parent;
                    successor.Left = node.Left;
                    successor.Right = node.Right;
                    successor.Parent = node.Parent;
                    node.Left.Parent = node.Right.Parent = successor;
                    
                    if(node.Parent != null)
                    {
                        node.Parent.Left = node.Parent.Left == node? successor : node.Parent.Left;
                        node.Parent.Right = node.Parent.Right == node? successor : node.Parent.Right;
                    } else {
                        _root = successor;
                    }
                } else {
                    var child = node.Left ?? node.Right;
                    child.Parent = node.Parent;
                    if(node.Parent != null){
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
            return true;
        }

        public void Clear()
        {
            _root = null;
        }

        public int Count => _count;

        private BinaryNode<K, V> FindParentToInsert(K key)
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
    
        private BinaryNode<K, V> FindClosestSmallerValue(BinaryNode<K,V> node){
            var tmp = node.Left;
            while(tmp.Right != null){
                tmp = tmp.Right;
            }
            return tmp;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return new BinaryTreeEnumerator<K,V>(_root);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new BinaryTreeEnumerator<K,V>(_root);
        }

        public bool ContainsKey(K key)
        {
            var node = this.GetNode(key);
            return node != null;
        }
    }
}