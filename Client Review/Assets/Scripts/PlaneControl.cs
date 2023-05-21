using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LocalMsgCenter.Instance.AddListener("OnSyncPlanePos", OnSyncPlanePos);
    }

    private void OnSyncPlanePos(object obj)
    {
        object[] ob=obj as object[];
        S2C_SyncPlaneInfo s2C_SyncPlaneInfo = ob[0] as S2C_SyncPlaneInfo;
        Vector3 pos = new Vector3(s2C_SyncPlaneInfo.Pos.X, s2C_SyncPlaneInfo.Pos.Y, s2C_SyncPlaneInfo.Pos.Z);
        Vector3 angle = new Vector3(s2C_SyncPlaneInfo.Rot.X, s2C_SyncPlaneInfo.Rot.Y, s2C_SyncPlaneInfo.Rot.Z);
        transform.position = pos;
        transform.eulerAngles = angle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
