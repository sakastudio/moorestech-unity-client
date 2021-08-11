using System;
using System.Collections.Generic;
using Const;
using Network;
using Network.Util;
using UnityEngine;

namespace ViewModel.Block
{
    public class BlockDataManager
    {
        private static readonly Dictionary<Coordinate,Block> CoordinateDictionary = new Dictionary<Coordinate, Block>();
        public int[,] GetBlocks(Vector2Int start,Vector2Int end)
        {
            SendBlockRequestPacket(start, end);
            return CollectBlockData(start, end);
        }

        private void SendBlockRequestPacket(Vector2Int start,Vector2Int end)
        {
            //リクエスト処理
            int startChunkX = Mathf.RoundToInt(start.x)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            int endChunkX = Mathf.RoundToInt(end.x)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            int startChunkY = Mathf.RoundToInt(start.y)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            int endChunkY = Mathf.RoundToInt(end.y)/
                ConstData.DefaultChunkSize * ConstData.DefaultChunkSize;
            for (int i = startChunkX; i < endChunkX; i+=ConstData.DefaultChunkSize)
            {
                for (int j = startChunkY; j < endChunkY; j+=ConstData.DefaultChunkSize)
                {
                    SendInstallationRequest(i, j);
                }
            }
        }
        void SendInstallationRequest(int chunkX, int chunkY)
        {
            var payload = new List<byte>();
            //パケットIDの挿入
            short id = 2;
            payload.AddRange(ByteArrayConverter.ToByteArray(id));
            payload.Add(0);
            payload.Add(0);
            payload.AddRange(ByteArrayConverter.ToByteArray(chunkX));
            payload.AddRange(ByteArrayConverter.ToByteArray(chunkY));
        
            PacketHandler.SendMessages(payload.ToArray());
        }
        
        private int[,] CollectBlockData(Vector2Int start,Vector2Int end)
        {
            
            var xSize = Math.Abs(start.x - end.x);
            var ySize = Math.Abs(start.y - end.y);
            var blocks = new int[xSize,ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    var c = new Coordinate {x = i + start.x, y = i + start.y};
                    if (CoordinateDictionary.ContainsKey(c))
                    {
                        blocks[i, j] = CoordinateDictionary[c].id;
                    }
                    else
                    {
                        blocks[i, j] = -1;
                    }
                }
            }
            return blocks;
        }
        
        
        
        
        private struct Coordinate
        {
            public int x;
            public int y;
        }
        private struct Block
        {
            public int id;
            public int intId;
        }
    }
}
