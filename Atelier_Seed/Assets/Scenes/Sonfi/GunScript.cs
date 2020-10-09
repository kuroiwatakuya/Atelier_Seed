using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    //プレイヤー
    private GameObject Player;

    private CPlayerScript PlayerScript;

    public float Angle  = 1;

    public bool Plus = true;
    public bool Minus = false;

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
        if (PlayerScript.GunFlag)
        {

            Quaternion Rot = Quaternion.AngleAxis(Angle, Vector3.forward);
            Quaternion NowRot = this.transform.rotation;
            this.transform.rotation = NowRot * Rot;
            if(transform.rotation.z >= 90)
            {
                Plus = false;
                Minus = true;
            }
            if (transform.rotation.z <= 0)
            {
                Plus = true;
                Minus = false;
            }
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
