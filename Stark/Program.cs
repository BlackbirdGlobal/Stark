using Stark.Graphs;
using Stark.Graphs.BFS;
using System;
using System.Net.Http;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;

namespace Stark
{
    class Program
    {
        static void Main(string[] args)
        {
            _matrix = new[] {
                "XXXXXXXXXXXXXXXXX",
                "XXX.XX.XXXXXXXXXX",
                "XX.*..M.XXXXXXXXX",
                "XXX.XX.XXXXXXXXXX",
                "XXXXXXXXXXXXXXXXX" };
            Console.WriteLine(CountLuck(1));
            Console.ReadKey();
        }

        private static string[] _matrix;

        private static string CountLuck(int k)
        {
            var builder = new StringMatrixGraphBuilder();
            var graph = builder.Build(_matrix);
            IGraphSearcher<char, int> s = new BFSSearcher<char, int>(graph);
            var r = s.Search(builder.Destination, (n, v) =>
            {
                if (n.Parent != null)
                    n.AdditionalData = n.Parent.AdditionalData;
                if (n.Value == v)
                    return;
                if (n.Children != null && n.Children.Count > 1)
                    n.AdditionalData += 1;
            });
            if (!r.Found)
                return "Oops!";
            return r.Result.AdditionalData == k ? "Impressed" : "Oops!";
        }

        static BigInteger GridlandMetro(int n, int m, int k, int[][] track) 
        {
            BigInteger r = 0;
            var grouped = track.GroupBy(x => x.First());
            foreach(var g in grouped){
                r += Count(g);
            }
            return r;
        }

        private static int Count(IGrouping<int, int[]> g)
        {
            var rngs = new List<Range>();

            foreach(var rng in g)
            {
                var r = rng[0];
                var c1 = rng[1];
                var c2 = rng[2];
                rngs.Add(new Range { L = c1, R = c2 });
            }

            var nonOverlapping = new List<Range>();

            while (rngs.Any())
            {
                var r = rngs.First();
                rngs.Remove(r);
                var merged = r;
                if(rngs.Any(x => r.IsOverlapping(x)))
                {
                    var overlapping = rngs.Where(x => r.IsOverlapping(x)).ToList();

                    foreach (var o in overlapping)
                    {
                        merged = merged.Merge(o);
                        rngs.Remove(o);
                    }
                }
                nonOverlapping.Add(merged);
            }

            return nonOverlapping.Sum(x => x.Length);

        }
    }
}
