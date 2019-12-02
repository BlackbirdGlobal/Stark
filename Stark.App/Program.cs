using System;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Stark App MK-I");
            var d = new TreeDictionary<string, string>(new BinaryTree<string, string>())
            {
                {"Foo", "Bar"}, {"Marko", "Polo"}
            };
            d.TryGetValue("Marko", out string r);
            Console.WriteLine(r);
        }
    }
}
