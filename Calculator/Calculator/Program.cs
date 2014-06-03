using System;

namespace StringCalculator
{
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