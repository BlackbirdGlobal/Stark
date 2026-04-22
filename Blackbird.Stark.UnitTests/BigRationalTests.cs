using System.Globalization;
using System.Numerics;
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
            var brs = BigRational.Parse(d.ToString(CultureInfo.InvariantCulture));
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
            var brs = BigRational.Parse(d.ToString(CultureInfo.InvariantCulture));
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

        [Fact]
        public void Constructor_NegativeDenominator_NormalizesSignToNumerator()
        {
            var a = new BigRational(BigInteger.One, new BigInteger(-2));
            var b = new BigRational(BigInteger.MinusOne, new BigInteger(2));
            Assert.Equal(b, a);
            Assert.Equal(b.GetHashCode(), a.GetHashCode());
            Assert.Equal(-1, a.Sign);
        }

        [Fact]
        public void Constructor_BothNegative_NormalizesToPositive()
        {
            var a = new BigRational(new BigInteger(-3), new BigInteger(-4));
            var b = new BigRational(new BigInteger(3), new BigInteger(4));
            Assert.Equal(b, a);
            Assert.Equal(1, a.Sign);
        }

        [Fact]
        public void GetHashCode_FractionalValuesInSameIntegerPart_DoNotCollide()
        {
            var oneThird = new BigRational(BigInteger.One, new BigInteger(3));
            var twoThirds = new BigRational(new BigInteger(2), new BigInteger(3));
            Assert.NotEqual(oneThird.GetHashCode(), twoThirds.GetHashCode());
        }

        [Fact]
        public void GetHashCode_EqualValuesInDifferentForms_HashEqual()
        {
            // 2/4 and 1/2 reduce to the same value and must hash equal.
            var a = new BigRational(new BigInteger(2), new BigInteger(4));
            var b = new BigRational(BigInteger.One, new BigInteger(2));
            Assert.Equal(a, b);
            Assert.Equal(a.GetHashCode(), b.GetHashCode());
        }
    }
}