using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Tabulate;

namespace TabulateNUnitTests
{
    public class TabulateTests
    {
        private CSVTabulate tabulate;
        private string[,] tableResults;
        private int[] stringLengthResult;

        [SetUp]
        public void Setup()
        {
            tabulate = new CSVTabulate();

            tableResults = new string[3, 4];
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

            stringLengthResult = new int[4];
            stringLengthResult[0] = 5;
            stringLengthResult[1] = 5;
            stringLengthResult[2] = 7;
            stringLengthResult[3] = 10;
        }

        [Test]
        public void Test_Output_Null()
        {
            MyOutput output = new MyOutput();
            IEnumerable<string> datas = null;

            // Test MyOutput class for throw the right exception if the data is null
            Assert.Throws<CustomException>(() => output.Out(datas));
        }

        [Test]
        public void Test_CreateDataPath()
        {
            string path = Directory.GetCurrentDirectory();
            string file = "Data.csv";
            string symbol = ";";

            // Path for the test
            Data testData = new Data(path + "\\" + file, symbol);

            // Test if the path result of the method equal to the expected path for class Data
            Assert.AreEqual(testData, LoadData.CreateDataPath(path, file, symbol));
        }

        [Test]
        public void Test_Tabulate_CreateTable()
        {
            string[] CSV_tests = new string[3];
            CSV_tests[0] = "Test1;Test2;Test3;TestHeader";
            CSV_tests[1] = "Test4;Test5;;Test6;TestOutRange";
            CSV_tests[2] = "Test7;Test8;Test9";
            string symbol = ";";

            // Test if the table result of the method equal to the expected table
            Assert.AreEqual(tableResults, tabulate.CreateTable(CSV_tests, symbol));
        }

        [Test]
        public void Test_Tabulate_StringLength_EachColumn()
        {
            // Test if the result of the methode matched the expected largest string each column
            Assert.AreEqual(stringLengthResult, tabulate.StringLengthEachColumn(tableResults));
        }

        [Test]
        public void Test_Tabulate_DataEdit()
        {
            List<string> testResult = new List<string>();

            string OutputTitle = "Test1|Test2|Test3  |TestHeader|";
            string HeaderOutput = "_____+_____+_______+__________+";
            string OutputBody1 = "Test4|Test5|isEmpty|Test6     |";
            string OutputBody2 = "Test7|Test8|Test9  |isNull    |";

            testResult.Add(OutputTitle);
            testResult.Add(HeaderOutput);
            testResult.Add(OutputBody1);
            testResult.Add(OutputBody2);

            // Test if the returned list is the same as the expected list
            Assert.AreEqual(testResult, tabulate.DataEdit(tableResults, stringLengthResult));
        }

        [Test]
        public void Test_Tabulate_ToString()
        {
            string[] test = new string[4];
            test[0] = "Test1|";
            test[1] = "Test2|";
            test[2] = "Test3  |";
            test[3] = "TestHeader|";

            string result = "Test1|Test2|Test3  |TestHeader|";

            // Test ToString Method of the class tabulate
            Assert.AreEqual(result, tabulate.ToString(test));
        }

        [Test]
        public void Test_CustomExMessage_NoInnerEx()
        {
            CustomException ex = new CustomException("It is a Test");
            string testmessage = "It is a Test";

            // Test custom message without innerexception 
            Assert.AreEqual(testmessage, CustomException.CustomMessage(ex));
        }

        [Test]
        public void Test_CustomExMessage_WithoutParameter()
        {
            Type T = this.GetType();

            try
            {
                try
                {
                    throw new FileNotFoundException();
                }
                catch (FileNotFoundException ex)
                {
                    throw new CustomException(ex.Message, ex);
                }
            }
            catch (CustomException ex)
            {
                //Line number where exception occur(pdb file needed)
                StackTrace trace = new StackTrace(ex.InnerException, true);
                var stackFrame = trace.GetFrame(trace.FrameCount - 1);
                var lineNumber = stackFrame.GetFileLineNumber();

                string stringToTest = "--------------------\n"
                        + "The type of exception is: FileNotFoundException\n"
                        + "\nFrom namespace " + T.Namespace + " in class " + T.Name + " under the method " + MethodBase.GetCurrentMethod().Name + "() at line: " + lineNumber + "."
                        + " Here are some detail information: \n"
                        + ex.Message + "\n"
                        + "--------------------";
                Assert.AreEqual(stringToTest, CustomException.CustomMessage(ex));
            }
        }

        [Test]
        public void Test_CustomExMessage_WithParameter(string test, string[] test2)
        {
            Type T = this.GetType();
            ParameterInfo[] infos = MethodBase.GetCurrentMethod().GetParameters();

            //string a = test;
            //string[] b = test2;

            try
            {
                try
                {
                    throw new DirectoryNotFoundException();
                }
                catch (DirectoryNotFoundException ex)
                {
                    throw new CustomException(ex.Message, ex);
                }
            }
            catch (CustomException ex)
            {
                // Line number where exception occur (pdb file needed)
                StackTrace trace = new StackTrace(ex.InnerException, true);
                var stackFrame = trace.GetFrame(trace.FrameCount - 1);
                var lineNumber = stackFrame.GetFileLineNumber();

                string stringToTest = "--------------------\n"
                        + "The type of exception is: DirectoryNotFoundException\n"
                        + "\nFrom namespace " + T.Namespace + " in class " + T.Name + " under the method "
                                                            + MethodBase.GetCurrentMethod().Name + "(" + infos[0] + ", " + infos[1] + ") at line: " + lineNumber + "."
                        + " Here are some detail information: \n"
                        + ex.Message + "\n"
                        + "--------------------";
                Assert.AreEqual(stringToTest, CustomException.CustomMessage(ex));
            }
        }
    }
}