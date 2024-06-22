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
            Console.WriteLine(Calculations.Factorial(5));
            Console.WriteLine(Calculations.Factorial(2));
            Console.WriteLine(Calculations.Combinations(3, 2));
        }
    }
}
