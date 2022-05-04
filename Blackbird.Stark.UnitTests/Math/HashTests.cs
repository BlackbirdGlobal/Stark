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
    public void CalculateForObject_PrimitiveTypes_ReturnsGetHashCodeResult(object obj)
    {
        //Arrange
        var expected = obj.GetHashCode();
        //Act
        var result = Hash.CalculateForObject(obj);
        //Assert
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData(new[]{1, 2, 3}, new[]{3, 2, 1})]
    public void CalculateForObject_SameObjects_SameResult(object a, object b)
    {
        //Arrange
        var expected = Hash.CalculateForObject(a);
        //Act
        var result = Hash.CalculateForObject(b);
        //Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void CalculateForObject_TwoListsWithSameEntries_SameHashcode()
    {
        //Arrange
        var lstA = new List<int>() { 2, 3, 4 };
        var lstB = new List<int>() { 4, 2, 3 };
        //Act
        var hashA = Hash.CalculateForObject(lstA);
        var hashB = Hash.CalculateForObject(lstB);
        //Assert
        Assert.Equal(hashA, hashB);
    }

    [Fact]
    public void CalculateForObject_BigListOverflowHappens_NotZeroHashCode()
    {
        //Arrange
        var bigListA = Enumerable.Range(0, 10_000_000);
        //Act
        var result = Hash.CalculateForObject(bigListA);
        //Assert
        Assert.NotEqual(0, result);
    }

    [Fact]
    public void CalculateForObject_TwoClasses_SameHash()
    {
        //Arrange
        var a = new SampleData
        {
            Id = 1,
            Firstname = "Artem",
            Lastname = "Drozdov"
        };
        var b = new SampleData()
        {
            Id = 1,
            Firstname = "Artem",
            Lastname = "Drozdov"
        };
        //Act
        var aHash = Hash.CalculateForObject(a);
        var bHash = Hash.CalculateForObject(b);
        //Assert
        Assert.Equal(aHash, bHash);
    }

    private class SampleData
    {
        public int Id { get; set; }
        
        public string Firstname { get; set; }
        
        public string Lastname { get; set; }
    }
}


