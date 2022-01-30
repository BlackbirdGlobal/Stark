using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Extensions;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            ITree<int, string> tree = new RbTree<int, string>();
            
            tree.Add(5, "A");
            tree.Add(3, "B");
            tree.Add(1, "C");
            
            tree.PrintTree();
        }
    }
}
