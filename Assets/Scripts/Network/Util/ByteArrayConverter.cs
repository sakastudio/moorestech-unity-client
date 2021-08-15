using System;

namespace Network.Util
{
    public static class ByteArrayConverter
    {
        public static byte[] ToByteArray(int sendData)
        {
            return  BitConverter.GetBytes(sendData);
        }
        public static byte[] ToByteArray(short sendData)
        {
            return BitConverter.GetBytes(sendData);
        }
    }
}