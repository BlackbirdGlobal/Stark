using System;
using System.Runtime.CompilerServices;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            var d = new TreeDictionary<string, string>(new AvlTree<string, string>())
            {
                {"foo", "bar"}, {"marco", "polo"}, {"ultra", "dyading"}, {"tuning","dyading"}
            };
            Console.WriteLine(d["marco"]);
            Console.WriteLine(d["ultra"]);
            Console.WriteLine(d["foo"]);
            Console.WriteLine(d.Remove("foo"));
            Console.WriteLine(d["ultra"]);
            Console.WriteLine(d["marco"]);
        }
    }
}
