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
        public void parseInputTest()
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
    }
}
