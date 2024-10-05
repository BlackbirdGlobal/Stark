using System.Collections.Generic;
using System.Text;

namespace Blackbird.Stark.Math;

public class Combinatorics
{
    public static IEnumerable<string> Permutations(string str)
    {
        var perms = new List<string> { string.Empty };
        foreach (var n in str)
        {
            var nextPerms = new List<string>();
            foreach (var p in perms)
            {
                for (int j = 0; j <= p.Length; j++)
                {
                    var tmp = new StringBuilder(p);
                    tmp.Insert(j, n);
                    nextPerms.Add(tmp.ToString());
                }
            }

            perms = nextPerms;
        }

        return perms;
    }
    
    public static IEnumerable<IList<int>> Permutations(int[] nums){
        var perms = new List<List<int>>{new List<int>()};
        foreach(var n in nums)
        {
            var nextPerms = new List<List<int>>();
            foreach(var p in perms)
            {
                for (int j = 0; j <= p.Count; j++)
                {
                    var tmp = new List<int>(p);
                    tmp.Insert(j, n);
                    nextPerms.Add(tmp);
                }
            }
            perms = nextPerms;
        }
        return perms;
    }
}