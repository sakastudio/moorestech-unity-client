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
        Thread thread = new Thread(PacketHandler.StartSocketClient);
        thread.Start();
    }
}
