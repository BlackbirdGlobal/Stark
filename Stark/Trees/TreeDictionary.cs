using System;
using System.Collections;
using System.Collections.Generic;

namespace Stark.Trees
{
    class TreeDictionary<K, V> : IDictionary<K, V> where K:IComparable<K>
    {
        private readonly ITree<K, V> _tree;

        public TreeDictionary(ITree<K, V> tree)
        {
            _tree = tree;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            _tree.Insert(item.Key, item.Value);
        }

        public void Clear()
        {
            _tree.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return _tree.Find(item.Key) != null;
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return _tree.Delete(item.Key);
        }

        public int Count => _tree.Count;
        public bool IsReadOnly => false;
        public void Add(K key, V value)
        {
            _tree.Insert(key, value);
        }

        public bool ContainsKey(K key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(K key)
        {
            return _tree.Delete(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            value = _tree.Find(key);
            return value != null;
        }

        public V this[K key]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public ICollection<K> Keys { get; }
        public ICollection<V> Values { get; }
    }
}