using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MethodInvocationStatistic
{
    internal class StackTraceUtils
    {
        private StackTrace stackTrace;
        private IList<StackFrame> callers;

        internal StackTraceUtils(StackTrace stackTrace)
        {
            this.stackTrace = stackTrace;
            callers = new List<StackFrame>(stackTrace.GetFrames().Reverse());
        }

        internal string GetStackTraceMethods()
        {
            string callStackString = "";

            // To ignore .NET standard invocated methods like Run(), ExecuteAssembly()
            for (int i = 8; i < callers.Count - 1; i++)
            {
                callStackString += callers[i].GetMethod() + "=>";
            }

            return callStackString;
        }

        internal string GetCurrentMethodName()
        {
            return callers[callers.Count - 2].GetMethod().ToString();
        }
    }
}
