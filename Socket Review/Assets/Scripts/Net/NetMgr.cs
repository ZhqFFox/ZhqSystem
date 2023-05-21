using System;
using System.Collections.Generic;
using System.Diagnostics;
//
using System.Net;
using System.Net.Sockets;

namespace AsyncGameServer
{
    /// <summary>
    /// 封装的客户端
    /// </summary>
    public class Client
    {
        /// <summary>
        /// 服务器存储和客户端进行通信的套接字对象socket
        /// </summary>
        public Socket client_Socket;
        /// <summary>
        /// 服务器和对应的客户端进行通信时，存储客户端发送过来的消息
        /// </summary>
        public byte[] receiveBuffer = new byte[1024 * 2];

        /// <summary>
        /// 客户端的唯一id标识
        /// </summary>
        public string unid;

        /// <summary>
        /// 服务器向客户端发送消息
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(int id,byte[] msgBody)
        {
            byte[] msg = PackMsgUtil.PackMsg(id, msgBody);//拼装一条完整的消息，消息头+消息id+消息体
            client_Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, OnSend, null);
        }

        private void OnSend(IAsyncResult ar)
        {
            int len = client_Socket.EndSend(ar);
        }
    }
    /// <summary>
    /// 网络管理
    /// </summary>
    public class NetMgr : Singleton<NetMgr>
    {

        /// <summary>
        /// 服务器套接字对象，用来进行接受客户端的连接
        /// </summary>
        private Socket server_Socket;

        /// <summary>
        /// 存储所有成功连接到服务器的客户端，也就是所有的在线玩家列表
        /// </summary>
        public IList<Client> lstClients = new List<Client>();
        /// <summary>
        ///  初始化游戏服务器
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="port">端口号</param>
        public void InitGameServer(string ip, int port)
        {
            //构造
            server_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //bind
            IPEndPoint iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            server_Socket.Bind(iPEndPoint);
            //listen
            server_Socket.Listen(0);//0,标识不做限制
            //异步接受客户端连接
            server_Socket.BeginAccept(OnAccept, server_Socket);
            UnityEngine.Debug.Log("异步游戏服务器启动成功！");
        }

        private void OnAccept(IAsyncResult ar)
        {
            Socket connetdToserver = server_Socket.EndAccept(ar);//回调处理后，服务器返回一个和客户端进行消息通信的套接字对象
            Client client = new Client();
            client.client_Socket = connetdToserver;
            client.unid = Guid.NewGuid().ToString();//为连接的客户端生成一个唯一id标识
            lstClients.Add(client);//把成功连接到服务器的客户端添加到集合中

            UnityEngine.Debug.Log(string.Format("当前玩家的数量：{0}", lstClients.Count));
            server_Socket.BeginAccept(OnAccept, server_Socket);//进行再次调用
            client.client_Socket.BeginReceive(client.receiveBuffer, 0, client.receiveBuffer.Length, SocketFlags.None, OnReceive, client);//服务器开始接受对应客户端发送过来的消息
        }

        private void OnReceive(IAsyncResult ar)
        {
            Client client = ar.AsyncState as Client;
            try
            {
                int len = client.client_Socket.EndReceive(ar);//返回客户端发送过来的实际的消息长度
                if (len > 0)//客户端正常连接，进行消息的接受
                {
                    byte[] realBuffer = new byte[len];//定义一个实际接收到的客户端的字节数组
                    Buffer.BlockCopy(client.receiveBuffer, 0, realBuffer, 0, len);//从消息缓冲区中获得实际接受的消息字节数组
                    #region 粘包处理
                    if (realBuffer.Length>2)//如果长度小于2表明消息连消息头的值都不够，表明这条消息肯定不完整。只有大于2才能进行下一步的判断
                    {
                        ushort msgLen = BitConverter.ToUInt16(realBuffer,0);//读取消息的长度
                        if (realBuffer.Length-2>=msgLen)//能够确定，至少包含一条完整的消息
                        {
                            byte[] oneFullMsg = new byte[msgLen];//定义一个完整的消息为协议id+pb对象
                            Buffer.BlockCopy(realBuffer, 2, oneFullMsg, 0, msgLen);//从真实接受数据的缓冲区中进行取值
                           
                            
                            //---------------------为什么不放在队列里？？？
                            int id = BitConverter.ToInt32(oneFullMsg, 0);//解析消息id
                            byte[] msgBody = new byte[msgLen - 4];//为什么减去4，这是因为协议id是int类型，这4个字节是留个他用的
                            Buffer.BlockCopy(oneFullMsg, 4, msgBody, 0, msgBody.Length);

                            //通过消息中心进行广播
                            MsgCenter.Instance.BrocastMsg(id, msgBody, client);
                        
                        }
                    }
                    #endregion

                }
                if (len==0)//表明客户端正常下线
                {
                    NotifyOffline(client);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            client.client_Socket.BeginReceive(client.receiveBuffer, 0, client.receiveBuffer.Length, SocketFlags.None, OnReceive, client);//服务器开始接受对应客户端发送过来的消息
        }

        /// <summary>
        /// 下线通知
        /// </summary>
        /// <param name="client"></param>
        private void NotifyOffline(Client client)
        {
            UnityEngine.Debug.Log(string.Format("下线客户端是：{0}", client.unid));
            lstClients.Remove(client);//从在线玩家中进行移除
        }

        /// <summary>
        ///  广播消息，向所有的在线客户端发送消息
        /// </summary>
        /// <param name="id">消息id</param>
        /// <param name="msgBody">pb对象序列化后的字节数组</param>
        public void BrocastToAllClients(int id,byte[] msgBody)
        {
            for (int i = 0; i < lstClients.Count; i++)
            {
                lstClients[i].SendMsg(id,msgBody);
            }
        }
    }
}
