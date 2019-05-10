using Stark.Graphs;
using Stark.Graphs.BFS;
using System;
using System.Net.Http;
using System.Numerics;
using System.Linq;
using System.Collections.Generic;
using Stark.Graphs.Builders;
using System.Globalization;
using Stark.Ranges;

namespace Stark
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GridlandMetro(1, 5, 3, new[] { new[] { 1, 1, 2 }, new[] { 1, 2, 4 }, new[] { 1, 3, 5 } }));
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

        static BigInteger GridlandMetro(long n, long m, int k, int[][] track) 
        {
            BigInteger r = 0;
            var grouped = track.GroupBy(x => x.First());
            foreach(var g in grouped){
                r += Count(g);
            }
            BigInteger a = (BigInteger)n * m;
            return a-r;
        }

        private static BigInteger Count(IGrouping<int, int[]> g)
        {
            var rngs = new List<Range>();

            foreach(var rng in g)
            {
                var r = rng[0];
                var c1 = rng[1];
                var c2 = rng[2];
                rngs.Add(new Range { L = c1, R = c2 });
            }

            var nonOverlapping = rngs.MergeOverlappingRanges();

            return nonOverlapping.Sum(x => x.Length);

        }
    }
}
