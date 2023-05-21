using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
//
using Google.Protobuf;

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
        MsgCenter.Instance.AddMsg(MsgID.S2C_SyncPlaneInfo, SyncPlaneInfo);
    }

    private void SyncPlaneInfo(byte[] obj)
    {
        S2C_SyncPlaneInfo s2c_SyncPlaneInfo= S2C_SyncPlaneInfo.Parser.ParseFrom(obj);

        LocalMsgCenter.Instance.Broadcast("OnSyncPlanePos", s2c_SyncPlaneInfo);
    }
}