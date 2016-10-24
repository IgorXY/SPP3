using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    public interface ILoggerTarget
    {
         Stream targetStream { get; set; }
        bool Flush();
        Task<bool> FlushAsync();
    }
}
