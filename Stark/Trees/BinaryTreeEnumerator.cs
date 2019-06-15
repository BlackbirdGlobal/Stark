using System;
using System.Collections;
using System.Collections.Generic;

namespace Stark.Trees
{
    internal class BinaryTreeEnumerator<K, V> : IEnumerator<KeyValuePair<K, V>> where K:IComparable<K>
    {
        private BinaryNode<K,V> _tree;
        private KeyValuePair<K,V> _current;
        private Queue<BinaryNode<K,V>> _q; 

        public BinaryTreeEnumerator(BinaryNode<K,V> tree)
        {
            _tree = tree;
            _q = new Queue<BinaryNode<K, V>>();
            _q.Enqueue(tree);
        }
        public KeyValuePair<K, V> Current => _current;

        object IEnumerator.Current => _current;

        public void Dispose()
        {
            _tree = null;
        }

        public bool MoveNext()
        {
            if(_q.Count > 0){
                var node = _q.Dequeue();
                if(node.HasLeftChild)
                    _q.Enqueue(node.Left);
                if(node.HasRightChild)
                    _q.Enqueue(node.Right);
                _current = new KeyValuePair<K,V>(node.Key, node.Value);
                return true;
            }
            return false;
        }

        public void Reset()
        {
            _q.Clear();
            _q.Enqueue(_tree);
        }
    }
}