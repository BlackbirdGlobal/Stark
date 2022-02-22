using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Blackbird.Stark.Math;
using Xunit;

namespace Blackbird.Stark.UnitTests.Math;

public class HashTests
{
    [Theory]
    [InlineData(123)]
    [InlineData('A')]
    [InlineData(32.1)]
    [InlineData("ABC")]
    public void Calculate_PrimitiveTypes_ReturnsGetHashCodeResult(object obj)
    {
        //Arrange
        var expected = obj.GetHashCode();
        //Act
        var result = Hash.Calculate(obj);
        //Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(new[]{1, 2, 3}, new[]{3, 2, 1})]
    public void Calculate_SameObjects_SameResult(object a, object b)
    {
        //Arrange
        var expected = Hash.Calculate(a);
        //Act
        var result = Hash.Calculate(b);
        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Calculate_TwoListsWithSameEntries_SameHashcode()
    {
        //Arrange
        var lstA = new List<int>() { 2, 3, 4 };
        var lstB = new List<int>() { 4, 2, 3 };
        //Act
        var hashA = Hash.Calculate(lstA);
        var hashB = Hash.Calculate(lstB);
        //Assert
        Assert.Equal(hashA, hashB);
    }

    [Fact]
    public void Calculate_BigListOverflowHappens_NotZeroHashCode()
    {
        //Arrange
        var bigListA = Enumerable.Range(0, 1000000);
        //Act
        var result = Hash.Calculate(bigListA);
        //Assert
        Assert.NotEqual(0, result);
    }
}