using AsyncGameServer;
using DG.Tweening;
using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaneControl : MonoBehaviour
{
    public List<Vector3> targetPos;
    public int targetCount=50;
    public float r=10;
    public float ang;

    public float height;


    public  bool isAuto=true;
    public bool isSyncPos=false;
    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        LocalMsgCenter.Instance.AddListener("OnClientConnected", OnClinetConnected);
        targetPos = new List<Vector3>();
        ang = 2*Mathf.PI/ targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            float x = Mathf.Sin(ang*i) * r;
            float z=Mathf.Cos(ang*i) * r;
            targetPos.Add(new Vector3(x, height, z));
            

        }
        transform.position = targetPos[0];


    }

    public void OnChangePlaneMode(bool flag)
    {
 
        isAuto = flag;
    }

    private void OnClinetConnected(object obj)
    {
        isSyncPos = true;

    }

    private int crtIndex = 0;
    Quaternion startQua;
    float totalDis;
    /// <summary>
    /// 自动飞行
    /// </summary>
    public void AutoFly()
    {
        float crtDis = Vector3.Distance(transform.position, targetPos[crtIndex]);
        if (crtDis <= 0.05f)
        {
            crtIndex++;

            startQua = transform.rotation;
            totalDis = Vector3.Distance(targetPos[crtIndex], transform.position);
            if (crtIndex == targetPos.Count - 1)
            {
                crtIndex = 0;
            }
        }
        else
        {
            Quaternion qua = Quaternion.LookRotation(targetPos[crtIndex] - transform.position);
            transform.rotation = Quaternion.Slerp(startQua, qua, /*timer */(totalDis - crtDis) / totalDis);
            transform.position = Vector3.MoveTowards(transform.position, targetPos[crtIndex], Time.deltaTime * speed);
        }
    }
    /// <summary>
    /// 手动操作
    /// </summary>
    public void HandControl()
    {
        transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * speed);
        transform.Rotate(Input.GetAxis("Horizontal") * transform.up * Time.deltaTime * 40);
        if (transform.rotation.z<=30&&transform.rotation.z>=-30)
        {
            float mouseY = Input.mousePosition.y;
            float mouseX = Input.mousePosition.x;
            float rotZ;
            float rotX;
            if (mouseY >= Screen.height/2)
            {
                rotX = -(mouseY - Screen.height / 2) / (Screen.height / 2)*30;
            }
            else
            {
                rotX = ( Screen.height- mouseY)  / Screen.height * 30;
            }


            if (mouseX >= Screen.width / 2)
            {
                rotZ = -(mouseX - Screen.height / 2) / (Screen.height / 2) * 30;
                Debug.Log(mouseX+":::::"+ Screen.height);
            }
            else
            {
                rotZ = (Screen.height - mouseX) / Screen.height* 30;

            }

            transform.localEulerAngles = new Vector3(rotX, transform.localEulerAngles.y, rotZ);
        }



    }

    /// <summary>
    /// 同步位置给客户端
    /// </summary>
    public void SyncPosToClient()
    {

        S2C_SyncPlaneInfo s2C_SyncPlaneInfo = new S2C_SyncPlaneInfo();
        s2C_SyncPlaneInfo.Pos = new Pos();
        s2C_SyncPlaneInfo.Rot = new Rot();
        s2C_SyncPlaneInfo.Pos.X = transform.position.x;
        s2C_SyncPlaneInfo.Pos.Y = transform.position.y;
        s2C_SyncPlaneInfo.Pos.Z = transform.position.z;

        s2C_SyncPlaneInfo.Rot.X = transform.localEulerAngles.x;
        s2C_SyncPlaneInfo.Rot.Y = transform.localEulerAngles.y;
        s2C_SyncPlaneInfo.Rot.Z = transform.localEulerAngles.z;
        NetMgr.Instance.BrocastToAllClients(MsgID.S2C_SyncPlaneInfo, s2C_SyncPlaneInfo.ToByteArray());
    }




    
   
    // Update is called once per frame
    void Update()
    {
        if (isAuto)
        {
            AutoFly();

        }
        else
        {
            HandControl();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            speed += 1;
            Debug.Log("当前速度:" + speed);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            if (speed > 0)
            {
                speed -= 1;
                Debug.Log("当前速度:" + speed);

            }

        }
        if (isSyncPos)
        {
            SyncPosToClient();
        }
    }


}
