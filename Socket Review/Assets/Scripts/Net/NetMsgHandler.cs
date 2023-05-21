//
namespace AsyncGameServer
{
    /// <summary>
    /// 网络消息处理中心
    /// </summary>
    public class NetMsgHandler : Singleton<NetMsgHandler>
    {
        /// <summary>
        /// 注册消息
        /// </summary>
        public void RegisterMsg()
        {
            MsgCenter.Instance.AddMsg(MsgID.C2S_OnConnected, OnClientConnected);
        }

        private void OnClientConnected(byte[] arg1, Client arg2)
        {
            UnityEngine.Debug.Log("连接成功");
            LocalMsgCenter.Instance.Broadcast("OnClientConnected",arg2);

        }
    }
}
