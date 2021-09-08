using System;
using System.Collections.Generic;
using System.Linq;
using Network;
using Network.ReceivePacket;
using Network.Util;
using UnityEngine;
using Util;
using Util.Block;

namespace ViewModel.Block
{
    public class BlockDataManager : Singleton<BlockDataManager>
    {
        private readonly Dictionary<Coordinate,BlockData> CoordinateDictionary = new Dictionary<Coordinate, BlockData>();
        public int[,] GetBlocks(Vector2Int start,Vector2Int end)
        {
            if(!isSubscribe) BlockCoordinateReceive.SubscribeEvent(ReceiveBlockDataEvent);
            
            //ブロックデータのリクエストパケットを送信する
            GenerateBlockRequestPacketData.Generate(start, end).ForEach(PacketHandler.SendMessages);
            
            return CollectBlock.Collect(start, end, CoordinateDictionary);
        }

        private bool isSubscribe = false;
        private void ReceiveBlockDataEvent(Dictionary<Coordinate, BlockData> blockData)
        {
            var startChunk = new Vector2Int(
                blockData.First().Key.x /
                ConstData.DefaultChunkSize * 
                ConstData.DefaultChunkSize,
                blockData.First().Key.y /
                ConstData.DefaultChunkSize * 
                ConstData.DefaultChunkSize);
            for (int i = startChunk.x; i < startChunk.x + ConstData.DefaultChunkSize; i++)
            {
                for (int j = startChunk.x; j < startChunk.x + ConstData.DefaultChunkSize; j++)
                {
                    var c = new Coordinate {x = i, y = j};
                    if (blockData.ContainsKey(c))
                    {
                        if (CoordinateDictionary.ContainsKey(c))
                        {
                            CoordinateDictionary[c] = blockData[c];
                        }
                        else
                        {
                            CoordinateDictionary.Add(c,blockData[c]);
                        }
                    }
                    else
                    {
                        CoordinateDictionary.Remove(c);
                    }
                    
                }
            }
        }
    }
}
