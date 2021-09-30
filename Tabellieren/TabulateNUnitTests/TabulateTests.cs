using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Tabulate;

namespace TabulateNUnitTests
{
    public class TabulateTests
    {
        

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test_Output_Null()
        {
            MyOutput output = new MyOutput();
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

        [Test]
        public void Test_Tabulate_CreateTable()
        {
            string[] CSV_tests = new string[3];
            CSV_tests[0] = "Test1;Test2;Test3;TestHeader";
            CSV_tests[1] = "Test4;Test5;;Test6;TestOutRange";
            CSV_tests[2] = "Test7;Test8;Test9";

            CSVTabulate tabulate = new CSVTabulate();

            string symbol = ";";

            string[,] tableResults = new string[3, 4];
            tableResults[0, 0] = "Test1";
            tableResults[0, 1] = "Test2";
            tableResults[0, 2] = "Test3";
            tableResults[0, 3] = "TestHeader";

            tableResults[1, 0] = "Test4";
            tableResults[1, 1] = "Test5";
            tableResults[1, 2] = "isEmpty";
            tableResults[1, 3] = "Test6";

            tableResults[2, 0] = "Test7";
            tableResults[2, 1] = "Test8";
            tableResults[2, 2] = "Test9";
            tableResults[2, 3] = "isNull";

            Assert.AreEqual(tableResults, tabulate.CreateTable(CSV_tests, symbol));

        }

        [Test]
        public void Test_Tabulate_StringLength_EachColumn()
        {

        }

    }
}