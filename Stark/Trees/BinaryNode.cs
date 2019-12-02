using System;

namespace Blackbird.Stark.Trees
{
    internal class BinaryNode<K,V> where K:IComparable<K>
    {
        public K Key { get; set; }
        public V Value { get; set; }
        
        public BinaryNode<K,V> Parent { get; set; }
        public BinaryNode<K,V> Left { get; set; }
        public BinaryNode<K,V> Right { get; set; }

        public bool IsRoot => Parent == null;
        public bool HasLeftChild => Left != null;
        public bool HasRightChild => Right != null;
        public bool HasChildren => HasLeftChild || HasRightChild;
        public bool HasBothChildren => HasLeftChild && HasRightChild;
    }
}