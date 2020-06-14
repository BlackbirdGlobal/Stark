using Blackbird.Stark.Trees;
using Blackbird.Stark.Trees.Enumerators;
using Blackbird.Stark.Trees.Nodes;
using Xunit;

namespace Blackbird.Stark.UnitTests.Trees
{
    public class BinaryTreePropertiesTests
    {
        [Fact]
        public void TestAvlTree()
        {
            //Arrange
            var avl = new AvlTree<int, int>();
            //Act
            for(int i=0; i<100; i++)
                avl.Add(i,i);
            //Assert
            AssertRightSubtree(avl._root.Key, avl._root.Right);
            AssertLeftSubtree(avl._root.Key, avl._root.Left);
        }
        
        [Fact]
        public void TestBinaryTree()
        {
            //Arrange
            var avl = new BinaryTree<int, int>();
            //Act
            for(int i=0; i<100; i++)
                avl.Add(i,i);
            //Assert
            AssertRightSubtree(avl._root.Key, avl._root.Right);
            AssertLeftSubtree(avl._root.Key, avl._root.Left);
        }

        private static void AssertRightSubtree(int parentKey, BinaryNode<int, int> rootRight)
        {
            if (rootRight == null) return;
            
            var en = new BinaryTreeEnumerator<int,int>(rootRight);
            while (en.MoveNext())
            {
                Assert.True(en.Current.Key > parentKey);
            }
            if(rootRight.HasRightChild)
                AssertRightSubtree(rootRight.Key, rootRight.Right);
            if (rootRight.HasLeftChild)
                AssertLeftSubtree(rootRight.Key, rootRight.Left);
        }
        
        private static void AssertLeftSubtree(int parentKey, BinaryNode<int, int> rootLeft)
        {
            if (rootLeft == null) return;
            
            var en = new BinaryTreeEnumerator<int,int>(rootLeft);
            while (en.MoveNext())
            {
                Assert.True(en.Current.Key < parentKey);
            }
            if(rootLeft.HasRightChild)
                AssertRightSubtree(rootLeft.Key, rootLeft.Right);
            if(rootLeft.HasLeftChild)
                AssertLeftSubtree(rootLeft.Key, rootLeft.Left);
        }
    }
}