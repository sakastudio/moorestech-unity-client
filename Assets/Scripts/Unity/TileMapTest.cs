using System;
using System.Collections;
using System.Collections.Generic;
using industrialization.Server.Util;
using Network;
using Network.ResponsePacket;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class TileMapTest : MonoBehaviour
{
    [SerializeField] private Tilemap machinesTilemap;
    [SerializeField] private TileBase machinesTile;
    // Start is called before the first frame update
    void Start()
    {
        InstallationCoordinateResponse.SubscribeEvent(InstallationCoordinateResponseEvent);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Vector3でマウス位置座標を取得する
            var position = Input.mousePosition;
            // Z軸修正
            position.z = 10f;
            // マウス位置座標をスクリーン座標からワールド座標に変換する
            var screen = Camera.main.ScreenToWorldPoint(position);
            
            var send = new List<byte>();
            send.AddRange(ByteArrayConverter.ToByteArray((short)1));
            send.AddRange(ByteArrayConverter.ToByteArray(0));
            send.AddRange(ByteArrayConverter.ToByteArray((short)0));
            send.AddRange(ByteArrayConverter.ToByteArray(Mathf.RoundToInt(screen.x)));
            send.AddRange(ByteArrayConverter.ToByteArray(Mathf.RoundToInt(screen.y)));
            send.AddRange(ByteArrayConverter.ToByteArray(Guid.Empty));
            send.AddRange(ByteArrayConverter.ToByteArray(Guid.Empty));
        
            PacketHandler.SendMessages(send.ToArray());
        }
    }
    void InstallationCoordinateResponseEvent(int x, int y, int id, Guid guid)
    {
        machinesTilemap.SetTile(new Vector3Int(x,y,0),machinesTile);
    }
}
