﻿namespace Network
{
    public class DummyProtocol : IPacketResponse
    {
        public byte[] GetResponse()
        {
            throw new System.NotImplementedException();
        }
    }
}