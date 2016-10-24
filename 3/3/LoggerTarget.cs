using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _3
{
    public class LoggerTarget : ILoggerTarget
    {
        private Stream targetStream;

        public LoggerTarget(Stream targetStream)
        {
            this.targetStream = targetStream;
        }   

        Stream ILoggerTarget.targetStream
        {
            get
            {
                return targetStream;
            }
            set { targetStream = value; }
        }

       
        public bool Flush()
        {
            throw new NotImplementedException();
        }

        public Task<bool> FlushAsync()
        {
            throw new NotImplementedException();
        }
    }
}
