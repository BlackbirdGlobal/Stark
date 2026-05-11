using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class UnionFindTests
    {
        [Fact]
        public void Find_InitialState_EveryElementIsItsOwnRoot()
        {
            var uf = new UnionFind(5);
            for (int i = 1; i <= 5; i++)
            {
                Assert.Equal(i, uf.Find(i));
            }
        }

        [Fact]
        public void Union_MergesSets_SharedRoot()
        {
            var uf = new UnionFind(5);
            Assert.True(uf.Union(1, 2));
            Assert.True(uf.Union(2, 3));
            Assert.Equal(uf.Find(1), uf.Find(3));
            Assert.NotEqual(uf.Find(1), uf.Find(4));
        }

        [Fact]
        public void Union_SameComponent_ReturnsFalse()
        {
            var uf = new UnionFind(3);
            Assert.True(uf.Union(1, 2));
            Assert.True(uf.Union(2, 3));
            Assert.False(uf.Union(1, 3));
        }

        [Fact]
        public void Union_TwoChains_Connect()
        {
            var uf = new UnionFind(6);
            uf.Union(1, 2);
            uf.Union(2, 3);
            uf.Union(4, 5);
            uf.Union(5, 6);
            Assert.NotEqual(uf.Find(1), uf.Find(4));
            uf.Union(3, 4);
            Assert.Equal(uf.Find(1), uf.Find(6));
        }
    }
}
