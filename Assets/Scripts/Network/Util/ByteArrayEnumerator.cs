using System;
using System.Collections.Generic;
using System.Linq;

namespace Network.Util
{
    public class ByteArrayEnumerator
    {
        private readonly IEnumerator<byte> _payload;
        public ByteArrayEnumerator(byte[] payload)
        {
            _payload = payload.ToList().GetEnumerator();
        }

        public int MoveNextToGetInt()
        {
            var b = new List<byte>();
            for (int i = 0; i < 4; i++)
            {
                _payload.MoveNext();
                b.Add(_payload.Current);
            }
            return BitConverter.ToInt32(b.ToArray(),0);
        }

        public short MoveNextToGetShort()
        {
            var b = new List<byte>();
            for (int i = 0; i < 2; i++)
            {
                _payload.MoveNext();
                b.Add(_payload.Current);
            }
            return BitConverter.ToInt16(b.ToArray(),0);
        }
    }
}