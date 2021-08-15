using System;
using System.Collections.Generic;
using Network.ReceivePacket;

namespace Network
{
    //TODO この辺の解析とかをインターフェースにしてテストコードを書く
    public static class ReceivePacketAnalysis
    {
        delegate void PacketAnalysis(byte[] payload);
        private static readonly List<PacketAnalysis> PacketAnalysisList = new List<PacketAnalysis>();

        private static void Init()
        {
            PacketAnalysisList.Add(DummyReceive.AnalysisResponse);
            PacketAnalysisList.Add(BlockCoordinateReceive.AnalysisResponse);
        }
        
        public static void Analysis(byte[] payload)
        {
            if (PacketAnalysisList.Count == 0) Init();

            var id = BitConverter.ToInt16(new byte[2] {payload[0], payload[1]},0);
            PacketAnalysisList[id](payload);
        }
    }
}