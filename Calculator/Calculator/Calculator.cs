using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator
{
    public class Calculator
    {
        public string[] SplitExpression(string expression)
        {
            var isOperatorBefore = true;
            expression = expression.Replace(" ", "");
            for (var i = 0; i < expression.Length - 1; i++)
            {
                if (isOperatorBefore && expression[i] == '-' || expression[i] == ',') continue;
                isOperatorBefore = Operators.LegalSymbols.Keys.Contains(expression[i].ToString());
                if (!(Char.IsDigit(expression[i]) && (Char.IsDigit(expression[i + 1]) || expression[i + 1] == ',')))    //Don't split numbers
                {
                    expression = expression.Insert(i++ + 1, " ");
                }
            }
            return expression.Split();
        }

        public string[] PolishNotation(string[] infix)
        {
            string result = "";
            var stack = new Stack<string>();
            foreach (var element in infix)
            {
                float tmp;
                if (float.TryParse(element, out tmp))
                    result += element + " ";
                else
                    if (Operators.Operations.Keys.Contains(element))
                    {
                        //If stack is empty or symbol's priority is less than top's of stack priority
                        if (stack.Count == 0 || Operators.AllSymbols[stack.Peek()].Priority < Operators.AllSymbols[element].Priority)
                            stack.Push(element);
                        else
                        {
                            //Pop from stack to string until stack is empty or find element of stack with higher priority than element's priority
                            while (stack.Count != 0 && Operators.AllSymbols[stack.Peek()].Priority >= Operators.AllSymbols[element].Priority)
                                result += stack.Pop() + " ";
                            stack.Push(element);
                        }
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
            foreach (var element in postfix)
            {
                float tmp;
                if (float.TryParse(element, out tmp))
                    stack.Push(float.Parse(element));
                else
                {
                    float operand2 = stack.Pop(), operand1 = stack.Pop();
                    stack.Push(Operators.Operations[element].Execute(operand1, operand2));
                }
            }
            return stack.Pop();
        }

        public float Calculate(string expression)
        {
            var result = PolishNotation(SplitExpression(expression));

            return CalculatePolishNotation(result);
        }
    }
}
