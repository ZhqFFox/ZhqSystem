using AsyncGameServer;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetMgr.Instance.InitGameServer("127.0.0.1",6500);
        NetMsgHandler.Instance.RegisterMsg();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
