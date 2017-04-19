using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Boolean p, q;
            Console.WriteLine("P\tQ\tAND\tOR\tXOR\tNOT");
            p = true;
            q = true;
            writeout(p, q);
            p = true;
            q = false;
            writeout(p, q);
            p = false;
            q = true;
            writeout(p, q);
            p = false;
            q = false;
            writeout(p, q);
            Console.ReadKey();

        }

        static public void writeout(bool a, bool b)
        {
            Console.Write(a+ "\t" + b + "\t");
            Console.Write((a & b) + "\t" + (a|b) + "\t");
            Console.WriteLine((a^b) + "\t" + (!a));
        }
        
    }
}
