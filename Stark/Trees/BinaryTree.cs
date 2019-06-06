using System;
using System.Collections.Generic;

namespace Stark.Trees
{
    public class BinaryTree<K,V>: ITree<K,V> where K:IComparable<K>
    {
        private int _count = 0;
        private BinaryNode<K, V> _root;
        
        public void Insert(K key, V value)
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

        public V Find(K key)
        {
            if(_root == null)
                throw new KeyNotFoundException();
            
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
                        return result.Value;
                }
            }
            throw new KeyNotFoundException();
        }

        public bool Delete(K key)
        {
            throw new NotImplementedException();
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
    }
}