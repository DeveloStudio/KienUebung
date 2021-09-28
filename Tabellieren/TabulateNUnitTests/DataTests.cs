using NUnit.Framework;
using System;
using System.Collections.Generic;
using Tabulate;

namespace TabulateNUnitTests
{
    public class DataTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test_Output_Null()
        {
            IEnumerable<string> datas = null;
            Assert.Throws<ArgumentNullException>(() => );
        }
    }
}