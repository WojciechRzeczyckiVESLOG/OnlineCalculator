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
            Parser p1 = new Parser("-1*2,3+2^2+43,8+21+9,7-17*2+2^2");
            Console.WriteLine(p1.getResult());
            p1.setUserInput("2*3-1-10+12*3-7+2^3");
            p1.execute();
            Console.WriteLine(p1.getResult());
            p1.setUserInput("cos(180)");
            p1.execute();
            Console.WriteLine(p1.getResult());
        }
    }
}
