using System;
using System.IO.Enumeration;
using System.Text;

namespace Stark.Trees
{
    public interface ITree<in K, V> where K:IComparable<K>
    {
        void Add(K key, V value);

        V Get(K key);

        bool Remove(K key);

        void Clear();

        int Count { get; }
    }
}
