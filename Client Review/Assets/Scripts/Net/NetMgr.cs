using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;

/// <summary>
/// 网络管理
/// </summary>
public class NetMgr : Singleton<NetMgr>
{
    /// <summary>
    /// 存储服务器发送过来的消息
    /// </summary>
    public Queue<byte[]> queMsg = new Queue<byte[]>();
    /// <summary>
    /// 和服务器进行消息通信的套接字对象
    /// </summary>
    public Socket client_Socket;
    /// <summary>
    /// 存储服务器发送过来的数据
    /// </summary>
    byte[] receivedBuffer = new byte[1024 * 2];//


    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="id">消息id</param>
    /// <param name="msgBody">消息体</param>
    public void SendMsg(int id, byte[] msgBody)
    {
        byte[] msg = PackMsgUtil.PackMsg(id, msgBody);//拼装一条完整的消息，消息头+消息id+消息体
        client_Socket.BeginSend(msg, 0, msg.Length, SocketFlags.None, OnSend, null);
    }

    private void OnSend(IAsyncResult ar)
    {
        int len = client_Socket.EndSend(ar);
        Debug.Log("发送数据成功，数据长度为:"+len);
    }

    /// <summary>
    /// 连接服务器
    /// </summary>
    /// <param name="ip"></param>
    /// <param name="port"></param>
    public void ConnectToServer(string ip, int port)
    {
        client_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        client_Socket.BeginConnect(ip, port, OnConnect, null);

    }

    private void OnConnect(IAsyncResult ar)
    {
        client_Socket.EndConnect(ar);
        SendMsg(MsgID.C2S_OnConnected,new byte[4]);
        client_Socket.BeginReceive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None, OnReceive, null);
    }

    private void OnReceive(IAsyncResult ar)
    {
        int len = client_Socket.EndReceive(ar);
        byte[] realBuffer = new byte[len];//客户端受服务器接发送过来的实际的字节数组
        Buffer.BlockCopy(receivedBuffer, 0, realBuffer, 0, len);//从消息缓冲区中
        #region  粘包处理
        if (realBuffer.Length > 2)//如果长度小于2表明消息连消息头的值都不够，表明这条消息肯定不完整。只有大于2才能进行下一步的判断
        {
            ushort msgLen = BitConverter.ToUInt16(realBuffer, 0);//读取消息的长度
            if (realBuffer.Length - 2 >= msgLen)//能够确定，至少包含一条完整的消息
            {
                byte[] oneFullMsg = new byte[msgLen];//定义一个完整的消息为协议id+pb对象
                Buffer.BlockCopy(realBuffer, 2, oneFullMsg, 0, msgLen);//从真实接受数据的缓冲区中进行取值
                queMsg.Enqueue(oneFullMsg);
            }
        }
        #endregion
        client_Socket.BeginReceive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None, OnReceive, null);
    }

    /// <summary>
    /// 更新处理消息
    /// </summary>
    public void UpdateHandleMsg()
    {
        if (queMsg.Count>0)
        {
            byte[] oneFullMsg = queMsg.Dequeue();
            int id = BitConverter.ToInt32(oneFullMsg, 0);//解析消息id
            byte[] msgBody = new byte[oneFullMsg.Length - 4];//为什么减去4，这是因为协议id是int类型，这4个字节是留个他用的
            Buffer.BlockCopy(oneFullMsg, 4, msgBody, 0, msgBody.Length);
            //通过消息中心进行广播
            MsgCenter.Instance.BrocastMsg(id, msgBody);
        }
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void Close()
    {
        if (client_Socket != null && client_Socket.Connected)
        {
            client_Socket.Shutdown(SocketShutdown.Both);
            client_Socket.Close();
        }
    }
}
