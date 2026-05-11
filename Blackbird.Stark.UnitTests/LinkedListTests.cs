using Blackbird.Stark.Collections;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class LinkedListTest
    {
        [Fact]
        public void LinkedListAdd()
        {
            var lst = new LinkedList<int> {1, 2, 3};
            int n = 1;
            foreach (var i in lst)
            {
                Assert.Equal(n++, i);
            }
            Assert.Equal(3, lst.Count);
        }

        [Fact]
        public void LinkedListContains()
        {
            var lst = new LinkedList<int> {2};
            Assert.Contains(2, lst);
        }
        
        [Fact]
        public void LinkedListRemove()
        {
            var lst = new LinkedList<int> {2};
            lst.Remove(2);
            Assert.Empty(lst);
        }

        [Fact]
        public void LinkedListLinq()
        {
            var lst = new LinkedList<int> {1, 2};
            Assert.Contains(lst, x => x == 2);
        }

        [Fact]
        public void IndexerTest_Get()
        {
            var lst = new LinkedList<int>() {0,1,2,3,4,5,6,7,8,9};
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(i, lst[i]);
            }
        }

        [Fact]
        public void IndexerTest_Set()
        {
            var lst = new LinkedList<int>() {9,8,7,6,5,4,3,2,1,0};
            for (int i = 0; i < 10; i++)
            {
                lst[i] = i;
                Assert.Equal(i, lst[i]);
            }
        }

        [Fact]
        public void RemoveAt()
        {
            var lst = new LinkedList<int>(){1,2,3,4,5,6,7};
            lst.RemoveAt(1);
            Assert.Equal(6, lst.Count);
            Assert.Equal(1,lst[0]);
            Assert.Equal(3,lst[1]);
            Assert.Equal(4, lst[2]);
        }

        [Fact]
        public void Insert()
        {
            var lst = new LinkedList<int>(){1,2,4};
            lst.Insert(2,3);
            Assert.Equal(4,lst.Count);
            Assert.Equal(3, lst[2]);
        }

        [Fact]
        public void Insert_AtTail_LinksForwardAndBackward()
        {
            var lst = new LinkedList<int> { 1, 2, 3 };
            lst.Insert(3, 4);
            Assert.Equal(4, lst.Count);
            Assert.Equal(new[] { 1, 2, 3, 4 }, lst);
            // Remove the old tail — if Next pointers were broken, new tail would not be reachable.
            lst.Remove(3);
            Assert.Equal(new[] { 1, 2, 4 }, lst);
            // Remove the new tail — forces back-pointer use.
            lst.Remove(4);
            Assert.Equal(new[] { 1, 2 }, lst);
        }

        [Fact]
        public void Insert_AtHead_LinksOldHeadBackPointer()
        {
            var lst = new LinkedList<int> { 2, 3 };
            lst.Insert(0, 1);
            Assert.Equal(new[] { 1, 2, 3 }, lst);
            // Remove the head — uses forward pointers.
            lst.Remove(1);
            Assert.Equal(new[] { 2, 3 }, lst);
        }

        [Fact]
        public void Insert_InMiddle_FixesBothDirections()
        {
            var lst = new LinkedList<int> { 1, 2, 4 };
            lst.Insert(2, 3);
            // Remove the node before the insertion point — requires the
            // inserted node's Previous backlink from 4 to point through 3.
            lst.Remove(2);
            Assert.Equal(new[] { 1, 3, 4 }, lst);
        }

        [Fact]
        public void Insert_OutOfRange_Throws()
        {
            var lst = new LinkedList<int> { 1 };
            Assert.Throws<System.ArgumentOutOfRangeException>(() => lst.Insert(-1, 0));
            Assert.Throws<System.ArgumentOutOfRangeException>(() => lst.Insert(2, 0));
        }

        [Fact]
        public void Contains_NullItem_DoesNotThrow()
        {
            var lst = new LinkedList<string> { "a", null, "b" };
            Assert.True(lst.Contains(null));
            Assert.False(lst.Contains("missing"));
        }

        [Fact]
        public void Remove_NullItem_Succeeds()
        {
            var lst = new LinkedList<string> { "a", null, "b" };
            Assert.True(lst.Remove(null));
            Assert.Equal(2, lst.Count);
            Assert.False(lst.Contains(null));
        }

        [Fact]
        public void IndexOf_NullItem_ReturnsPosition()
        {
            var lst = new LinkedList<string> { "a", null, "b" };
            Assert.Equal(1, lst.IndexOf(null));
        }
    }
}
