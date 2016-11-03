using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using _3;
using System.Threading;
using System.Threading.Tasks;

namespace LoggerTest
{
    [TestClass]
    public class UnitTest1
    {
        public static Logger logger;

        static void StupidTask()
        {
            int n = Thread.CurrentThread.ManagedThreadId;
            logger.Log(LogLevel.Info, "Task "  +n.ToString() + " started");
           // Thread.Sleep(1000);
            for(int i = 0; i< 10; i++)
            {
                logger.Log(LogLevel.Info, "Task " + n.ToString() + " working " + i.ToString());
                //Thread.Sleep(100);
            }
            logger.Log(LogLevel.Info, "Task " +n.ToString() + " finish");
        }

        [TestMethod]
        public void TestMethod1()
        {
            FileStream fs1 = new FileStream("log1.b", FileMode.Create);
            LoggerTarget lt1 = new LoggerTarget(fs1);
            FileStream fs2 = new FileStream("log2.b", FileMode.Create);
            LoggerTarget lt2 = new LoggerTarget(fs2);
            ILoggerTarget[] ilt = new ILoggerTarget[2];
            ilt[0] = lt1;
            ilt[1] = lt2;

            logger = new Logger(10, ilt);

            Task[] tasks = new Task[5];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Factory.StartNew(StupidTask);
            }

            Task.WaitAll(tasks);

            Assert.IsTrue(true);
        }
    }
}
