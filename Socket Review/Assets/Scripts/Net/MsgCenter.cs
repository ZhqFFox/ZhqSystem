using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncGameServer
{
    /// <summary>
    /// 消息中心
    /// </summary>
    public class MsgCenter : Singleton<MsgCenter>
    {
        /// <summary>
        /// 存储注册消息集合
        /// </summary>
        public Dictionary<int, Action<byte[], Client>> dic = new Dictionary<int, Action<byte[], Client>>();

        /// <summary>
        /// 添加消息
        /// </summary>
        /// <param name="id">协议id</param>
        /// <param name="action">协议id对应的消息回调</param>
        public void AddMsg(int id,Action<byte[],Client> action)
        {
            if (!dic.ContainsKey(id))
            {
                dic.Add(id, action);
            }
        }

        /// <summary>
        /// 移除消息
        /// </summary>
        /// <param name="id"></param>
        public void RemoveMsg(int id)
        {
            if (dic.ContainsKey(id))
            {
                dic.Remove(id);
            }
        }

        /// <summary>
        /// 消息广播
        /// </summary>
        /// <param name="id">协议id，也就是消息的key</param>
        /// <param name="msgBody">消息粘包处理后的pb的字节数组</param>
        /// <param name="client">发送消息的客户端对象</param>
        public void BrocastMsg(int id,byte[] msgBody,Client client)
        {
            if (dic.ContainsKey(id))
            {
                Action<byte[], Client> action = dic[id];
                if (action!=null)
                {
                    action(msgBody, client);
                }
            }
        }
    }
}
