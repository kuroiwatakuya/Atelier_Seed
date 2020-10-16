using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{

    public GameObject Player;

    public float Power;

    //プレイヤースクリプト
    private CPlayerScript PlayerScript;

    private bool BreakFlag;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = Player.GetComponent<CPlayerScript>();
        BreakFlag = false;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(BreakFlag)
        {
            BreakFlag = false;
            PlayerScript.BreakBlockCount++;
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            BreakFlag = true;
        }
    }
}
