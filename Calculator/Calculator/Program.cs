using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        private readonly static Dictionary<string, int> Operators = new Dictionary<string, int>()
        {
            {"+", 2},
            {"-", 2},
            {"*", 3},
            {"/", 3}
        };
        private readonly static Dictionary<string, int> LegalSymbols = new Dictionary<string, int>(Operators)
            {
                {"(", 1},
                {",", 0}
            };
        private readonly static Dictionary<string, int> AllSymbols = new Dictionary<string, int>(LegalSymbols) { {")", 0} };

        public string[] SplitExpression(string expression)
        {
            bool isOperatorBefore = true;
            expression = expression.Replace(" ", "");
            for (int i = 0; i < expression.Length - 1; i++)
            {
                if (!(isOperatorBefore && expression[i] == '-' || expression[i] == ','))
                {
                    isOperatorBefore = LegalSymbols.Keys.Contains(expression[i].ToString());
                    if (!(Char.IsDigit(expression[i]) && (Char.IsDigit(expression[i + 1]) || expression[i + 1] == ',')))    //Don't split numbers
                    {
                        expression = expression.Insert(i++ + 1, " ");
                    }
                }
            }
            return expression.Split();
        }

        public string[] PolishNotation(string[] infix)
        {
            string result = "";
            var stack = new Stack<string>();
            foreach (string element in infix)
            {
                float tmp;
                if (float.TryParse(element, out tmp))
                    result += element + " ";
                else
                    if (Operators.Keys.Contains(element))
                        //If stack is empty or symbol's priority is less than top's of stack priority
                        if (stack.Count == 0 || AllSymbols[stack.Peek()] < AllSymbols[element])
                            stack.Push(element);
                        else
                        {
                            //Pop from stack to string until stack is empty or find element of stack with higher priority than element's priority
                            while (stack.Count != 0 && AllSymbols[stack.Peek()] >= AllSymbols[element])
                                result += stack.Pop() + " ";
                            stack.Push(element);
                        }
                    else
                        switch (element)
                        {
                            case "(":
                                stack.Push(element);
                                break;
                            case ")":
                                while (stack.Peek() != "(")
                                    result += stack.Pop() + " ";
                                stack.Pop();
                                break;
                            case " ":
                                break;
                            default:
                                Console.WriteLine("Unknown symbol was detected! The answer could be wrong!");
                                break;
                        }
            }

            while (stack.Count > 0)
                result += stack.Pop() + " ";
            result = result.Remove(result.Length - 1);  //Remove last space
            return result.Split(' ');
        }

        private float CalculatePolishNotation(IEnumerable<string> postfix)
        {
            var stack = new Stack<float>();
            foreach (string element in postfix)
            {
                float tmp;
                if (float.TryParse(element, out tmp))
                    stack.Push(float.Parse(element));
                else
                {
                    float operand2 = stack.Pop(), operand1 = stack.Pop();
                    switch (element)
                    {
                        case "+":
                            stack.Push(operand1 + operand2);
                            break;
                        case "-":
                            stack.Push(operand1 - operand2);
                            break;
                        case "*":
                            stack.Push(operand1*operand2);
                            break;
                        case "/":
                            stack.Push(operand1/operand2);
                            break;
                    }
                }
            }
            return stack.Pop();
        }

        public float Calculate(string expression)
        {
            string[] result = PolishNotation(SplitExpression(expression));

            return CalculatePolishNotation(result);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input your expression:");
            string expression = Console.ReadLine();

            var calculator = new Calculator();
            float result = calculator.Calculate(expression);
            Console.WriteLine("{0} = {1}", expression, result);
        }
    }
}