using System;
using System.Collections;
using System.Collections.Generic;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Extensions;
using Blackbird.Stark.Math;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.App
{
    static class Program
    {
        static void Main(string[] args)
        {
            var a = new BigRational(1);
            var b = new BigRational(1);
            Console.WriteLine(a.Equals(b));
        }
    }
}
