using System;

namespace Blackbird.Stark.Trees
{
    public class AvlTree<TK,TV>: ITree<TK,TV> where TK : IComparable<TK>
    {
        private AvlNode<TK, TV> _root;

        public void Add(TK key, TV value)
        {
            if(key == null)
                throw new ArgumentNullException(nameof(key));
            
            var node = new AvlNode<TK,TV>(key,value);
            if (_root == null)
            {
                _root = node;
                Count = 1;
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
                Count++;
            }

            var balance = node.Balance;

            switch (balance)
            {
                case int b when b > 1 && node.Key.CompareTo(node.Left.Key) == -1:
                    RightRotate(node);
                    break;
                case int b when b > 1 && node.Key.CompareTo(node.Left.Key) == 1:
                    node.Left = LeftRotate(node.Left);
                    RightRotate(node);
                    break;
                case int b when b < -1 && node.Key.CompareTo(node.Right.Key) == 1:
                    LeftRotate(node);
                    break;
                case int b when b < -1 && node.Key.CompareTo(node.Right.Key) == -1:
                    node.Right = RightRotate(node.Right);
                    LeftRotate(node);
                    break;
                case int b when b < -1 && node.Key.CompareTo(node.Right.Key) == 1:
                    LeftRotate(node);
                    break;
            }
        }

        private AvlNode<TK, TV> LeftRotate(BinaryNode<TK, TV> nodeLeft)
        {
            throw new NotImplementedException();
        }

        private AvlNode<TK,TV> RightRotate(AvlNode<TK, TV> node)
        {
            throw new NotImplementedException();
        }

        private AvlNode<TK, TV> FindParentToInsert(TK key)
        {
            if (_root == null)
                return null;
            var result = _root;
            while (result.HasChildren)
            {
                switch (key.CompareTo(result.Key))
                {
                    case int cmp when cmp == -1 && result.HasLeftChild:
                        result = result.Left;
                        break;
                    case int cmp when cmp == 1 && result.HasRightChild:
                        result = result.Right;
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
            _root = null;
        }

        public int Count { get; private set; } = 0;

        public bool ContainsKey(TK key)
        {
            throw new NotImplementedException();
        }
    }
}