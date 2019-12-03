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
            Assert.Equal(1, tree.Count);
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
            Assert.Equal(2, tree.Count);
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
            Assert.Equal(2, tree.Count);
        }

        [Fact]
        public void Add_ThreeNodesTree_AddsRightLeft()
        {
            //Arrange
            const int rootKey = 5;
            const string rootVal = "RootNodeData";
            const int rightKey = 7;
            const string rightVal = "RightNodeData";
            const int rightLeftKey = 6;
            const string rightLeftData = "RightLeftNodeData";
            //Act
            var tree = new BinaryTree<int, string> { { rootKey, rootVal }, { rightKey, rightVal }, {rightLeftKey, rightLeftData} };
            //Assert
            Assert.Equal(3, tree.Count);
            var parent = tree._root.Right;
            Assert.NotNull(parent);
            Assert.True(parent.HasChildren);
            Assert.False(parent.HasBothChildren);
            Assert.True(parent.HasLeftChild);
            var leftChild = parent.Left;
            Assert.NotNull(leftChild);
            Assert.Equal(rightLeftKey, leftChild.Key);
            Assert.Equal(rightLeftData, leftChild.Value);
        }

        [Fact]
        public void Add_ThreeNodesTree_AddsRightRight()
        {
            //Arrange
            const int rootKey = 5;
            const string rootVal = "RootNodeData";
            const int rightKey = 7;
            const string rightVal = "RightNodeData";
            const int rightLeftKey = 8;
            const string rightLeftData = "RightRightNodeData";
            //Act
            var tree = new BinaryTree<int, string> { { rootKey, rootVal }, { rightKey, rightVal }, { rightLeftKey, rightLeftData } };
            //Assert
            Assert.Equal(3, tree.Count);
            var parent = tree._root.Right;
            Assert.NotNull(parent);
            Assert.True(parent.HasChildren);
            Assert.False(parent.HasBothChildren);
            Assert.False(parent.HasLeftChild);
            Assert.True(parent.HasRightChild);
            var rightChild = parent.Right;
            Assert.NotNull(rightChild);
            Assert.Equal(rightLeftKey, rightChild.Key);
            Assert.Equal(rightLeftData, rightChild.Value);
        }

        [Fact]
        public void Add_ThreeNodesTree_AddsLeftRight()
        {
            //Arrange
            const int rootKey = 5;
            const string rootVal = "RootNodeData";
            const int rightKey = 3;
            const string rightVal = "RightNodeData";
            const int rightLeftKey = 4;
            const string rightLeftData = "LRNodeData";
            //Act
            var tree = new BinaryTree<int, string> { { rootKey, rootVal }, { rightKey, rightVal }, { rightLeftKey, rightLeftData } };
            //Assert
            Assert.Equal(3, tree.Count);
            var parent = tree._root.Left;
            Assert.NotNull(parent);
            Assert.True(parent.HasChildren);
            Assert.False(parent.HasBothChildren);
            Assert.False(parent.HasLeftChild);
            Assert.True(parent.HasRightChild);
            var rightChild = parent.Right;
            Assert.NotNull(rightChild);
            Assert.Equal(rightLeftKey, rightChild.Key);
            Assert.Equal(rightLeftData, rightChild.Value);
        }

        [Fact]
        public void Add_ThreeNodesTree_AddsLeftLeft()
        {
            //Arrange
            const int rootKey = 5;
            const string rootVal = "RootNodeData";
            const int rightKey = 3;
            const string rightVal = "RightNodeData";
            const int rightLeftKey = 1;
            const string rightLeftData = "LLNodeData";
            //Act
            var tree = new BinaryTree<int, string> { { rootKey, rootVal }, { rightKey, rightVal }, { rightLeftKey, rightLeftData } };
            //Assert
            Assert.Equal(3, tree.Count);
            var parent = tree._root.Left;
            Assert.NotNull(parent);
            Assert.True(parent.HasChildren);
            Assert.False(parent.HasBothChildren);
            Assert.True(parent.HasLeftChild);
            Assert.False(parent.HasRightChild);
            var leftChild = parent.Left;
            Assert.NotNull(leftChild);
            Assert.Equal(rightLeftKey, leftChild.Key);
            Assert.Equal(rightLeftData, leftChild.Value);
        }

        [Fact]
        public void Add_ThreeNodesTree_RemoveRoot()
        {
            //Arrange
            const int rootKey = 2;
            const int rightKey = 3;
            const int rightLeftKey = 1;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string>();
            tree.Add(rootKey, data);
            tree.Add(rightKey, data);
            tree.Add(rightLeftKey, data);
            //Act
            var result = tree.Remove(rootKey);
            //Assert
            Assert.True(result);
            Assert.Equal(2, tree.Count);
            Assert.Equal(tree._root.Key, rightLeftKey);
            Assert.False(tree._root.HasLeftChild);
            Assert.True(tree._root.HasRightChild);
            Assert.True(tree._root.IsRoot);
            var child = tree._root.Right;
            Assert.NotNull(child);
            Assert.Equal(rightKey, child.Key);
            Assert.False(child.IsRoot);
            Assert.False(child.HasChildren);
        }
    }
}
