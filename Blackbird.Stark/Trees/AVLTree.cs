using System;

namespace Blackbird.Stark.Trees
{
    public class AVLTree<TK,TV>: ITree<TK,TV> where TK : IComparable<TK>
    {
        private AVLNode<TK, TV> _root;
        
        public void Add(TK key, TV value)
        {
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var node = new AVLNode<TK,TV>(key,value);
            if (_root == null)
            {
                _root = node;
            }
            else
            {
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
            }
        }
        
        private AVLNode<TK, TV> FindParentToInsert(TK key)
        {
            if (_root == null)
                return null;
            var result = _root;
            while (result.HasChildren)
            {
                switch (key.CompareTo(result.Key))
                {
                    case int cmp when cmp == -1 && result.HasLeftChild:
                        result = (AVLNode<TK,TV>)result.Left;
                        break;
                    case int cmp when cmp == 1 && result.HasRightChild:
                        result = (AVLNode<TK,TV>)result.Right;
                        break;
                    default:
                        return result;
                }
            }
            return result;
        }

        public TV Get(TK key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TK key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool ContainsKey(TK key)
        {
            throw new NotImplementedException();
        }
    }
}