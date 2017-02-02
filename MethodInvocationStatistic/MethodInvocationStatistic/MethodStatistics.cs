using AspectInjector.Broker;
using NLog;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MethodInvocationStatistic
{
   internal class MethodStatistics
    {
        private static readonly Logger log = LogManager.GetCurrentClassLogger();
        private string methodName;
        private Stopwatch timer;
        
        [Advice(InjectionPoints.Before, InjectionTargets.Method)]
        public void OnEntry()
        {
            StackTrace stack = new StackTrace();
            
            StackTraceUtils stackUtils = new StackTraceUtils(stack);
            
            methodName = stackUtils.GetCurrentMethodName();
            log.Info("Method " + methodName);

            string callers =  stackUtils.GetStackTraceMethods();
            timer = new Stopwatch();
            timer.Start();
            
            log.Info("Call stack: {0}", callers);
        }

        [Advice(InjectionPoints.After, InjectionTargets.Method)]
        public void OnExit()
        {
            timer.Stop();
            log.Info("Execution time: {0} seconds" , timer.ElapsedMilliseconds/1000);
        }
    }
}
