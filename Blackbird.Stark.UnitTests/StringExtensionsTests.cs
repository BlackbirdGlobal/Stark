using Blackbird.Stark.Extensions;
using Xunit;

namespace Blackbird.Stark.UnitTests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("123000123.777000777", true)]
        [InlineData("-123.777",true)]
        [InlineData("-123",true)]
        [InlineData("123",true)]
        [InlineData("-123.-777",false)]
        [InlineData("-123.1e777",false)]
        public void IsNumber_Tests(string input, bool expected)
        {
            //Act
            var result = input.IsNumber();
            //Assert
            Assert.Equal(expected, result);
        }
    }
}