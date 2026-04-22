using System.Collections.Generic;
using System.Linq;
using Blackbird.Stark.Trees;
using Blackbird.Stark.Trees.Nodes;
using FsCheck;
using FsCheck.Xunit;
using Xunit;

namespace Blackbird.Stark.UnitTests.Trees
{
    // Property-based checks for AvlTree. Any random sequence of Add/Remove ops
    // must leave the tree as a valid, balanced BST whose visible state matches
    // the "reference" HashSet that tracks the same inserts and removes.
    public class AvlTreePropertyTests
    {
        private static AvlTree<int, int> Build(int[] adds, int[] removes, out HashSet<int> reference)
        {
            var tree = new AvlTree<int, int>();
            reference = new HashSet<int>();
            foreach (var k in adds ?? System.Array.Empty<int>())
            {
                if (reference.Add(k))
                    tree.Add(k, k);
            }
            foreach (var k in removes ?? System.Array.Empty<int>())
            {
                if (reference.Remove(k))
                    tree.Remove(k);
            }
            return tree;
        }

        [Property(MaxTest = 500)]
        public bool CountAndMembershipMatchReferenceSet(int[] adds, int[] removes)
        {
            var tree = Build(adds, removes, out var reference);

            if (tree.Count != reference.Count)
                return false;

            foreach (var k in reference)
            {
                if (!tree.ContainsKey(k))
                    return false;
            }
            return true;
        }

        [Property(MaxTest = 500)]
        public bool BalanceFactorStaysWithinOne(int[] adds, int[] removes)
        {
            var tree = Build(adds, removes, out _);
            return AllBalanceFactorsValid(tree._root);
        }

        [Property(MaxTest = 500)]
        public bool InOrderTraversalOfKeysIsSorted(int[] adds, int[] removes)
        {
            var tree = Build(adds, removes, out _);
            var inOrder = CollectInOrderKeys(tree._root).ToArray();
            for (int i = 1; i < inOrder.Length; i++)
            {
                if (inOrder[i - 1] >= inOrder[i])
                    return false;
            }
            return true;
        }

        [Property(MaxTest = 500)]
        public bool StoredHeightMatchesActualSubtreeHeight(int[] adds, int[] removes)
        {
            var tree = Build(adds, removes, out _);
            return StoredHeightsConsistent(tree._root);
        }

        [Fact]
        public void AddThenRemoveAll_LeavesEmptyTree()
        {
            // A deterministic regression case close to what FsCheck tends to
            // shrink to for the broken Remove path.
            var tree = new AvlTree<int, int>();
            var keys = new[] { 5, 3, 7, 1, 4, 6, 8, 2 };
            foreach (var k in keys) tree.Add(k, k);
            foreach (var k in keys) Assert.True(tree.Remove(k));
            Assert.Equal(0, tree.Count);
            Assert.Null(tree._root);
        }

        private static bool AllBalanceFactorsValid(AvlNode<int, int> node)
        {
            if (node == null) return true;
            if (node.Balance < -1 || node.Balance > 1) return false;
            return AllBalanceFactorsValid(node.Left) && AllBalanceFactorsValid(node.Right);
        }

        private static IEnumerable<int> CollectInOrderKeys(AvlNode<int, int> node)
        {
            if (node == null) yield break;
            foreach (var k in CollectInOrderKeys(node.Left)) yield return k;
            yield return node.Key;
            foreach (var k in CollectInOrderKeys(node.Right)) yield return k;
        }

        private static bool StoredHeightsConsistent(AvlNode<int, int> node)
        {
            if (node == null) return true;
            var actual = ActualHeight(node);
            if (node.Height != actual) return false;
            return StoredHeightsConsistent(node.Left) && StoredHeightsConsistent(node.Right);
        }

        private static int ActualHeight(AvlNode<int, int> node)
        {
            if (node == null) return 0;
            return 1 + System.Math.Max(ActualHeight(node.Left), ActualHeight(node.Right));
        }
    }
}
