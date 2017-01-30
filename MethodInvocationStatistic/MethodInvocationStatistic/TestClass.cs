using AspectInjector.Broker;
using System;
using System.Threading;


namespace MethodInvocationStatistic
{
    class TestClass
    {
        [Aspect(typeof(MethodStatistics))]
        public static void TestMethod()
        {
            Thread.Sleep(4000);
            Console.WriteLine("TestMethod");
        }


        static void Main(string[] args)
        {
            TestMethod();
            Console.ReadKey();
        }
    }
}