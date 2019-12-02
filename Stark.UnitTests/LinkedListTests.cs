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
    }
}
