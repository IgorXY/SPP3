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
        bool Flush(List<string> buffer);
        Task<bool> FlushAsync(List<string> buffer);
    }
}
