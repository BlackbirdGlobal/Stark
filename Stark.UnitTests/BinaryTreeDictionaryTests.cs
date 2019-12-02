using System.Collections.Generic;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class BinaryTreeDictionaryTests
    {
        [Fact]
        public void ContainsKey_InsertNull_ReturnsTrue()
        {
            //Arrange
            var dictionary = new TreeDictionary<string, string>(new BinaryTree<string, string>());
            const string key = "testKey";
            dictionary.Add(key, null);
            //Act
            var result = dictionary.ContainsKey(key);
            //Assert
            Assert.True(result);
            Assert.Single(dictionary);
        }

        [Fact]
        public void ContainsKey_InsertNotNull_ReturnsTrue()
        {
            //Arrange
            var dictionary = new TreeDictionary<string, string>(new BinaryTree<string, string>());
            const string key = "testKey";
            const string val = "test";
            dictionary.Add(key, val);
            //Act
            var result = dictionary.ContainsKey(key);
            //Assert
            Assert.True(result);
            Assert.Single(dictionary);
        }

        [Fact]
        public void Iteration_IteratesThroughCollection()
        {
            //Arrange
            var dictionary = new TreeDictionary<int, string>(new BinaryTree<int, string>());
            var arr = new List<int>{1, 2, 3, 4, 5, 6, 7};
            foreach (var i in arr)
            {
                dictionary.Add(i, i.ToString());
            }
            //Act & Assert
            foreach (var p in dictionary)
            {
                Assert.Equal(p.Key, int.Parse(p.Value));
                arr.Remove(p.Key);
            }
            Assert.Empty(arr);
        }

        [Fact]
        public void Remove_RemoveExistingItem_ReturnsTrue()
        {
            //Arrange
            var dictionary = new TreeDictionary<int, int>(new BinaryTree<int, int>())
            {
                {3, 30}, {5, 50}, {1, 10}, {2, 20}
            };
            //Act
            var result = dictionary.Remove(3);
            //Assert
            Assert.True(result);
            Assert.False(dictionary.ContainsKey(3));
            foreach (var p in dictionary)
            {
                Assert.NotEqual(3, p.Key);
            }
            Assert.Equal(3, dictionary.Count);
        }

        [Fact]
        public void Add_Added()
        {
            //Arrange
            var d = new TreeDictionary<int,int>(new BinaryTree<int, int>());
            //Act
            d.Add(100, 1500);
            //Assert
            Assert.Single(d);
            Assert.Equal(1500, d[100]);
            // ReSharper disable once xUnit2013
            Assert.Equal(1, d.Count);
        }

        [Fact]
        public void Indexer_SetValue_GetValue()
        {
            //Arrange
            var d = new TreeDictionary<string, string>(new BinaryTree<string, string>());
            //Act
            d["test"] = "testValue";
            //Assert
            Assert.Equal("testValue", d["test"]);
        }
    }
}
