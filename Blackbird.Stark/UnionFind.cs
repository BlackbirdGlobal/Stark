using System;

namespace Blackbird.Stark;

public class UnionFind
{
    private readonly int[] _parents;
    private readonly int[] _rank;

    public UnionFind(int n)
    {
        _parents = new int[n + 1];
        _rank = new int[n + 1];
        for (int i = 1; i <= n; i++)
        {
            _parents[i] = i;
        }
    }

    public bool Union(int x, int y)
    {
        var px = Find(x);
        var py = Find(y);
        if (px == py)
            return false;

        if (_rank[px] > _rank[py])
        {
            _parents[py] = px;
        }
        else if (_rank[px] < _rank[py])
        {
            _parents[px] = py;
        }
        else
        {
            _parents[py] = px;
            _rank[px]++;
        }
        return true;
    }

    public int Find(int x)
    {
        if (x < 1 || x >= _parents.Length)
            throw new ArgumentOutOfRangeException(nameof(x));
        if (_parents[x] != x)
            _parents[x] = Find(_parents[x]);
        return _parents[x];
    }
}
