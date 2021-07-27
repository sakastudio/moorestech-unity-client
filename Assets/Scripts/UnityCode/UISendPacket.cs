using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using industrialization.Server.Util;
using Network;
using UnityEngine;
using UnityEngine.UI;

public class UISendPacket : MonoBehaviour
{
    [SerializeField] private InputField InstallationID;
    [SerializeField] private InputField PutX;
    [SerializeField] private InputField PutY;
    [SerializeField] private InputField GetX;
    [SerializeField] private InputField GetY;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Put()
    {
        //パケットIDの挿入

        var send = new List<byte>();
        send.AddRange(ByteArrayConverter.ToByteArray((short)1));
        send.AddRange(ByteArrayConverter.ToByteArray(int.Parse(InstallationID.text)));
        send.AddRange(ByteArrayConverter.ToByteArray((short)0));
        send.AddRange(ByteArrayConverter.ToByteArray(int.Parse(PutX.text)));
        send.AddRange(ByteArrayConverter.ToByteArray(int.Parse(PutY.text)));
        send.AddRange(ByteArrayConverter.ToByteArray(Int32.MaxValue));
        send.AddRange(ByteArrayConverter.ToByteArray(Int32.MaxValue));
        
        PacketHandler.SendMessages(send.ToArray());
    }

    public void Get()
    {
        var payload = new List<byte>();
        //パケットIDの挿入
        short id = 2;
        BitConverter.GetBytes(id).ToList().ForEach(b => payload.Add(b));
        
        payload.Add(0);
        payload.Add(0);
        BitConverter.GetBytes(int.Parse(GetX.text)).ToList().ForEach(b => payload.Add(b));
        BitConverter.GetBytes(int.Parse(GetY.text)).ToList().ForEach(b => payload.Add(b));
        
        PacketHandler.SendMessages(payload.ToArray());
    }
}
