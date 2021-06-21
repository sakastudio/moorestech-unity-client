using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        var payload = new List<byte>();
        //パケットIDの挿入
        short id = 1;
        BitConverter.GetBytes(id).ToList().ForEach(b => payload.Add(b));
        
        BitConverter.GetBytes(int.Parse(InstallationID.text)).ToList().ForEach(b => payload.Add(b));
        BitConverter.GetBytes(int.Parse(PutX.text)).ToList().ForEach(b => payload.Add(b));
        BitConverter.GetBytes(int.Parse(PutY.text)).ToList().ForEach(b => payload.Add(b));
        payload.Add(0);
        payload.Add(0);
        Guid.Empty.ToByteArray().ToList().ForEach(b => payload.Add(b));
        Guid.Empty.ToByteArray().ToList().ForEach(b => payload.Add(b));
        
        AsynchronousClient.Send(payload.ToArray());
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
        
        AsynchronousClient.Send(payload.ToArray());
    }
}
