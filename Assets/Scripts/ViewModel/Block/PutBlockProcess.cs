using System;
using System.Collections.Generic;
using Network;
using Network.Util;
using UnityEngine;

namespace ViewModel.Block
{
    public static class PutBlockProcess
    {
        public static void SendPutBlock(Vector2Int position, int blockId)
        {
            var send = new List<byte>();
            send.AddRange(ByteArrayConverter.ToByteArray((short)1));
            send.AddRange(ByteArrayConverter.ToByteArray(blockId));
            send.AddRange(ByteArrayConverter.ToByteArray((short)0));
            send.AddRange(ByteArrayConverter.ToByteArray(position.x));
            send.AddRange(ByteArrayConverter.ToByteArray(position.y));
            send.AddRange(ByteArrayConverter.ToByteArray(Int32.MaxValue));
            send.AddRange(ByteArrayConverter.ToByteArray(Int32.MaxValue));
            PacketHandler.SendMessages(send.ToArray());
        }
    }
}