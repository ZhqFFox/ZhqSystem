using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
using System.Linq;
using System;
/// <summary>
/// 拼装消息的帮助类
/// </summary>
public class PackMsgUtil 
{
    /// <summary>
    /// 组装消息：消息头+消息id+消息body
    /// </summary>
    /// <param name="id"></param>
    /// <param name="msgBody"></param>
    /// <returns></returns>
    public static byte[] PackMsg(int id,byte[] msgBody)
    {
        ushort msgLen = (ushort)(4 + msgBody.Length);//获得消息的长度
        byte[] msgLenBuffer = BitConverter.GetBytes(msgLen);//获得消息长度的字节数组
        byte[] idBuffer = BitConverter.GetBytes(id);//获得协议id的字节数组
        byte[] fullMsgBuffer = new byte[0];//记住这里是0
        fullMsgBuffer = fullMsgBuffer.Concat(msgLenBuffer).ToArray();//1. 组装消息头
        fullMsgBuffer = fullMsgBuffer.Concat(idBuffer).ToArray();//2. 组装消息id
        fullMsgBuffer = fullMsgBuffer.Concat(msgBody).ToArray();//3. 组装pb消息体
        return fullMsgBuffer;
    }
}
