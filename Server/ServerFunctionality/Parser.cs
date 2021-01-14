using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerFunctionality
{
    public class Parser
    {
        private string userInput;
        private List<double> numbers = new List<double>();
        private List<char> operations = new List<char>();
        private double result;

        public Parser(string userInput)
        {
            this.userInput = userInput;
            parseInput();
            CalculationEngine.FixOrder(ref numbers, ref operations);
            calculateResult();
        }
        public Parser() { }

        private void parseInput()
        {
            int inputSize = userInput.Length;
            for (int i = 0; i < inputSize; i++)
            {
                double tmp = 0.0, num = 0.0;
                bool isNum = false, isDouble = false;
                string tmp_str = "";
                // if checked element is a number, count how many elements long it is
                while (i + (int)tmp < inputSize && (Char.IsNumber(userInput, i + (int)tmp) ||
                    (i + (int)tmp > 0 && Char.IsNumber(userInput, i + (int)tmp - 1) && userInput.ElementAt(i + (int)tmp) == ',' && Char.IsNumber(userInput, i + (int)tmp + 1))))
                {
                    if (userInput.ElementAt(i + (int)tmp) == '.')
                        isDouble = true;
                    tmp++;
                    isNum = true;
                }
                // if number was found above, convert it to double and add it to vector
                if (isNum == true)
                {
                    for (int k = i; k <= (int)tmp + i - 1; k++)
                    {
                        tmp_str += userInput.ElementAt(k);
                    }
                    numbers.Add(Convert.ToDouble(tmp_str));
                    i--;
                }
                // else it means that our element is an operation character
                else
                {
                    switch (userInput.ElementAt(i))
                    {
                        case '+': operations.Add('+'); break;
                        case '-': operations.Add('-'); break;
                        case '*': operations.Add('*'); break;
                        case '/': operations.Add('/'); break;
                        case '^': operations.Add('^'); break;
                        default: throw new FormatException("Wrong operation."); break;
                    }
                }
                i += (int)tmp;
            }
        }

        private void calculateResult()
        {
            for (int i = 0; i < operations.Count(); i++)
            {
                switch (operations.ElementAt(i))
                {
                    case '+': numbers[i + 1] = CalculationEngine.AddNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '-': numbers[i + 1] = CalculationEngine.SubNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '*': numbers[i + 1] = CalculationEngine.MulNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '/': numbers[i + 1] = CalculationEngine.DivNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '^': numbers[i + 1] = CalculationEngine.PowNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    default: throw new FormatException("Wrong operation.");
                }
            }
            this.result = numbers.ElementAt(numbers.Count() - 1);
            numbers.Clear();
            operations.Clear();
        }

        public void setUserInput(string userInput)
        {
            this.userInput = userInput;
        }

        public double getResult()
        {
            return this.result;
        }

        public double execute()
        {
            parseInput();
            CalculationEngine.FixOrder(ref numbers, ref operations);
            calculateResult();
            return result;
        }
    }
}
