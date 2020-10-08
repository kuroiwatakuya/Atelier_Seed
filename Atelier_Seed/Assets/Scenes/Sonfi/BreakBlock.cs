using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{

    //************
    //対象タグ
    //************
    public string PlayerTag;

    private GameObject Player;

    //プレイヤースクリプト
    private CPlayerScript PlayerScript;

    private float BreakSpeed = 10;

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == PlayerTag && PlayerScript.Velocity.y>BreakSpeed && PlayerScript.Velocity.x > BreakSpeed)
        {
            Destroy(this.gameObject);
        }
    }
}
