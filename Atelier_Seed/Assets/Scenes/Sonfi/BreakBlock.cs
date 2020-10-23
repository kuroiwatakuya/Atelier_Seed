using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakBlock : MonoBehaviour
{

    public GameObject Player;
    //---宮本加筆ここから------------------------------
    [SerializeField] private GameObject BreakEffect;    // 破壊エフェクトオブジェクト
    private Vector3 EffectPosition;                     // エフェクト座標
    //---宮本加筆ここまで------------------------------
    public float Power;

    //プレイヤースクリプト
    private CPlayerScript PlayerScript;

    private bool BreakFlag;

    // Start is called before the first frame update
    void Start()
    {
        PlayerScript = Player.GetComponent<CPlayerScript>();
        BreakFlag = false;

        //---宮本加筆ここから------------------------------
        if (SceneManager.GetActiveScene().name == "Stage3")
        {
            BreakEffect = (GameObject)Resources.Load("Effect_BreakCookies");
        }
        if (SceneManager.GetActiveScene().name == "Stage4")
        {
            BreakEffect = (GameObject)Resources.Load("Effect_BreakPipe");
        }
        if (SceneManager.GetActiveScene().name == "Stage5")
        {
            BreakEffect = (GameObject)Resources.Load("Effect_BreakTree");
        }
        //---宮本加筆ここまで------------------------------
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(BreakFlag)
        {
            BreakFlag = false;
            PlayerScript.BreakBlockCount++;
            //---宮本加筆ここから------------------------------
            EffectPosition = this.transform.position;
            Instantiate(BreakEffect, EffectPosition, Quaternion.identity);
            //---宮本加筆ここまで------------------------------
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && (PlayerScript.Velocity.x >= Power || PlayerScript.Velocity.x >= Power))
        {
            BreakFlag = true;
        }
    }
}
