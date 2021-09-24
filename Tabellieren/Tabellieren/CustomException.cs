using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Tabellieren
{
    class CustomException : Exception
    {
        public CustomException() : base() { }

        public CustomException(string message) : base(message) { }

        public CustomException(string message, Exception innerException) : base(message, innerException) { }

        public static string CustomMessage(Exception ex)
        {
            // Gets the method that throws the current exception
            MethodBase site = ex.TargetSite;

            // Exception type
            string exceptionType = ex.InnerException.GetType().Name == null ? "Unknown Exception" : ex.InnerException.GetType().Name;

            // Line number where exception occur (pdb file needed)
            StackTrace trace = new StackTrace(ex.InnerException, true);
            var stackFrame = trace.GetFrame(trace.FrameCount - 1);
            var lineNumber = stackFrame.GetFileLineNumber();

            if (site != null)
            {
                StringBuilder builder = new StringBuilder("--------------------\n");
                StringBuilder paramsBuilder = new StringBuilder();

                // metadata 
                string namespaceName = site.DeclaringType.Namespace == null ? " " : site.DeclaringType.Namespace;
                string className = site.DeclaringType.Name == null ? " " : site.DeclaringType.Name;
                string methodName = site.Name == null ? " " : site.Name;
                ParameterInfo[] parameters = site.GetParameters();

                builder.Append("The type of exception is: " + exceptionType + "\n");
                builder.Append("\nFrom namespace " + namespaceName + " in class " + className + " under the method " + methodName);

                if (parameters != null)
                {
                    bool firstElement = true;
                    foreach (ParameterInfo parameter in parameters)
                    {
                        if (firstElement)
                        {
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
                builder.Append(" at line: " + lineNumber + ". ");
                builder.Append("Here are some detail information: \n");
                builder.Append(ex.Message + "\n");
                return builder.Append("--------------------\n").ToString();
            }
            return "Unknown Exception";
        }
    }
}