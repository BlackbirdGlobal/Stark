using System.Collections;
using System.Collections.Generic;

namespace Blackbird.Stark.Collections
{
    class LinkedListEnumerator<T>: IEnumerator<T>
    {
        private ListNode<T> _currentNode;
        public LinkedListEnumerator(ListNode<T> node)
        {
            _currentNode = node;
            Current = _currentNode.Value;
        }

        public T Current { get; private set; }

        public void Dispose()
        {
            _currentNode = null;
            Current = default(T);
        }

        object IEnumerator.Current => new LinkedListEnumerator<T>(_currentNode);

        public bool MoveNext()
        {
            if (_currentNode.Next != null)
            {
                _currentNode = _currentNode.Next;
                Current = _currentNode.Value;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            while (_currentNode.Previous != null)
            {
                _currentNode = _currentNode.Previous;
            }
        }
    }
}
