using System;
using Blackbird.Stark.Math;

namespace Blackbird.Stark.Runner
{
    static class Program
    {
        static void Main(string[] args)
        {
            var a = BigRational.Parse("20202020456,12548");
            var b = BigRational.Parse("-10000.00000012300056700000111144456666777778901110202034");
            var l = new BigRational((decimal) 1.0001);
            Console.WriteLine(a * b);
        }
    }
}
