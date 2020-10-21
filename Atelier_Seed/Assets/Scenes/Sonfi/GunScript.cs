using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    //プレイヤー
    public GameObject Player;

    public CPlayerScript PlayerScript;

    private float Angle = -1;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.Find("Player");
        //PlayerScript = Player.GetComponent<CPlayerScript>();
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
            if (Angle == 1 && gameObject.transform.eulerAngles.z >= 90)
            {
                Angle *= -1;
            }
            if (Angle == -1 && gameObject.transform.eulerAngles.z <= -90)
            {
                Angle *= -1;
            }
        }
        if(!PlayerScript.GunFlag && !PlayerScript.CoolTime)
        {
            var Distance = (Player.transform.position - transform.position).normalized;
            var Angle = (Mathf.Atan2(Distance.y, Distance.x) * Mathf.Rad2Deg) + 90.0f;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Angle);
        }
    }
}
