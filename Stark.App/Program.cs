using System;
using Stark.Collections;
using Stark.Trees;

namespace Stark.App
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
