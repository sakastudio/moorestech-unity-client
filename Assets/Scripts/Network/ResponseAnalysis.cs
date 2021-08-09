using System;
using System.Collections.Generic;
using Network.RequestPacket;
using Network.ResponsePacket;

namespace Network
{
    //TODO この辺の解析とかをインターフェースにしてテストコードを書く
    public static class ResponseAnalysis
    {
        delegate void AnalysisResponse(byte[] payload);
        private static List<AnalysisResponse> _packetResponseList = new List<AnalysisResponse>();

        private static void Init()
        {
            _packetResponseList.Add(DummyResponse.AnalysisResponse);
            _packetResponseList.Add(BlockCoordinateResponse.AnalysisResponse);
        }
        
        public static void GetPacketResponse(byte[] payload)
        {
            if (_packetResponseList.Count == 0) Init();

            var id = BitConverter.ToInt16(new byte[2] {payload[0], payload[1]},0);
            _packetResponseList[id](payload);
        }
    }
}