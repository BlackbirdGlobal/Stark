using System;
using System.Collections;
using System.Collections.Generic;

namespace Blackbird.Stark.Collections
{
    public class LinkedList<T> : ICollection<T>, IList<T>
    {
        private ListNode<T> _head;
        private ListNode<T> _tail;

        public void Add(T item)
        {
            var node = new ListNode<T>(item);
            if (_head == null)
            {
                _head = _tail = node;
            }
            else
            {
                node.Previous = _tail;
                _tail = _tail.Next = node;
            }
            Count++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {            
            var tmp = _head;
            while (tmp != null)
            {
                if (tmp.Value.Equals(item))
                    return true;
                tmp = tmp.Next;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var tmp = _head;
            while (tmp != null && arrayIndex < array.Length)
            {
                array[arrayIndex] = tmp.Value;
                tmp = tmp.Next;
                arrayIndex++;
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly => false;

        public IEnumerator<T> GetEnumerator()
        {
            return new LinkedListEnumerator<T>(new ListNode<T>(default(T)) { Next = _head});
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LinkedListEnumerator<T>(new ListNode<T>(default(T)) { Next = _head });
        }

        public bool Remove(T item)
        {
            var tmp = _head;
            while (tmp != null)
            {
                if (item.Equals(tmp.Value))
                {
                    if (tmp.Previous != null)
                        tmp.Previous.Next = tmp.Next;
                    else
                        _head = tmp.Next;

                    if (tmp.Next != null)
                        tmp.Next.Previous = tmp.Previous;
                    else
                        _tail = tmp.Previous;

                    Count--;
                    return true;
                }
                tmp = tmp.Next;
            }
            return false;
        }

        public int IndexOf(T item)
        {
            int i = 0;
            var tmp = _head;
            while (tmp != null)
            {
                if (item.Equals(tmp.Value))
                    return i;
                i++;
                tmp = tmp.Next;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            var node = new ListNode<T>(item);
            if (index == 0)
            {
                node.Next = _head;
                _head = node;
                Count++;
                return;
            }

            if (index == Count)
            {
                node.Previous = _tail;
                _tail = node;
                Count++;
                return;
            }

            var middle = FindNode(index);
            node.Next = middle;
            node.Previous = middle.Previous;
            node.Previous.Next = node;
            Count++;
        }

        public void RemoveAt(int index)
        {
            int currentIndex = 0;
            var tmp = _head;
            while (tmp != null)
            {
                if (index == currentIndex++)
                {
                    if (tmp.Previous != null)
                        tmp.Previous.Next = tmp.Next;
                    else
                        _head = tmp.Next;

                    if (tmp.Next != null)
                        tmp.Next.Previous = tmp.Previous;
                    else
                        _tail = tmp.Previous;

                    Count--;
                    return;
                }
                tmp = tmp.Next;
            }
            throw new IndexOutOfRangeException();
        }

        public T this[int i]
        {
            get
            {
                int currentIndex = 0;
                var tmp = _head;
                while (tmp != null)
                {
                    if (i == currentIndex++)
                        return tmp.Value;
                    tmp = tmp.Next;
                }
                throw new IndexOutOfRangeException();
            }

            set
            {
                int currentIndex = 0;
                var tmp = _head;
                while (tmp != null)
                {
                    if (i == currentIndex++)
                    {
                        tmp.Value = value;
                        return;
                    }
                    tmp = tmp.Next;
                }
                throw new IndexOutOfRangeException();
            }
        }

        private ListNode<T> FindNode(int index)
        {
            int currentIndex = 0;
            var tmp = _head;
            while (tmp != null)
            {
                if (index == currentIndex++)
                    return tmp;
                tmp = tmp.Next;
            }
            throw new IndexOutOfRangeException();
        }
    }
}
