using System;
using System.Collections;
using System.Collections.Generic;
using Blackbird.Stark.Collections;
using Blackbird.Stark.Extensions;
using Blackbird.Stark.Trees;

namespace Blackbird.Stark.App
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number");
            var number = Console.ReadLine();
            var r = number.IsNumber();
            Console.WriteLine(r);
        }
    }
}
