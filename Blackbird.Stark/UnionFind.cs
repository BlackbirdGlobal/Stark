using System.Collections.Generic;

namespace Blackbird.Stark;

public class UnionFind
{
    private Dictionary<int, int> _parents = new Dictionary<int, int>();
    private Dictionary<int, int> _rank = new Dictionary<int, int>();

    
    public UnionFind(int n)
    {

        for (int i = 1; i <= n; i++)
        {
            _parents[i] = i;
            _rank[i] = 0;
        }
    }

    public bool Union(int x, int y)
    {
        var px = Find(x);
        var py = Find(y);
        //if they alredy united there is a cycle
        if (px == py)
            return false;

        //get parent's ranks
        var rpx = _rank[px];
        var rpy = _rank[py];

        if (rpx > rpy)
        {
            _parents[py] = px;
        }

        if (rpx < rpy)
        {
            _parents[px] = py;
        }

        if (rpx == rpy)
        {
            _parents[py] = px;
            _rank[px] += 1;
        }
        return true;
    }

    public int Find(int x)
    {
        var p = _parents[x];
        //path compression
        while (p != _parents[p])
        {
            _parents[x] = _parents[_parents[x]];
            p = _parents[x];
        }
        return p;
    }
}