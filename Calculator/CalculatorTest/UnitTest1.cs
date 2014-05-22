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
        public void PrimitiveExpression()
        {
            Assert.AreEqual(1, calc.Calculate("2 / 2"));
        }

        [TestMethod]
        public void PriorityExpression()
        {
            Assert.AreEqual(2, calc.Calculate("(2+ 2* 4 )/ 5"));
        }

        [TestMethod]
        public void MultiValueDigitExpression()
        {
            Assert.AreEqual(92, calc.Calculate("2+ 2* 45 + 20/ 5 - 4"));
        }

        [TestMethod]
        public void NegativeNumbersExpression()
        {
            Assert.AreEqual(-4, calc.Calculate("-2 * (3 + -1)"));
        }
    }
}