using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Network;
using UnityEngine;

public class StartPacketHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Thread thread = new Thread(new ThreadStart(PacketHandler.StartSocketClient));
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}