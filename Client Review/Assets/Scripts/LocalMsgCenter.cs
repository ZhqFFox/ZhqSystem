using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalMsgCenter : Singleton<LocalMsgCenter>
{
    public Dictionary<string, Action<object>> dic = new Dictionary<string, Action<object>>();
    public void AddListener(string id, Action<object> callback)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] += callback;
        }
        else
        {
            dic.Add(id, callback);
        }
    }
    public void RemoveListener(string id, Action<object> callback)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] -= callback;
            if (dic[id] == null)
            {
                dic.Remove(id);
            }
        }
    }
    public void Broadcast(string id, params object[] obj)
    {
        if (dic.ContainsKey(id))
        {
            dic[id](obj);
        }
    }
}
