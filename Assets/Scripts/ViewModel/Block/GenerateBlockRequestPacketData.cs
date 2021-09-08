using System.Collections.Generic;
using Network;
using Network.Util;
using UnityEngine;
using Util.Block;

namespace ViewModel.Block
{
    public static class GenerateBlockRequestPacketData
    {
        public static List<byte[]> Generate(Vector2Int start,Vector2Int end)
        {
            int startChunkX = Mathf.RoundToInt(start.x)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            int endChunkX = Mathf.RoundToInt(end.x)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            int startChunkY = Mathf.RoundToInt(start.y)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            int endChunkY = Mathf.RoundToInt(end.y)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            
            
            var result =  new List<byte[]>();
            for (int i = startChunkX; i <= endChunkX; i+=ConstData.DefaultChunkSize)
            {
                for (int j = startChunkY; j <= endChunkY; j+=ConstData.DefaultChunkSize)
                {
                    result.Add(GenerateByteArray(i, j));
                }
            }
            
            return result;
        }
        public static byte[] GenerateByteArray(int chunkX, int chunkY)
        {
            var payload = new List<byte>();
            //パケットIDの挿入
            short id = 2;
            payload.AddRange(ByteArrayConverter.ToByteArray(id));
            payload.Add(0);
            payload.Add(0);
            payload.AddRange(ByteArrayConverter.ToByteArray(chunkX));
            payload.AddRange(ByteArrayConverter.ToByteArray(chunkY));

            return payload.ToArray();
        }
    }
}