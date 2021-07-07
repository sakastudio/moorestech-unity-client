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

    private List<PutReservation> _putReservationList = new List<PutReservation>();
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
            var tile = machinesTilemap.WorldToCell(Camera.main.ScreenToWorldPoint(position));
            
            var send = new List<byte>();
            send.AddRange(ByteArrayConverter.ToByteArray((short)1));
            send.AddRange(ByteArrayConverter.ToByteArray(0));
            send.AddRange(ByteArrayConverter.ToByteArray((short)0));
            send.AddRange(ByteArrayConverter.ToByteArray(tile.x));
            send.AddRange(ByteArrayConverter.ToByteArray(tile.y));
            send.AddRange(ByteArrayConverter.ToByteArray(Guid.Empty));
            send.AddRange(ByteArrayConverter.ToByteArray(Guid.Empty));
        
            PacketHandler.SendMessages(send.ToArray());
        }

        foreach (var putReservation in _putReservationList)
        {
            machinesTilemap.SetTile(new Vector3Int(putReservation.X,putReservation.Y,0),machinesTile);
        }

        _putReservationList.Clear();
    }

    private void FixedUpdate()
    {
        GetData();
    }

    public void GetData()
    {
        for (int i = -10; i <= 10; i+=10)
        {
            for (int j = -10; j <= 10; j+=10)
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
    void InstallationCoordinateResponseEvent(int x, int y, int id, Guid guid)
    {
        _putReservationList.Add(new PutReservation(x,y,0));
    }
}

class PutReservation
{
    public readonly int X;
    public readonly int Y;
    public readonly int id;

    public PutReservation(int x, int y, int id)
    {
        X = x;
        Y = y;
        this.id = id;
    }
}