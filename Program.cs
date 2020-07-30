using System;
using System.Collections.Generic;

namespace Pattern_Matching_Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new PatternMatchingDictionary<int, int>();
            dict.AddCondition(1, (Predicate<int>)((i) => i > 10));
            dict.AddCondition(2, new PatternMatch<int>((i) => i % 2 == 0));
            dict.DefaultValue(-1);
            System.Console.WriteLine(dict[23]); // 1
            System.Console.WriteLine(dict[25]); // 1
            System.Console.WriteLine(dict[10]); // 2
            System.Console.WriteLine(dict[12]); // 1
            System.Console.WriteLine(dict[-1]); // -1
        }
    }
}
