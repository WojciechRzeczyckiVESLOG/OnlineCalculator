using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_Lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser p1 = new Parser("-1*2+43+21+9-17");
            Console.WriteLine(p1.getResult());
        }
    }
}
