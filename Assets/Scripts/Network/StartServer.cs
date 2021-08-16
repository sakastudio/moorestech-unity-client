using System.Threading;
using UnityEngine;
using Util;

namespace Network
{
    public class StartServer : SingletonMonoBehaviour<StartServer>
    {
        // Start is called before the first frame update
        void Start()
        {
            var t = new Thread(PacketHandler.StartSocketClient);
            t.Start();
        }
    }
}
