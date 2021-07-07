using System;
using Network.Util;
using UnityEngine;

namespace Network.ResponsePacket
{
    public static class InstallationCoordinateResponse
    {
        public delegate void InstallationCoordinateResponseEvent(int x, int y, int id, int intId);
        private static event InstallationCoordinateResponseEvent ResponseEvent;
        public static void AnalysisResponse(byte[] payload)
        {
            var responseAnalysis = new ByteArrayEnumerator(payload);
            //パケットの解析
            responseAnalysis.MoveNextToGetShort();
            var num = responseAnalysis.MoveNextToGetInt();
            responseAnalysis.MoveNextToGetShort();
            var chunkX = responseAnalysis.MoveNextToGetInt();
            var chunkY = responseAnalysis.MoveNextToGetInt();
            Debug.Log($"チャンク座標 {chunkX} {chunkY}");
            //建物一覧を取得
            for (int i = 0; i < num; i++)
            {
                var instX = responseAnalysis.MoveNextToGetInt();
                var instY = responseAnalysis.MoveNextToGetInt();
                var id = responseAnalysis.MoveNextToGetInt();
                var intId = responseAnalysis.MoveNextToGetInt();
                
                Debug.Log($"建物 ID:{id} 座標:({instX} ,{instY}) intId:{intId}");
                ResponseEvent(instX,instY,id,intId);
            }
        }

        public static void SubscribeEvent(InstallationCoordinateResponseEvent @event)
        {
            ResponseEvent += @event;
        }
    }
}