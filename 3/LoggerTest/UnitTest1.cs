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
            Thread.Sleep(30);
            for(int i = 0; i< 100; i++)
            {
                logger.Log(LogLevel.Info, "Task " + n.ToString() + " working " + i.ToString());
                Thread.Sleep(300);
            }
            logger.Log(LogLevel.Info, "Task " +n.ToString() + " finish");
        }

        [TestMethod]
        public void TestMethod1()
        {
            MemoryStream ms1 = new MemoryStream();
            MemoryStream ms2 = new MemoryStream();
            MemoryStream[] ilt = new MemoryStream[2];
            ilt[0] = ms1;
            ilt[1] = ms2;

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
