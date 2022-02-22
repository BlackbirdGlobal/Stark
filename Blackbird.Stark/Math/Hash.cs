using System;
using System.Collections;
using System.Numerics;
using System.Reflection;

namespace Blackbird.Stark.Math;

public static class Hash
{
    public static int Calculate(object obj, int seed = 3, int offset = 7)
    {
        var objType = obj.GetType();
        if (objType.IsPrimitive || obj is string)
            return obj.GetHashCode();

        if (objType.IsArray || obj is IEnumerable)
        {
            var arr = obj as IEnumerable;
            var result = new BigInteger(seed);
            foreach (var e in arr)
            {
                unchecked
                {
                    var a = offset * Calculate(e);
                    if (a != 0)
                    {
                        result *= a;
                        if (result == 0)
                            throw new InvalidOperationException();
                    }
                    else
                    {
                        throw new ArgumentException($"{a} / {e}");
                    }
                }
            }
            return (int) (result % int.MaxValue);
        }

        if (objType.IsClass)
        {
            foreach (var member in objType.GetFields(BindingFlags.Instance))
            {
                
            }
        }
        
        return 1;
    }
}