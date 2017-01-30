using AspectInjector.Broker;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodInvocationStatistic
{
   internal class MethodStatistics
    {
        private const int currentMethodIndex = 1;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private string methodName;
        private Stopwatch timer;

        [Advice(InjectionPoints.Before, InjectionTargets.Method)]
        public void OnEntry()
        {
            StackTrace stack = new StackTrace();

            StackTraceUtils stackUtils = new StackTraceUtils(stack);
            
            methodName = stackUtils.GetCurrentMethodName();
            log.Trace("Method " + methodName);

            string callers = stackUtils.GetStackTraceMethods();
            timer = new Stopwatch();
            timer.Start();
             

            log.Trace("Call stack: {0}", callers);
        }

        [Advice(InjectionPoints.After, InjectionTargets.Method)]
        public void OnExit()
        {
            timer.Stop();
            log.Trace("Execution time: {0}" , timer.ElapsedMilliseconds);
        }
    }
}
    

