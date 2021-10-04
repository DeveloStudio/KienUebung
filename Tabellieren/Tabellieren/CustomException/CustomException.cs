using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Tabulate
{
    public class CustomException : Exception
    {
        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, Exception innerException) : base(message, innerException) { }

        public static string CustomMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                // Gets the method that throws the current exception
                MethodBase site = ex.TargetSite;

                // Exception type
                string exceptionType = ex.InnerException.GetType().Name ?? "Unknown Exception";

                // Line number where exception occur (pdb file needed)
                // From StackOverFlow: https://stackoverflow.com/questions/3328990/how-can-i-get-the-line-number-which-threw-exception
                StackTrace trace = new StackTrace(ex.InnerException, true);
                var stackFrame = trace.GetFrame(trace.FrameCount - 1);
                var lineNumber = stackFrame.GetFileLineNumber();

                if (site != null)
                {
                    StringBuilder builder = new StringBuilder("--------------------\n");
                    StringBuilder paramsBuilder = new StringBuilder();

                    // Metadata 
                    string namespaceName = site.DeclaringType.Namespace ?? " ";
                    string className = site.DeclaringType.Name ?? " ";
                    string methodName = site.Name ?? " ";
                    ParameterInfo[] parameters = site.GetParameters();

                    builder.Append("The type of exception is: " + exceptionType + "\n");
                    builder.Append("\nFrom namespace " + namespaceName + " in class " + className + " under the method " + methodName);

                    if (parameters.Length != 0)
                    {
                        bool firstElement = true;
                        foreach (ParameterInfo parameter in parameters)
                        {
                            if (firstElement)
                            {
                                //Console.WriteLine(parameter.ParameterType.Name);
                                paramsBuilder.Append("(" + parameter);
                                firstElement = false;
                            }
                            else
                            {
                                paramsBuilder.Append(", " + parameter);
                            }
                        }
                        paramsBuilder.Append(")");
                    }
                    else
                    {
                        paramsBuilder.Append("()");
                    }
                    builder.Append(paramsBuilder);
                    builder.Append(" at line: " + lineNumber + ".");
                    builder.Append(" Here are some detail information: \n");
                    builder.Append(ex.Message + "\n");
                    return builder.Append("--------------------").ToString();
                }
            }
            return ex.Message;
        }
    }
}