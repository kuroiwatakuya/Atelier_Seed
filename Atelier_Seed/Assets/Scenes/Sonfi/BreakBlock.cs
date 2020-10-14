using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock : MonoBehaviour
{

    public GameObject Player;

    public float Power;

    //プレイヤースクリプト
    private CPlayerScript PlayerScript;

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
            Destroy(this.gameObject);
        }
    }
}
