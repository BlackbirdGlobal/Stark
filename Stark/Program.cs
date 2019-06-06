using Stark.Graphs;
using Stark.Graphs.BFS;
using System;
using System.Numerics;
using System.Linq;
using Stark.Collections;
using Stark.Graphs.Builders;
using Stark.Trees;

namespace Stark
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = new TreeDictionary<string,string>( new BinaryTree<string, string>());
            d.Add("Foo","Bar");
            d.Add("Marko","Polo");
            d.TryGetValue("Marko", out string r);
            Console.WriteLine(r);
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
    }
}
