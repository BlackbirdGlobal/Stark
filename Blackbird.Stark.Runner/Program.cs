using System;
using Blackbird.Stark.Math;

namespace Blackbird.Stark.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            var a = BigRational.One;
            while (true)
            {
                a *= 1.01;
                Console.WriteLine(a);
            }
        }
    }
}
