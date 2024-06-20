namespace Blackbird.Stark;

public class UnionFinder
{
    private readonly int[] _parent;
    public UnionFinder(int n)
    {
        _parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            _parent[i] = i;
        }
    }

    public int Find(int x)
    {
        if (_parent[x] != x)
            _parent[x] = Find(_parent[x]);
        return _parent[x];
    }

    public void Union(int x, int y)
    {
        _parent[Find(x)] = _parent[Find(y)];
    }
}