using System;
using System.Collections.Generic;
using Network.Util;
using UnityEngine;

namespace Network.ResponsePacket
{
    public static class BlockCoordinateResponse
    {
        public const int DefaultChunkSize = 4;
        public delegate void BlockCoordinateResponseEvent(int[,] id, int[,] intId);
        private static event BlockCoordinateResponseEvent ResponseEvent;
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
                idList[instX%DefaultChunkSize,instY%DefaultChunkSize] = responseAnalysis.MoveNextToGetInt();
                intIdList[instX%DefaultChunkSize,instY%DefaultChunkSize] = responseAnalysis.MoveNextToGetInt();
                
            }
            ResponseEvent(idList,intIdList);
        }

        public static void SubscribeEvent(BlockCoordinateResponseEvent @event)
        {
            ResponseEvent += @event;
        }
    }
}