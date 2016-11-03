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
        private FileStream fileStream;

        public static byte[] GetBytes(List<string> buffer)
        {
            string str = "";
            for(int i = 0; i<buffer.Count; i++)
            {
                str += buffer.ElementAt(i)+'\n';
            }

            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public LoggerTarget(FileStream fileStream)
        {
            this.fileStream = fileStream;
        }

        public bool Flush(List<string> buffer)
        {
            byte[] b_array = GetBytes(buffer);            
            fileStream.Write(b_array, 0, b_array.Length);
            fileStream.Flush();
            return true;
        }

        

        public async Task<bool> FlushAsync(List<string> buffer)
        {
            byte[] b_array = GetBytes(buffer);
            fileStream.Write(b_array, 0, b_array.Length);
            await fileStream.FlushAsync();
            return true;
        }
    }
}
