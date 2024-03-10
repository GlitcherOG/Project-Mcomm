using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSX3_Server
{
    public class ByteUtil
    {
        public static string ReadString(byte[] bytes, int Offset, int Size)
        {
            byte[] tempByte = new byte[Size];

            Array.Copy(bytes, Offset, tempByte, 0, Size);

            return Encoding.ASCII.GetString(tempByte);
        }

    }
}
