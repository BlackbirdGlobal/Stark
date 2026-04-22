using System.Linq;
using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests.Trees
{
    // Trees are generic over TK : IComparable<TK>, but the implementations
    // historically assumed CompareTo returns exactly {-1, 0, 1}. string.CompareTo
    // returns any negative/positive int, so these exercises catch the bug.
    public class TreeStringKeyTests
    {
        [Fact]
        public void AvlTree_StringKeys_AddAndLookupRoundtrip()
        {
            var tree = new AvlTree<string, int>();
            var keys = new[] { "foo", "bar", "baz", "qux", "apple", "zebra", "mango" };
            for (int i = 0; i < keys.Length; i++)
                tree.Add(keys[i], i);

            Assert.Equal(keys.Length, tree.Count);
            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(tree.ContainsKey(keys[i]));
                Assert.Equal(i, tree.Get(keys[i]));
            }
        }

        [Fact]
        public void AvlTree_StringKeys_EnumerationContainsEveryKey()
        {
            var tree = new AvlTree<string, int>();
            var keys = new[] { "foo", "bar", "baz", "qux", "apple", "zebra", "mango" };
            for (int i = 0; i < keys.Length; i++)
                tree.Add(keys[i], i);

            // The current enumerator is BFS/level-order, not in-order, so we only
            // assert set-equality. What matters for this test is that the tree is
            // a valid BST: every inserted key is findable and the structure keeps
            // them all reachable.
            var enumerated = tree.Select(kv => kv.Key).OrderBy(k => k).ToArray();
            var expected = keys.OrderBy(k => k).ToArray();
            Assert.Equal(expected, enumerated);
        }

        [Fact]
        public void AvlTree_StringKeys_RemoveAllLeavesEmptyTree()
        {
            var tree = new AvlTree<string, int>();
            var keys = new[] { "foo", "bar", "baz", "qux", "apple", "zebra", "mango" };
            for (int i = 0; i < keys.Length; i++)
                tree.Add(keys[i], i);

            foreach (var k in keys)
                Assert.True(tree.Remove(k));

            Assert.Equal(0, tree.Count);
            foreach (var k in keys)
                Assert.False(tree.ContainsKey(k));
        }

        [Fact]
        public void RbTree_StringKeys_AddAndLookupRoundtrip()
        {
            var tree = new RbTree<string, int>();
            var keys = new[] { "foo", "bar", "baz", "qux", "apple", "zebra", "mango" };
            for (int i = 0; i < keys.Length; i++)
                tree.Add(keys[i], i);

            Assert.Equal(keys.Length, tree.Count);
            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(tree.ContainsKey(keys[i]));
                Assert.Equal(i, tree.Get(keys[i]));
            }
        }

        [Fact]
        public void BinaryTree_StringKeys_AddAndLookupRoundtrip()
        {
            var tree = new BinaryTree<string, int>();
            var keys = new[] { "foo", "bar", "baz", "qux", "apple", "zebra", "mango" };
            for (int i = 0; i < keys.Length; i++)
                tree.Add(keys[i], i);

            Assert.Equal(keys.Length, tree.Count);
            for (int i = 0; i < keys.Length; i++)
            {
                Assert.True(tree.ContainsKey(keys[i]));
                Assert.Equal(i, tree.Get(keys[i]));
            }
        }
    }
}
