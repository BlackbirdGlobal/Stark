using System;

namespace Blackbird.Stark.Trees
{
    class AVLNode<TK,TV>: BinaryNode<TK,TV> where TK:IComparable<TK>
    {
        private int Height { get; set; }
        
        public int GetHeight()
        {
            var result = 1;
            if (!IsRoot)
            {
                var parent = (AVLNode<TK, TV>) Parent;
                result = parent.Height + 1;
            }
            return Height = result;
        }

        public AVLNode(TK key, TV val)
        {
            Key = key;
            Value = val;
        }
    }
}