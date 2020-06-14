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
            for(int i=0; i< 100; i++)
                avl.Add(i,i.ToString());
            
            
            //avl.Remove(10);
            avl.PrintTree();
        }
    }
}
