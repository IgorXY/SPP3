using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace _3
{
    public class Logger : ILogger
    {
        private List<string> buffer = new List<string>();
        private int bufferLimit;
        private ILoggerTarget[] targets;

        public Logger(int bufferLimit, ILoggerTarget[] targets)
        {
            this.bufferLimit = bufferLimit;
            this.targets = targets;
        }

        

        public void Log(LogLevel level, string message)
        {
            DateTime dt = DateTime.Now;
            string log_message = level.ToString() + ' ' + dt.ToString() + dt.Millisecond.ToString()+ ' ' + message;

            Monitor.Enter(buffer);
            buffer.Add(log_message);

            if (buffer.Count == bufferLimit)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Monitor.Enter(targets[i]);
                    try
                    {
                        targets[i].Flush(buffer);                        
                    }
                    finally
                    {
                        Monitor.Exit(targets[i]);
                    }
                    //targets[i].Flush();
                }
                buffer.Clear();
            }
            Monitor.Exit(buffer);
        }
    }
}
