using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Extensions;
using Blackbird.Stark.Math;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            var pi = new BigRational(103993, 33102);
            Console.WriteLine(pi.ToString(4096));

            IList<int> lst = new Collections.LinkedList<int>();

            var dic = new TreeDictionary<string, int>(new RbTree<string, int>());
            dic.Add("Marco", 123);
            Console.WriteLine(dic["Marco"]);

            var a = new BigRational(0.1);
            var b = new BigRational(0.2);
            Console.WriteLine(a+b);

            var arr = new int[] { 1, 2, 3, 2, 1 };
            var counter = arr.Counter();
            foreach (var p in counter)
            {
                Console.WriteLine($"{p.Key} = {p.Value}");
            }
        }
    }
}
