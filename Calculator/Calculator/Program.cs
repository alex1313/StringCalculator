﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    public class Calculator
    {
        private readonly HashSet<string> operators;
        private HashSet<string> legalSymbols;

        public Calculator()
        {
            operators = new HashSet<string>() {"+", "-", "*", "/"};
            legalSymbols = new HashSet<string>();
            legalSymbols.UnionWith(operators);
            legalSymbols.Add("(");
            legalSymbols.Add(",");
        }

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
            bool isOperatorBefore = true;
            s = s.Replace(" ", "");
            for (int i = 0; i < s.Length - 1; i++)
            {
                if (isOperatorBefore && s[i] == '-' || s[i] == ',')
                    continue;

                if (legalSymbols.Contains(s[i].ToString()))
                    isOperatorBefore = true;
                else
                    isOperatorBefore = false;
                if (!(Char.IsDigit(s[i]) && (Char.IsDigit(s[i + 1]) || s[i + 1] == ',')))
                    s = s.Insert(i++ + 1, " ");
            }
            return s.Split();
        }

        public string[] PolishNotation(string[] s)
        {
            float tmp;
            string result = "";
            var stack = new Stack<string>();
            foreach (string element in s)
            {
                if (float.TryParse(element, out tmp))
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

        private float CalculatePolishNotation(string[] s)
        {
            float tmp;
            var stack = new Stack<float>();
            foreach (string element in s)
            {
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

        public float Calculate(string s)
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
            float result = calculator.Calculate(expression);
            Console.WriteLine("{0} = {1}", expression, result);
        }
    }
}