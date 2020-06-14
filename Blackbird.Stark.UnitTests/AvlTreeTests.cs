using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class AvlTreeTests
    {
        [Fact]
        public void A()
        {
            //Arrange
            var tree = new AvlTree<int,string>();
            
            tree.Add(1,"A");
            tree.Add(2,"B");
            tree.Add(3,"C");
            tree.Add(4,"D");
            tree.Add(5,"E");
            //Act
            var result = tree._root;
            //Assert
            Assert.NotEqual("A",result.Value);
            tree.Remove(result.Key);
            
        }
        [Fact]
        public void GeeksForGeeks_AvlTest()
        {
            //Arrange
            var tree = new AvlTree<int,string>();
            tree.Add(10,"A");
            tree.Add(20,"B");
            tree.Add(30,"C");
            tree.Add(40,"D");
            tree.Add(50,"E");
            tree.Add(25,"F");
            //Act
            var result = tree._root;
            //Assert
            Assert.NotEqual("A",result.Value);
            Assert.Equal("C", result.Value);
            Assert.Equal("D", result.Right.Value);
            Assert.Equal("E", result.Right.Right.Value);
            Assert.Equal("B", result.Left.Value);
            Assert.Equal("A", result.Left.Left.Value);
            Assert.Equal("F", result.Left.Right.Value);
        }
    }
}