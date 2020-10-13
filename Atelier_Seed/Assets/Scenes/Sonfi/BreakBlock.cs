using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{

    //************
    //対象タグ
    //************
    public string PlayerTag;

    public GameObject Player;

    public bool Flag = false;

    //プレイヤースクリプト
    private CPlayerScript PlayerScript;

    private float BreakSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = Player.GetComponent<CPlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Flag = true;
            Destroy(this.gameObject);
        }
    }
}
