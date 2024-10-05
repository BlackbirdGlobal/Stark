using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Extensions;
using Blackbird.Stark.Trees;
using Blackbird.Stark.Math;

namespace Blackbird.Stark.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Calculations.Factorial(300));
            Console.WriteLine(Calculations.Factorial(0));
            Console.WriteLine(Calculations.Combinations(3, 2));
            Console.WriteLine(Calculations.Permutations(300, 300));

            var perm = Combinatorics.Permutations(new int[]{1,9,3,6});
            foreach (var p in perm)
            {
                foreach (var c in p)
                    Console.Write(c);
                Console.WriteLine();
            }
        }
    }
}
