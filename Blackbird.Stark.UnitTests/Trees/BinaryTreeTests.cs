using System;
using System.Collections.Generic;
using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests.Trees
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
            var tree = new BinaryTree<int, string> {{rootKey, data}, {rightKey, data}, {rightLeftKey, data}};
            //Act
            var result = tree.Remove(rootKey);
            //Assert
            Assert.True(result);
            Assert.Equal(2, tree.Count);
            Assert.Equal(rightLeftKey, tree._root.Key);
            Assert.False(tree._root.HasLeftChild);
            Assert.True(tree._root.HasRightChild);
            Assert.True(tree._root.IsRoot);
            var child = tree._root.Right;
            Assert.NotNull(child);
            Assert.Equal(rightKey, child.Key);
            Assert.False(child.IsRoot);
            Assert.False(child.HasChildren);
        }

        [Fact]
        public void Add_ThreeNodesTree_RemoveLeft()
        {
            //Arrange
            const int rootKey = 2;
            const int rightKey = 3;
            const int rightLeftKey = 1;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string> {{rootKey, data}, {rightKey, data}, {rightLeftKey, data}};
            //Act
            var result = tree.Remove(rightLeftKey);
            //Assert
            Assert.True(result);
            Assert.Equal(2, tree.Count);
            Assert.Equal(rootKey,tree._root.Key);
            Assert.True(tree._root.HasRightChild);
            Assert.False(tree._root.HasLeftChild);
            Assert.True(tree._root.HasChildren);
            var right = tree._root.Right;
            Assert.NotNull(right);
            Assert.Equal(tree._root, right.Parent);
            Assert.False(right.HasChildren);
            Assert.Equal(rightKey, right.Key);
        }
        
        [Fact]
        public void Remove_ThreeNodesTree_RemoveRight()
        {
            //Arrange
            const int rootKey = 2;
            const int rightKey = 3;
            const int rightLeftKey = 1;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string> {{rootKey, data}, {rightKey, data}, {rightLeftKey, data}};
            //Act
            var result = tree.Remove(rightKey);
            //Assert
            Assert.True(result);
            Assert.Equal(2, tree.Count);
            Assert.Equal(rootKey,tree._root.Key);
            Assert.True(tree._root.HasLeftChild);
            Assert.False(tree._root.HasRightChild);
            Assert.True(tree._root.HasChildren);
            var left = tree._root.Left;
            Assert.NotNull(left);
            Assert.Equal(tree._root, left.Parent);
            Assert.False(left.HasChildren);
            Assert.Equal(rightLeftKey, left.Key);
        }

        [Fact]
        public void Remove_FourNodesTree_RemoveRoot()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string>
            {
                {rootKey, data}, {rightKey, data}, {leftKey, data}, {successorKey, data}
            };
            //Act
            var result = tree.Remove(rootKey);
            //Assert
            Assert.True(result);
            Assert.Equal(3, tree.Count);
            Assert.Equal(successorKey, tree._root.Key);
            Assert.Equal(leftKey, tree._root.Left.Key);
            Assert.Equal(rightKey, tree._root.Right.Key);
            Assert.Equal(tree._root, tree._root.Left.Parent);
            Assert.Equal(tree._root, tree._root.Right.Parent);
            Assert.True(tree._root.IsRoot);
        }

        [Fact]
        public void Remove_ExpressTest()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string>
            {
                {rootKey, data}, {rightKey, data}, {leftKey, data}, {successorKey, data}
            };
            //Act
            while (tree.Count != 0)
                tree.Remove(tree._root.Key);
            //Assert
            Assert.Equal(0, tree.Count);
            Assert.Null(tree._root);
        }

        [Fact]
        public void ContainsKey_PositiveFlow()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string>
            {
                {rootKey, data}, {rightKey, data}, {leftKey, data}, {successorKey, data}
            };
            //Act
            var result = tree.ContainsKey(successorKey);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void ContainsKey_NegativeFlow()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string>
            {
                {rootKey, data}, {rightKey, data}, {leftKey, data}, {successorKey, data}
            };
            //Act
            var result = tree.ContainsKey(666);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void ContainsKey_KeyNull_ThrowsException()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<string, string>
            {
                {rootKey.ToString(), data},
                {rightKey.ToString(), data},
                {leftKey.ToString(), data},
                {successorKey.ToString(), data}
            };
            //Act & Assert
            Assert.Throws<ArgumentNullException>(()=> tree.ContainsKey(null));
        }

        [Fact]
        public void ContainsKey_PassEmptyString_NegativeFlow()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<string, string>
            {
                {rootKey.ToString(), data},
                {rightKey.ToString(), data},
                {leftKey.ToString(), data},
                {successorKey.ToString(), data}
            };
            //Act
            var result = tree.ContainsKey(string.Empty);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public void Remove_KeyNull_ThrowsException()
        {
            //Arrange
            const int rootKey = 5;
            const string data = "DummyData";
            var tree = new BinaryTree<string, string>
            {
                {rootKey.ToString(), data}
            };
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Remove(null));
        }

        [Fact]
        public void Add_KeyNull_ThrowsException()
        {
            //Arrange
            const string data = "DummyData";
            var tree = new BinaryTree<string, string>();
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Add(null, data));
        }

        [Fact]
        public void Get_KeyNull_ThrowsException()
        {
            //Arrange
            var tree = new BinaryTree<string, string>();
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => tree.Get(null));
        }

        [Fact]
        public void Get_PositiveFlow()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            const string expectedData = "Expected success";
            var tree = new BinaryTree<int, string>
            {
                {rootKey, data}, {rightKey, data}, {leftKey, data}, {successorKey, expectedData}
            };
            //Act
            var result = tree.Get(successorKey);
            //Assert
            Assert.Equal(expectedData, result);
        }

        [Fact]
        public void Get_NoSuchKey_ReturnsDefaulValue()
        {
            //Arrange
            const int rootKey = 5;
            const int rightKey = 7;
            const int leftKey = 3;
            const int successorKey = 4;
            const string data = "DummyData";
            var tree = new BinaryTree<int, string>
            {
                {rootKey, data}, {rightKey, data}, {leftKey, data}, {successorKey, data}
            };
            //Act
            var result = tree.Get(successorKey+100);
            //Assert
            Assert.Equal(default(string), result);
        }

        [Fact]
        public void Add_EmptyStringAsKey_AddsAndGetsValue()
        {
            //Arrange
            var tree = new BinaryTree<string, string>();
            string data = "ExpectedValue";
            //Act
            tree.Add(string.Empty, data);
            var result = tree.Get(string.Empty);
            //Assert
            Assert.NotNull(result);
            Assert.Equal(data, result);
            Assert.Equal(1, tree.Count);
        }

        [Fact]
        public void Enumeration()
        {
            //Arrange
            var keys = new List<int>{1, 2, 3, 4, 5, 6, 7};
            const string dataPrefix = "value";
            var tree = new BinaryTree<int, string>();
            foreach (var k in keys)
            {
                tree.Add(k, $"{dataPrefix}{k}");
            }
            //Act
            foreach (var pair in tree)
            {
                keys.Remove(pair.Key);
                Assert.Equal($"{dataPrefix}{pair.Key}", pair.Value);
            }
            //Assert
            Assert.Equal(7, tree.Count);
            Assert.Empty(keys);
        }
    }
}
