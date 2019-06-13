using Stark.Collections;
using Stark.Trees;
using Xunit;

namespace Stark.UnitTests
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
    }
}
