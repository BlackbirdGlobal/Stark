using System.Numerics;
using Blackbird.Stark.Extensions;
using Xunit;

namespace Blackbird.Stark.UnitTests.Extensions
{
    public class BigIntegerExtensionsTests
    {
        [Fact]
        public void Sqrt_SimpleRoot_PositiveFlow()
        {
           //Arrange
           var a = new BigInteger(625);
           //Act
           var r = a.Sqrt();
           //Assert
           Assert.Equal(25, r);
        }
        
        [Fact]
        public void Sqrt_NoSimpleRoot_ReturnsSmaller()
        {
            //Arrange
            var a = new BigInteger(10);
            //Act
            var r = a.Sqrt();
            //Assert
            Assert.Equal(3, r);
        }
        
        [Fact]
        public void Sqrt_NoSimpleRoot2_ReturnsSmaller()
        {
            //Arrange
            var a = new BigInteger(8);
            //Act
            var r = a.Sqrt();
            //Assert
            Assert.Equal(2, r);
        }
        
        [Fact]
        public void Sqrt_NegativeValue_ReturnsMinusOne()
        {
            //Arrange
            var a = new BigInteger(-10);
            //Act
            var r = a.Sqrt();
            //Assert
            Assert.Equal(-1, r);
        }
    }
}