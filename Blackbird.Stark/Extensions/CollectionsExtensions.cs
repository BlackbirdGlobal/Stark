using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackbird.Stark.Extensions;

public static class CollectionsExtensions
{
    /// <summary>
    /// Compares two collections, if they are similar like [3,2,1] and [1,2,3] returns true
    /// </summary>
    /// <param name="self"></param>
    /// <param name="other"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool LogicalEquals<T>(this IEnumerable<T> self, IEnumerable<T> other)
    {
        var mapSelf = BuildMap(self);
        var mapOther = BuildMap(other);
        return mapSelf.LogicalEquals(mapOther);
    }

    private static Dictionary<T, long> BuildMap<T>(IEnumerable<T> col)
    {
        var result = new Dictionary<T, long>();
        foreach (var v in col)
        {
            if (result.ContainsKey(v))
            {
                result[v] += 1;
            }
            else
            {
                result.Add(v, 1);
            }
        }

        return result;
    }
    
    public static bool LogicalEquals<TK, TV>(this IDictionary<TK, TV> self,
        IDictionary<TK, TV> other)
    {
        if (self.Count != other.Count)
            return false;
        foreach (var k in self.Keys)
        {
            if (other.ContainsKey(k))
            {
                var valSelf = self[k];
                var valOther = other[k];
                if (!valSelf.Equals(valOther))
                    return false;
            }
            else
                return false;
        }

        return true;
    }
    
    public static Dictionary<T, int> Counter<T>(this IEnumerable<T> self)
    {
        var result = new Dictionary<T, int>();
        foreach (var item in self)
        {
            if (result.ContainsKey(item))
            {
                result[item]++;
            }
            else
            {
                result.Add(item, 1);
            }
        }

        return result;
    }
}