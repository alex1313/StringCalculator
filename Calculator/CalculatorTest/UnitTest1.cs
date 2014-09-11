using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StringCalculator;

namespace CalculatorTest
{
    [TestClass]
    public class ExpressionsTest
    {
        private readonly Calculator _calc;

        public ExpressionsTest()
        {
            _calc = new Calculator();
        }

        [TestMethod]
        public void TestPrimitiveExpressions()
        {
            Assert.AreEqual(4, _calc.Calculate("2 + 2"));
            Assert.AreEqual(0, _calc.Calculate("2 - 2"));
            Assert.AreEqual(6, _calc.Calculate("2 * 3"));
            Assert.AreEqual(1, _calc.Calculate("2 / 2"));
        }

        [TestMethod]
        public void TestPriority()
        {
            Assert.AreEqual(2, _calc.Calculate("(2+ 2* 4 )/ 5"));
            Assert.AreEqual(3, _calc.Calculate("(5 - 3)/2 + 1*2"));
            Assert.AreEqual(5, _calc.Calculate("2* (6-4) - 1 + 4 / 2"));
        }

        [TestMethod]
        public void TestMultiValueDigit()
        {
            Assert.AreEqual(92, _calc.Calculate("2+ 2* 45 + 20/ 5 - 4"));
            Assert.AreEqual(12, _calc.Calculate("19 * 4 / 2 -22 - 4"));
        }

        [TestMethod]
        public void TestNegativeNumbers()
        {
            Assert.AreEqual(-4, _calc.Calculate("-2 * (3 + -1)"));
            Assert.AreEqual(46, _calc.Calculate("2-22*-2"));
            Assert.AreEqual(-64, _calc.Calculate("2   * (-33 + 1)"));
        }

        [TestMethod]
        public void TestFloatNumbers()
        {
            Assert.AreEqual(-4.62, Math.Round(_calc.Calculate("-2,2 * (3,1 + -1)"), 2));
            Assert.AreEqual(6.28, Math.Round(_calc.Calculate("2-2,14*-2"), 2));
            Assert.AreEqual(-1, _calc.Calculate("2   + -3,3 / 1,1"));
        }
    }
}