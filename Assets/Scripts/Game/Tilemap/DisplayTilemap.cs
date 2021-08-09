using System;
using System.Collections.Generic;
using Game.Player;
using Network;
using Network.ResponsePacket;
using Network.Util;
using UnityEngine;

namespace Game.Tilemap
{
    //TODO この辺のタイルマップの処理をきちんと考える
    public class DisplayTilemap : MonoBehaviour
    {
        private const int DisplayRange = 8;
        
        //座標がキーのデータストア
        private static readonly Dictionary<Coordinate,Block> CoordinateDictionary = new Dictionary<Coordinate, Block>();
        private static readonly Dictionary<Coordinate,GameObject> GameObjectsDictionary = new Dictionary<Coordinate, GameObject>();

        private void Start()
        {
            BlockCoordinateResponse.SubscribeEvent(InstallationCoordinateResponseEvent);
        }

        private void Update()
        {
            //リクエスト処理
            var cam = CameraTransformController.Instance.transform.position;
            int startChunkX = (Mathf.RoundToInt(cam.x)-
                               DisplayRange )/
                BlockCoordinateResponse.DefaultChunkSize * BlockCoordinateResponse.DefaultChunkSize;
            int endChunkX = (Mathf.RoundToInt(cam.x)+
                             DisplayRange )/
                BlockCoordinateResponse.DefaultChunkSize * BlockCoordinateResponse.DefaultChunkSize;
            int startChunkY = (Mathf.RoundToInt(cam.y)-
                               DisplayRange )/
                BlockCoordinateResponse.DefaultChunkSize * BlockCoordinateResponse.DefaultChunkSize;
            int endChunkY = (Mathf.RoundToInt(cam.y)+
                             DisplayRange )/
                BlockCoordinateResponse.DefaultChunkSize * BlockCoordinateResponse.DefaultChunkSize;
            for (int i = startChunkX; i < endChunkX; i+=BlockCoordinateResponse.DefaultChunkSize)
            {
                for (int j = startChunkY; j < endChunkY; j+=BlockCoordinateResponse.DefaultChunkSize)
                {
                    SendInstallationRequest(i, j);
                }
            }
            //表示処理
            for (int i = Mathf.RoundToInt(cam.x) - DisplayRange; i < Mathf.RoundToInt(cam.x) + DisplayRange; i++)
            {
                for (int j = Mathf.RoundToInt(cam.x) - DisplayRange; j < Mathf.RoundToInt(cam.x) + DisplayRange; j++)
                {
                    var co = new Coordinate {x = i, y = j};
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

        void InstallationCoordinateResponseEvent(int[,] id, int[,] intId)
        {
            for (int i = 0; i < BlockCoordinateResponse.DefaultChunkSize; i++)
            {
                for (int j = 0; j < BlockCoordinateResponse.DefaultChunkSize; j++)
                {
                    //TODO ここの座標修正
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