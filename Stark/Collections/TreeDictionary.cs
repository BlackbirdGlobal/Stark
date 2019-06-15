using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Stark.Trees;

namespace Stark.Collections
{
    public class TreeDictionary<K, V> : IDictionary<K, V> where K:IComparable<K>
    {
        private readonly ITree<K, V> _tree;
        private IEnumerable<KeyValuePair<K, V>> Collection => _tree as IEnumerable<KeyValuePair<K, V>>;

        public TreeDictionary(ITree<K, V> tree)
        {
            _tree = tree;
        }

        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            return Collection?.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<K, V> item)
        {
            _tree.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _tree.Clear();
        }

        public bool Contains(KeyValuePair<K, V> item)
        {
            return _tree.ContainsKey(item.Key) && _tree.Get(item.Key).Equals(item.Value);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            if (Collection == null)
                return;
            var i = arrayIndex;
            foreach (var p in Collection)
            {
                array[i++] = p;
            }
        }

        public bool Remove(KeyValuePair<K, V> item)
        {
            return _tree.Remove(item.Key);
        }

        public int Count => _tree.Count;
        public bool IsReadOnly => false;
        public void Add(K key, V value)
        {
            _tree.Add(key, value);
        }

        public bool ContainsKey(K key)
        {
            return _tree.ContainsKey(key);
        }

        public bool Remove(K key)
        {
            return _tree.Remove(key);
        }

        public bool TryGetValue(K key, out V value)
        {
            value = _tree.Get(key);
            return value != null;
        }

        public V this[K key]
        {
            get => _tree.Get(key);
            set { _tree.Remove(key); _tree.Add(key, value); }
        }

        public ICollection<K> Keys => Collection?.Select(x => x.Key).ToArray();
        public ICollection<V> Values => Collection?.Select(x => x.Value).ToArray();
    }
}