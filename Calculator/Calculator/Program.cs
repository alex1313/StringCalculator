using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        private HashSet<string> operators = new HashSet<string>() {"+", "-", "*", "/"};

        private int GetPriority(string c)
        {
            switch (c)
            {
                case "*":
                case "/":
                    return 3;
                case "+":
                case "-":
                    return 2;
                case "(":
                    return 1;
                default:
                    return 0;
            }
        }

        public string[] SplitExpression(string s)
        {
            s = s.Replace(" ", "");
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (!(Char.IsDigit(s[i]) && Char.IsDigit(s[i + 1])))
                    s = s.Insert(i++ + 1, " ");
            }
            return s.Split();
        }

        public string[] PolishNotation(string[] s)
        {
            string result = "";
            var stack = new Stack<string>();
            foreach (string element in s)
            {
                if (Char.IsDigit(element[0]))
                    result += element + " ";
                else
                    if (operators.Contains(element))
                        //If stack is empty or symbol's priority is less than top's of stack priority
                        if (stack.Count == 0 || GetPriority(stack.Peek()) < GetPriority(element))
                            stack.Push(element);
                        else
                        {
                            //Pop from stack to string until stack is empty or find element of stack with higher priority than element's priority
                            while (stack.Count != 0 && GetPriority(stack.Peek()) >= GetPriority(element))
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
                                Console.WriteLine("Unknown symbol was detected!");
                                break;
                        }
            }

            while (stack.Count > 0)
                result += stack.Pop() + " ";
            result = result.Remove(result.Length - 1);
            return result.Split(' ');
        }

        private int CalculatePolishNotation(string[] s)
        {
            var stack = new Stack<int>();
            foreach (string element in s)
            {
                if (Char.IsDigit(element[0]))
                    stack.Push(int.Parse(element));
                else
                {
                    int operand2 = stack.Pop(), operand1 = stack.Pop();
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

        public int Calculate(string s)
        {
            string[] result = PolishNotation(SplitExpression(s));

            return CalculatePolishNotation(result);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            string expression = Console.ReadLine();

            var calculator = new Calculator();
            int result = calculator.Calculate(expression);
            Console.WriteLine("{0} = {1}", expression, result);
        }
    }
}