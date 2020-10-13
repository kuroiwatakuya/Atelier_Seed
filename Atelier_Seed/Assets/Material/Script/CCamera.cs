using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCamera : MonoBehaviour
{
    //オブジェクト取得
    private GameObject Player;
    public bool CameraMoveFlag;  //カメラ追従フラグ
    public float CameraMin;     //カメラ左端
    public float CameraMax;    //カメラ右端

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 PlayerPosition = Player.transform.position;
        
        //カメラが左端に到達したら動かさない
        if (transform.position.x >= CameraMin)
        {
            CameraMoveFlag = false;
        }
        //カメラが右端に到達したら動かさない
        if(transform.position.x <= CameraMax)
        {
            CameraMoveFlag = false;
        }
        
        //移動
        if(PlayerPosition.x >= CameraMin && PlayerPosition.x <= CameraMax)
        {
            CameraMoveFlag = true;
            transform.position = new Vector3(PlayerPosition.x, 0.0f, -10.0f);
        }
    }
}
