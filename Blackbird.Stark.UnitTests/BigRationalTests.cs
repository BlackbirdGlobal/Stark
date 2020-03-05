using Blackbird.Stark.Math;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class BigRationalTests
    {
        [Fact]
        public void ConstructorDecimalTest_CompareWithParse_BothEquals()
        {
            //Arrange
            const decimal d = 1234.1234m;
            //Act
            var brs = BigRational.Parse(d.ToString());
            var brd = new BigRational(d);
            //Assert
            Assert.Equal(brs, brd);
            Assert.Equal(brs.ToString(), brd.ToString());
        }

        [Fact]
        public void ConstructorDoubleTest_CompareWithParse_BothEquals()
        {
            //Arrange
            const double d = 1234.1234;
            //Act
            var brs = BigRational.Parse(d.ToString());
            var brd = new BigRational(d);
            //Assert
            Assert.Equal(brs, brd);
            Assert.Equal(brs.ToString(), brd.ToString());
        }

        [Fact]
        public void ToString_Zero_ReturnsZeroDotZero()
        {
            var result = BigRational.Zero.ToString();
        }
    }
}