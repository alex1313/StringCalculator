using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Program
    {
        public static int Calculate(string expression)
        {
            return 0;
        }

        static void Main(string[] args)
        {
            string expression = Console.ReadLine();
            Console.WriteLine("{0} = {1}", expression, Calculate(expression));
        }
    }
}