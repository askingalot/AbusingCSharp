using System;
using System.Collections.Generic;
using System.Linq;
using static AbusingDotNet.FuncTools;

namespace AbusingDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var f = F((int a) => a + 2);
            var g = F((int a) => a * a);

            var num = 2 
                | F((int a) => a + 2)
                | f
                | g
                | g
                | F<int, int>(Triple);

            Console.WriteLine(num);


            var flippedAndLoud = "Hello World"
                | F((string s) => s.Reverse())
                | F((IEnumerable<char> chs) => chs.ToArray())
                | F((char[] chs) => new String(chs))
                | F((string s) => s.ToUpper());

            Console.WriteLine(flippedAndLoud);


            var tomorrow = F((DateTime dt) => dt.AddDays(1));
            var noon = F((DateTime dt) => dt.Date.AddHours(12));
            var tomorrowNoon = tomorrow + noon;
            Console.WriteLine(
                DateTime.Now | tomorrowNoon
            );
        }

        static int Triple(int n) {
            return 3 * n;
        }
    }
}
