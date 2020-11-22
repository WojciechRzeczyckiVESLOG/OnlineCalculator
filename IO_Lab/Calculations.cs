using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_Lab
{
    class Calculations
    {
        public static int AddNumbers(int num1, int num2)
        {
            return num1 + num2;
        }

        public static double AddNumbers(double num1, double num2)
        {
            return num1 + num2;
        }

        public static int SubNumbers(int num1, int num2)
        {
            // if (num1 < num2) return 0;
            return num1 - num2;
        }

        public static double SubNumbers(double num1, double num2)
        {
            // if (num1 < num2) return 0;
            return num1 - num2;
        }

        public static int MulNumbers(int num1, int num2)
        {
            return num1 * num2;
        }

        public static double MulNumbers(double num1, double num2)
        {
            return num1 * num2;
        }

        public static int DivNumbers(int num1, int num2)
        {
            return num1 / num2;
        }

        public static double DivNumbers(double num1, double num2)
        {
            return num1 / num2;
        }
    }
}
