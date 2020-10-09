using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    //プレイヤー
    private GameObject Player;

    private CPlayerScript PlayerScript;

    public float Angle  = -1;

    public bool Plus = false;
    public bool Minus = true;

    public float NowRotation;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        PlayerScript = Player.GetComponent<CPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (PlayerScript.GunFlag)
        {

            Quaternion Rot = Quaternion.AngleAxis(Angle, Vector3.forward);
            Quaternion NowRot = this.transform.rotation;
            this.transform.rotation = NowRot * Rot;
            if (gameObject.transform.eulerAngles.z >= 90 && gameObject.transform.eulerAngles.z <= 180)
            {
                Plus = false;
                Minus = true;
                Angle = -1;
            }
            if (gameObject.transform.eulerAngles.z <= 0 || gameObject.transform.eulerAngles.z > 180)
            {
                Plus = true;
                Minus = false;
                Angle = 1;
            }

            /*if(Input)
            float rad = gameObject.transform.eulerAngles.z * Mathf.Deg2Rad; //角度をラジアン角に変換
            //rad(ラジアン角)から発射用ベクトルを作成
            double addforceX = Mathf.Sin(rad) * 10;
            double addforceY = Mathf.Cos(rad) * 10;
            Vector2 shotVector = new Vector2((float)addforceX, (float)addforceY);

            PlayerScript.Rbody.AddForce(shotVector);*/

        }
        else
        {
            var Distance = (Player.transform.position - transform.position).normalized;
            var Angle = (Mathf.Atan2(Distance.y, Distance.x) * Mathf.Rad2Deg) + 90.0f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Angle);

            NowRotation = transform.rotation.z;
        }
    }
}
