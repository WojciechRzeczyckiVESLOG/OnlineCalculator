using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServerFunctionality
{
    class Parser
    {
        private string userInput;
        private List<double> numbers = new List<double>();
        private List<char> operations = new List<char>();
        private double result;

        public Parser(string userInput)
        {
            this.userInput = userInput;
            parseInput();
            calculateResult();
        }

        public Parser() { }

        public void parseInput()
        {
            int inputSize = userInput.Length;
            for(int i = 0; i < inputSize; i++)
            {
                double tmp = 0.0, num = 0.0;
                bool isNum = false;
                while(i + (int)tmp < inputSize && Char.IsNumber(userInput, i + (int)tmp)) // || (Char.IsNumber(userInput, i + (int)tmp - 1) && userInput.ElementAt(i + (int)tmp) == '.') && Char.IsNumber(userInput, i + (int)tmp + 1))
                {
                    tmp++;
                    isNum = true;
                }
                for(int k = i; k <= (int)tmp + i - 1; k++)
                {
                    num += (int)(Char.GetNumericValue(userInput, k) * Math.Pow(10.0, tmp + i - 1 - k));
                }
                if (isNum == true)
                {
                    numbers.Add(num);
                    i--;
                }
                else
                {
                    switch (userInput.ElementAt(i))
                    {
                        case '+': operations.Add('+'); break;
                        case '-': operations.Add('-'); break;
                        case '*': operations.Add('*'); break;
                        case '/': operations.Add('/'); break;
                        default: throw new FormatException("Wrong operation.");
                    }
                }
                i += (int)tmp;
            }
        }

        public void calculateResult()
        {
            if(operations.Count() == numbers.Count())
            {
                numbers[0] *= -1;
                operations.RemoveAt(0);
            }
            for(int i = 0; i < operations.Count(); i++)
            {
                switch (operations.ElementAt(i))
                {
                    case '+': numbers[i + 1] = CalculationEngine.AddNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '-': numbers[i + 1] = CalculationEngine.SubNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '*': numbers[i + 1] = CalculationEngine.MulNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    case '/': numbers[i + 1] = CalculationEngine.DivNumbers(numbers.ElementAt(i), numbers.ElementAt(i + 1)); break;
                    default: throw new FormatException("Wrong operation.");
                }
            }
            this.result = numbers.ElementAt(numbers.Count() - 1);
        }

        public void setUserInput(string userInput)
        {
            this.userInput = userInput;
        }

        public double parse()
        {
            parseInput();
            calculateResult();
            numbers.Clear();
            operations.Clear();
            return result;
        }

    }
}
