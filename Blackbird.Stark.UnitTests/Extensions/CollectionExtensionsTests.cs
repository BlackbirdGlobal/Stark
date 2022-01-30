using System.Collections.Generic;
using Blackbird.Stark.Extensions;
using Xunit;

namespace Blackbird.Stark.UnitTests.Extensions;

public class CollectionExtensionsTests
{
    [Fact]
    public void LogicalEquals_arrayOfInts_returnsTrue()
    {
        //Arrange
        var arr1 = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        var arr2 = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        //Act
        var result = arr1.LogicalEquals(arr2);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public void LogicalEquals_ListOfStrings_returnsTrue()
    {
        //Arrange
        var lst1 = new List<string>() { "Two", "Four", "Three" };
        var lst2 = new List<string>() { "Two", "Three", "Four" };
        //Act
        var result = lst1.LogicalEquals(lst2);
        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void LogicalEquals_arrayOfIntsWithSameValues_returnsTrue()
    {
        //Arrange
        var arr1 = new[] { 9, 1, 1 };
        var arr2 = new[] { 1, 9, 1 };
        //Act
        var result = arr1.LogicalEquals(arr2);
        //Assert
        Assert.True(result);
    }
}