using System;

namespace Blackbird.Stark.Trees
{
    internal class BinaryNode<TK,TV> where TK:IComparable<TK>
    {
        public TK Key { get; set; }
        public TV Value { get; set; }
        
        public BinaryNode<TK,TV> Parent { get; set; }
        public BinaryNode<TK,TV> Left { get; set; }
        public BinaryNode<TK,TV> Right { get; set; }

        public bool IsRoot => Parent == null;
        public bool HasLeftChild => Left != null;
        public bool HasRightChild => Right != null;
        public bool HasChildren => HasLeftChild || HasRightChild;
        public bool HasBothChildren => HasLeftChild && HasRightChild;
    }
}