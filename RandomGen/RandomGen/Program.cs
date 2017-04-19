using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomGen
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int n = 1;
            int[] result = new int[10001];

            while (n < 10000)
            {
                Console.WriteLine("Current value of n is {0}", n);
                Console.WriteLine(r.Next(1, 10000));
                int x;
                x = Convert.ToInt16(r);
                Console.Write(n);
                result[x] = +1;
                // int milliseconds = 2000;
               
                n++;
            }

            foreach (int k in result)
            {
                System.Console.WriteLine(k);
            }


            Console.ReadKey();

        }
    }
}

