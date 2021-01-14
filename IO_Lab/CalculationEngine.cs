using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO_Lab
{
    class CalculationEngine
    {
        public static void FixOrder(ref List<double> numbers, ref List<char> operations)
        {
            if (operations.Count() == numbers.Count())
            {
                numbers[0] *= -1;
                operations.RemoveAt(0);
            }
            for (int i = 0; i < operations.Count(); i++)
            {
                if (operations.ElementAt(i) == '^')
                {
                    numbers[i] = PowNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1));
                    numbers.RemoveAt(i + 1);
                    operations.Remove('^');
                    i--;
                }
                else if (operations.ElementAt(i) == '*')
                {
                    numbers[i] = MulNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1));
                    numbers.RemoveAt(i + 1);
                    operations.Remove('*');
                    i--;
                }
                else if(operations.ElementAt(i) == '/')
                {
                    numbers[i] = DivNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1));
                    numbers.RemoveAt(i + 1);
                    operations.Remove('/');
                    i--;
                }
            }
        }
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
            return num1 - num2;
        }

        public static double SubNumbers(double num1, double num2)
        {
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

        public static double PowNumbers(double num1, double num2)
        {
            return Math.Pow(num1, num2);
        }
        public static double PowNumbers(int num1, int num2)
        {
            return Math.Pow(Convert.ToDouble(num1), Convert.ToDouble(num2));
        }
    }
}
