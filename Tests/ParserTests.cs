using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ServerFunctionality;
using System.Linq;



namespace Tests
{
    [TestClass]
    public class ParserTest
    {
        private static Parser parser = new Parser();

        [TestMethod]
        public void setUserInputTest()
        {
            try
            {
                parser.setUserInput("2+2");
                return;
            }
            catch
            {
                Assert.Fail("Can't set user input");
            }
        }

        [TestMethod]
        public void executeTest()
        {
            parser.setUserInput("2+2");
            Assert.AreEqual(4, parser.execute());
        }

        [TestMethod]
        public void powersTests()
        {
            try
            {
                parser.setUserInput("2+2*2^3");
                parser.execute();
                return;
            }
            catch
            {
                Assert.AreEqual(18, parser.getResult());
            }
        }

        [TestMethod]
        public void trigonometricalFunctionsTests()
        {
            parser.setUserInput("sin(30^2)");
            parser.execute();
            Assert.AreEqual(0, parser.getResult());
            
            parser.setUserInput("sin(180)");
            parser.execute();
            Assert.AreEqual(0, parser.getResult());
        }
    }
}
