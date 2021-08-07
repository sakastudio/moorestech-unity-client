using System.Threading;
using Network;
using UnityEngine;

namespace Game
{
    public class StartPacketHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Thread thread = new Thread(PacketHandler.StartSocketClient);
            thread.Start();
        }
    }
}
