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
            tree.Add(0,"F");
            tree.Add(6,"G");
            //Act
            var result = tree._root;
            //Assert
            Assert.NotEqual("A",result.Value);
        }
    }
}