using System;
using System.IO.Enumeration;
using System.Text;

namespace Stark.Trees
{
    public interface ITree<in K, V> where K:IComparable<K>
    {
        void Insert(K key, V value);

        V Find(K key);

        bool Delete(K key);

        void Clear();

        int Count { get; }
    }
}
