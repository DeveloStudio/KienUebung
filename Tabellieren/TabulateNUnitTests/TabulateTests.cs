using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Tabulate;

namespace TabulateNUnitTests
{
    public class TabulateTests
    {
        private MyOutput output;

        [SetUp]
        public void Setup()
        {
            output = new MyOutput();
        }

        [Test]
        public void Test_Output_Null()
        {
            IEnumerable<string> datas = null;
            Assert.Throws<CustomException>(() => output.Out(datas));
        }

        [Test]
        public void Test_CreateDataPath()
        {
            string path = Directory.GetCurrentDirectory();
            string file = "Data.csv";
            string symbol = ";";
            Data testData = new Data(path + "\\" + file, symbol);

            Assert.AreEqual(testData, LoadData.CreateDataPath(path, file, symbol));
        }


    }
}