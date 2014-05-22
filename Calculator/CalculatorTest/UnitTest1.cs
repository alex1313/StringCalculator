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
            Assert.AreEqual(4, calc.Calculate("2 + 2"));
        }
    }
}