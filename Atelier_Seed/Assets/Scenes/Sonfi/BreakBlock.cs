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
    AudioSource BreakBlocl_Source;

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
            //---宮本加筆ここから------------------------------
            EffectPosition = this.transform.position;
            Instantiate(BreakEffect, EffectPosition, Quaternion.identity);
            //---宮本加筆ここまで------------------------------
            this.gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && (PlayerScript.Velocity.x >= Power || PlayerScript.Velocity.y >= Power || PlayerScript.Velocity.x <= Power || PlayerScript.Velocity.y <= Power))
        {
            BreakFlag = true;
        }
    }
}
