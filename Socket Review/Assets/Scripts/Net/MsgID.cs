using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncGameServer
{
    /// <summary>
    /// 协议id
    /// </summary>
    public class MsgID
    {
        public const int C2S_OnConnected = 1001;

        public const int S2C_SyncPlaneInfo = 1002;
    }
}
