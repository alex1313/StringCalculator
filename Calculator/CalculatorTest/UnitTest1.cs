using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;

namespace CalculatorTest
{
    [TestClass]
    public class ExpressionsTest
    {
        [TestMethod]
        public void RegularExpression()
        {
            Assert.AreEqual(4, Program.Calculate("2 + 2"));
        }
    }
}