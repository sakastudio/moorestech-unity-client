using System;
using System.Collections.Generic;
using Network;
using Network.ResponsePacket;
using Network.Util;
using UnityEngine;

namespace Game.Tilemap
{
    public class DisplayTilemap : MonoBehaviour
    {
        //座標がキーのデータストア
        private static readonly Dictionary<Coordinate,Block> CoordinateDictionary = new Dictionary<Coordinate, Block>();

        private void Start()
        {
            BlockCoordinateResponse.SubscribeEvent(InstallationCoordinateResponseEvent);
        }

        private void Update()
        {
            //TODO　表示処理
            //TODO リクエスト処理
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

        void InstallationCoordinateResponseEvent(int[,] id, int[,] intId)
        {
            for (int i = 0; i < BlockCoordinateResponse.DefaultChunkSize; i++)
            {
                for (int j = 0; j < BlockCoordinateResponse.DefaultChunkSize; j++)
                {
                    CoordinateDictionary.Add(
                        new Coordinate {x=i,y=j},
                        new Block {id=id[i,j],intId=intId[i,j]});
                }
            }
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