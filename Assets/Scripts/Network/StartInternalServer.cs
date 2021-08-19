using System;
using industrialization.Core.GameSystem;
using Util;

namespace Network
{
    public class StartInternalServer : SingletonMonoBehaviour<StartServerCommunication>
    {
        public void FixedUpdate()
        {
            GameUpdate.Update();
        }
    }
}