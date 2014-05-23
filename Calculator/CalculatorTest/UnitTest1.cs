using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace CalculatorTest
{
    [TestClass]
    public class ExpressionsTest
    {
        private Calculator calc;

        public ExpressionsTest()
        {
            calc = new Calculator();
        }

        [TestMethod]
        public void TestPrimitiveExpressions()
        {
            Assert.AreEqual(4, calc.Calculate("2 + 2"));
            Assert.AreEqual(0, calc.Calculate("2 - 2"));
            Assert.AreEqual(6, calc.Calculate("2 * 3"));
            Assert.AreEqual(1, calc.Calculate("2 / 2"));
        }

        [TestMethod]
        public void TestPriority()
        {
            Assert.AreEqual(2, calc.Calculate("(2+ 2* 4 )/ 5"));
            Assert.AreEqual(3, calc.Calculate("(5 - 3)/2 + 1*2"));
            Assert.AreEqual(5, calc.Calculate("2* (6-4) - 1 + 4 / 2"));
        }

        [TestMethod]
        public void TestMultiValueDigit()
        {
            Assert.AreEqual(92, calc.Calculate("2+ 2* 45 + 20/ 5 - 4"));
            Assert.AreEqual(12, calc.Calculate("19 * 4 / 2 -22 - 4"));
        }

        [TestMethod]
        public void TestNegativeNumbers()
        {
            Assert.AreEqual(-4, calc.Calculate("-2 * (3 + -1)"));
            Assert.AreEqual(6, calc.Calculate("2-2*-2"));
            Assert.AreEqual(-1, calc.Calculate("2   + (-3 * 1)"));
        }
    }
}