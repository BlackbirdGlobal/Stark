using System;

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
            
            _count++;
        }

        public V Find(K key)
        {
            throw new NotImplementedException();
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
    }
}