using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCalculator
{
    class Operators
    {
        public readonly static Dictionary<string, int> OperatorsPriority = new Dictionary<string, int>()
        {
            {"+", 2},
            {"-", 2},
            {"*", 3},
            {"/", 3}
        };
        public readonly static Dictionary<string, int> LegalSymbolsPriority = new Dictionary<string, int>(OperatorsPriority)
        {
            {"(", 1},
            {",", 0}
        };
        public readonly static Dictionary<string, int> AllSymbolsPriority = new Dictionary<string, int>(LegalSymbolsPriority) { { ")", 0 } };

        public static Dictionary<string, Func<float, float, float>> Operations = new Dictionary<string, Func<float, float, float>>
        {
            { "+", (x, y) => x + y },
            { "-", (x, y) => x - y },
            { "*", (x, y) => x * y },
            { "/", (x, y) => x / y },
        };
    }
}
