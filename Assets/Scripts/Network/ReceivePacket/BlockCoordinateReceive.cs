using Network.Util;
using Util.Block;

namespace Network.ReceivePacket
{
    public static class BlockCoordinateReceive
    {
        public delegate void BlockCoordinateResponseEvent(int[,] id, int[,] intId);
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

            var idList = new int[4,4];
            var intIdList = new int[4,4];
            //建物一覧を取得
            for (int i = 0; i < num; i++)
            {
                var instX = responseAnalysis.MoveNextToGetInt();
                var instY = responseAnalysis.MoveNextToGetInt();
                idList[instX%ConstData.DefaultChunkSize,instY%ConstData.DefaultChunkSize] = responseAnalysis.MoveNextToGetInt();
                intIdList[instX%ConstData.DefaultChunkSize,instY%ConstData.DefaultChunkSize] = responseAnalysis.MoveNextToGetInt();
                
            }

            if (ReceiveEvent != null) ReceiveEvent(idList, intIdList);
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