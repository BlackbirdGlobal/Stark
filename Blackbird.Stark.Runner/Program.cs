using System;
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
            var avl = new AvlTree<int, string>();
            avl.Add(9,"9");
            avl.Add(5,"5");
            avl.Add(10,"10");
            avl.Add(0,"0");
            avl.Add(6,"6");
            avl.Add(11,"11");
            avl.Add(-1,"-1");
            avl.Add(1,"1");
            avl.Add(2,"2");
            
            //avl.Remove(10);
            avl.PrintTree();
        }
    }
}
