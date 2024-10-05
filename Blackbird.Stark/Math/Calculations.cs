using System.Collections.Generic;
using System.Numerics;

namespace Blackbird.Stark.Math;

public static class Calculations
{
    // n! / r! * (n - r)!
    /// <summary>
    /// C = n! / r! * (n - r)!
    /// </summary>
    /// <param name="n">number of total objects</param>
    /// <param name="r">number of objects chosen at once</param>
    /// <returns></returns>
    public static BigInteger Combinations(int n, int r)
    {
        return Factorial(n) / Factorial(r) * Factorial(n - r);
    }
    // n! /(n-r)! where n is the Number of Total Objects  and r is the Number of Objects Chosen at Once
    /// <summary>
    /// P = n! / (n-r)!
    /// </summary>
    /// <param name="n">number of total objects</param>
    /// <param name="r">number of objects chosen at once</param>
    /// <returns></returns>
    public static BigInteger Permutations(int n, int r)
    {
        return Factorial(n) / Factorial(n-r);
    }

    private static readonly List<BigInteger> _cache = new List<BigInteger>(){1};
    public static BigInteger Factorial(int n)
    {
        if (_cache.Count > n)
            return _cache[n];

        for (var i = _cache.Count; i <= n; i++)
        {
            if(i == 0)
                _cache.Add(1);
            else
                _cache.Add(_cache[i-1] * i);
        }

        return _cache[n];
    }
}