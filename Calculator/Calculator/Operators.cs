using System;
using System.Collections.Generic;

namespace StringCalculator
{
    class Operators
    {
        public struct OperatorsDef
        {
            public int Priority;

            public Func<float, float, float> Execute;
        }

        public readonly static Dictionary<string, OperatorsDef> Operations = new Dictionary<string, OperatorsDef>()
        {
            {"+", new OperatorsDef() { Priority = 2, Execute = (x, y) => x + y } },
            {"-", new OperatorsDef() { Priority = 2, Execute = (x, y) => x - y } },
            {"*", new OperatorsDef() { Priority = 3, Execute = (x, y) => x * y } },
            {"/", new OperatorsDef() { Priority = 3, Execute = (x, y) => x / y } },
        };

        public readonly static Dictionary<string, OperatorsDef> LegalSymbols = new Dictionary<string, OperatorsDef>(Operations)
        {
            {"(", new OperatorsDef() { Priority = 1 } },
            {",", new OperatorsDef() { Priority = 0 } },
        };

        public readonly static Dictionary<string, OperatorsDef> AllSymbols = new Dictionary<string, OperatorsDef>(LegalSymbols)
        {
            { ")", new OperatorsDef() { Priority = 0 } }
        };
    }
}
