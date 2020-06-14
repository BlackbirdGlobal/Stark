using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class AvlTreeTests
    {
        [Fact]
        public void TreeConstruction()
        {
            //Arrange
            var avl = new AvlTree<int,string>();
            
            avl.Add(9,"9");
            avl.Add(5,"5");
            avl.Add(10,"10");
            avl.Add(0,"0");
            avl.Add(6,"6");
            avl.Add(11,"11");
            avl.Add(-1,"-1");
            avl.Add(1,"1");
            avl.Add(2,"2");
            //Act
            var result = avl._root;
            //Assert
            Assert.Equal(9,result.Key);
            Assert.Equal(10, result.Right.Key);
            Assert.Equal(1, result.Left.Key);
            Assert.Equal(11, result.Right.Right.Key);
            Assert.Equal(5, result.Left.Right.Key);
            Assert.Equal(6, result.Left.Right.Right.Key);
            Assert.Equal(2, result.Left.Right.Left.Key);
            Assert.Equal(0, result.Left.Left.Key);
            Assert.Equal(-1, result.Left.Left.Left.Key);
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