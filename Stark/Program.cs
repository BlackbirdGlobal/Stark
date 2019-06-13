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
            var d = new TreeDictionary<string, string>(new BinaryTree<string, string>())
            {
                {"Foo", "Bar"}, {"Marko", "Polo"}
            };
            d.TryGetValue("Marko", out string r);
            Console.WriteLine(r);
        }
    }
}
