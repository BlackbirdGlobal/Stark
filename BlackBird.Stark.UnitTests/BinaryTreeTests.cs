using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class BinaryTreeTests
    {
        [Fact]
        public void Add_EmptyTree_CreatesRoot()
        {
            //Arrange
            const int key = 5;
            const string val = "RootNodeData";
            var tree = new BinaryTree<int, string>();
            //Act
            tree.Add(key, val);
            //Assert
            Assert.NotNull(tree._root);
            Assert.Equal(key, tree._root.Key);
            Assert.Equal(val, tree._root.Value);
            Assert.True(tree._root.IsRoot);
            Assert.False(tree._root.HasChildren);
            Assert.False(tree._root.HasBothChildren);
            Assert.False(tree._root.HasLeftChild);
            Assert.False(tree._root.HasRightChild);
            Assert.Null(tree._root.Parent);
            Assert.Null(tree._root.Left);
            Assert.Null(tree._root.Right);
        }

        [Fact]
        public void Add_SmallerThanRoot_CreatesLeft()
        {
            //Arrange
            const int rootKey = 5;
            const string rootVal = "RootNodeData";
            const int leftKey = 3;
            const string leftVal = "LeftNodeData";
            //Act
            var tree = new BinaryTree<int, string> {{rootKey, rootVal}, {leftKey, leftVal}};
            //Assert
            Assert.NotNull(tree._root);
            Assert.Equal(rootKey, tree._root.Key);
            Assert.Equal(rootVal, tree._root.Value);
            Assert.True(tree._root.IsRoot);
            Assert.True(tree._root.HasChildren);
            Assert.False(tree._root.HasBothChildren);
            Assert.True(tree._root.HasLeftChild);
            Assert.False(tree._root.HasRightChild);
            Assert.Null(tree._root.Parent);
            Assert.NotNull(tree._root.Left);
            Assert.Null(tree._root.Right);
            var leftNode = tree._root.Left;
            Assert.Equal(leftNode.Key, leftKey);
            Assert.Equal(leftNode.Value, leftVal);
            Assert.False(leftNode.IsRoot);
            Assert.False(leftNode.HasChildren);
            Assert.False(leftNode.HasBothChildren);
            Assert.False(leftNode.HasLeftChild);
            Assert.False(leftNode.HasRightChild);
            Assert.NotNull(leftNode.Parent);
            Assert.Equal(tree._root, leftNode.Parent);
            Assert.Null(leftNode.Left);
            Assert.Null(leftNode.Right);
        }
        
        [Fact]
        public void Add_GreaterThanRoot_CreatesRight()
        {
            //Arrange
            const int rootKey = 5;
            const string rootVal = "RootNodeData";
            const int rightKey = 7;
            const string rightVal = "RightNodeData";
            //Act
            var tree = new BinaryTree<int, string> {{rootKey, rootVal}, {rightKey, rightVal}};
            //Assert
            Assert.NotNull(tree._root);
            Assert.Equal(rootKey, tree._root.Key);
            Assert.Equal(rootVal, tree._root.Value);
            Assert.True(tree._root.IsRoot);
            Assert.True(tree._root.HasChildren);
            Assert.False(tree._root.HasBothChildren);
            Assert.False(tree._root.HasLeftChild);
            Assert.True(tree._root.HasRightChild);
            Assert.Null(tree._root.Parent);
            Assert.Null(tree._root.Left);
            Assert.NotNull(tree._root.Right);
            var rightNode = tree._root.Right;
            Assert.Equal(rightNode.Key, rightKey);
            Assert.Equal(rightNode.Value, rightVal);
            Assert.False(rightNode.IsRoot);
            Assert.False(rightNode.HasChildren);
            Assert.False(rightNode.HasBothChildren);
            Assert.False(rightNode.HasLeftChild);
            Assert.False(rightNode.HasRightChild);
            Assert.NotNull(rightNode.Parent);
            Assert.Equal(tree._root, rightNode.Parent);
            Assert.Null(rightNode.Left);
            Assert.Null(rightNode.Right);
            
        }
    }
}
