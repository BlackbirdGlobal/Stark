using System;
using System.Collections.Generic;
using System.Linq;
using Blackbird.Stark.Trees;
using Xunit;

namespace Blackbird.Stark.UnitTests.Trees;

public class RbTreeTests
{
    private readonly RbTree<int, string> _tree = new RbTree<int, string>();
    
    [Fact]
    public void Add_inOrder_treeBalanced()
    {
        //Act
        _tree.Add(1, "One");
        _tree.Add(2, "Two");
        _tree.Add(3, "Three");
        //Assert
        Assert.Equal(2, _tree._root.Key);
        Assert.Equal(1, _tree._root.Left.Key);
        Assert.Equal(3, _tree._root.Right.Key);
    }

    [Fact]
    public void Remove_addAndDelete1k_CountIs0()
    {
        //Arrange
        for(var i=0; i < 1000; i++)
            _tree.Add(i, i.ToString());
        //Act
        for (int i = 0; i < 1000; i++)
            Assert.True(_tree.Remove(i));
        //Assert
        Assert.Equal(0, _tree.Count);
    }

    [Fact]
    public void Count_add10k_countEquals10k()
    {
        //Act
        for(var i=0; i < 10000; i++)
            _tree.Add(i, i.ToString());
        //Assert
        Assert.Equal(10000, _tree.Count);
    }

    [Fact]
    public void Clear_add100k_countEquals0()
    {
        //Arrange
        for(var i = 0; i < 100000; i++)
            _tree.Add(i, i.ToString());
        //Act
        _tree.Clear();
        //Assert
        Assert.Equal(0, _tree.Count);
    }

    [Fact]
    public void Get_addSampleData_returnsSampleData()
    {
        //Arrange
        const int key = 666;
        string value = "What it's like to be a heretic?";
        _tree.Add(key, value);
        //Act
        var result = _tree.Get(key);
        //Assert
        Assert.Equal(value, result);
    }
    
    [Fact]
    public void ContainsKey_addTestData_returnsTrue()
    {
        //Arrange
        const int key = 666;
        string value = "What it's like to be a heretic?";
        _tree.Add(key, value);
        //Act
        var result = _tree.ContainsKey(key);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public void Iterator_iterateThroughTree_keysAndValuesAreEmpty()
    {
        //Arrange
        const int max = 100;
        var keys = Enumerable.Range(0, max).ToList();
        var values = keys.Select(x => x.ToString()).ToList();
        for (var i = 0; i < max; i++)
        {
            _tree.Add(keys[i], values[i]);
        }
        //Act
        foreach (var pair in _tree)
        {
            keys.Remove(pair.Key);
            values.Remove(pair.Value);
        }
        //Assert
        Assert.Empty(keys);
        Assert.Empty(values);
    }
}