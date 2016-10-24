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
        private MemoryStream[] targets;

        public Logger(int bufferLimit, MemoryStream[] targets)
        {
            this.bufferLimit = bufferLimit;
            this.targets = targets;
        }

        public static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public void Log(LogLevel level, string message)
        {
            string log_message = level.ToString() + ' ' + DateTime.Now.ToString() + ' ' + message;

            Monitor.Enter(buffer);
            buffer.Add(log_message);

            if (buffer.Count == bufferLimit)
            {
                for (int i = 0; i < targets.Length; i++)
                {
                    Monitor.Enter(targets[i]);
                    try
                    {
                        for (int j = 0; j < bufferLimit; j++)
                        {
                            byte[] bArray = GetBytes(buffer.ElementAt(j));
                            targets[i].Write(bArray, 0, bArray.Length);
                        }
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
