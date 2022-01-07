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
            var dt = DateTime.Now;
            foreach (var d in dt.GetDatesOfDayOfWeek(DayOfWeek.Monday))
            {
                Console.WriteLine(d);
            }
            
            
            var tree = new RbTree<int, string>();
            for(int i=0; i< 100; i++)
                tree.Add(i,i.ToString());
            
            tree.PrintTree();
        }
    }
}
