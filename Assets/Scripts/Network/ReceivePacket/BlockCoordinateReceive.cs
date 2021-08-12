using System.Collections.Generic;
using Network.Util;
using Util.Block;

namespace Network.ReceivePacket
{
    public static class BlockCoordinateReceive
    {
        public delegate void BlockCoordinateResponseEvent(Dictionary<Coordinate, BlockData> blockData);
        private static event BlockCoordinateResponseEvent ReceiveEvent;
        public static void AnalysisResponse(byte[] payload)
        {
            var responseAnalysis = new ByteArrayEnumerator(payload);
            //パケットの解析
            responseAnalysis.MoveNextToGetShort();
            var num = responseAnalysis.MoveNextToGetInt();
            responseAnalysis.MoveNextToGetShort();
            var chunkX = responseAnalysis.MoveNextToGetInt();
            var chunkY = responseAnalysis.MoveNextToGetInt();

            var blockData = new Dictionary<Coordinate, BlockData>();
            //建物一覧を取得
            for (int i = 0; i < num; i++)
            {
                var instX = responseAnalysis.MoveNextToGetInt();
                var instY = responseAnalysis.MoveNextToGetInt();
                var blockId = responseAnalysis.MoveNextToGetInt();
                var intId = responseAnalysis.MoveNextToGetInt();
                
                blockData.Add(
                    new Coordinate{x = instX,y = instY},
                    new BlockData{id = blockId,intId = intId}
                );
            }

            if (ReceiveEvent != null) ReceiveEvent(blockData);
        }

        public static void SubscribeEvent(BlockCoordinateResponseEvent @event)
        {
            ReceiveEvent += @event;
        }
        public static void UnSubscribeEvent(BlockCoordinateResponseEvent @event)
        {
            ReceiveEvent -= @event;
        }
    }
}