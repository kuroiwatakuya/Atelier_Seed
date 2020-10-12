
// //                                    // //
// //   Author:宮本 早希                 // //
// //   プレイヤーの横側の当たり判定と   // //
// //   むにゅってなる挙動               // //
// //                                    // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class CPlayerCollision_Width : MonoBehaviour
{
    // 変形フラグ
    public bool Crush_Flag_Width;


    // プレイヤー取得関係
    GameObject Player;              // プレイヤーのオブジェクト
    CPlayerScript PlayerScript;     // CPlayerScript
    Vector2 PlayerVelocity;         // プレイヤーのVelocity
    Vector3 PlayerScale;            // プレイヤーのScale


    // // 初期化 // //
    void Start()
    {
        // プレイヤーのオブジェクトを検索して代入
        Player = GameObject.Find("Player");

        // CPlayerScript を取得
        PlayerScript = Player.GetComponent<CPlayerScript>();


        // 変形フラグＯＦＦ
        Crush_Flag_Width = false;        
    }


    // // 更新 // //
    void Update()
    {
        // プレイヤーの速度を取得
        PlayerVelocity = PlayerScript.Velocity;

        // プレイヤーの拡大縮小を取得
        PlayerScale = this.transform.parent.localScale;


        // 変形フラグがＯＮになっていたら
        if (Crush_Flag_Width)
        {
            // プレイヤーの速度が大きかったとき
            if (PlayerVelocity.x > 3.0f || PlayerVelocity.x < -3.0f ||
                PlayerVelocity.y > 3.0f || PlayerVelocity.y < -3.0f)
            {
                // 大きく変形する
                PlayerScale = CJellyBound.Crush_Width(playerscale, 0.25f, 0.5f);
            }


            // プレイヤーの速度がほぼなかったとき
            else if (PlayerVelocity.x < 1.0f && PlayerVelocity.x > -1.0f ||
                     PlayerVelocity.y < 1.0f && PlayerVelocity.y > -1.0f)
            {
                // 変形フラグをＯＦＦにする
                Crush_Flag_Width = false;
            }


            // プレイヤーの速度が小さかったとき
            else if (PlayerVelocity.x < 3.0f && PlayerVelocity.x > -3.0f ||
                     PlayerVelocity.y < 3.0f && PlayerVelocity.y > -3.0f)
            {
                // 小さく変形する
                PlayerScale = CJellyBound.Crush_Width(PlayerScale, 0.5f, 0.5f);
            }
        }

        // 変形フラグがＯＦＦになっていたら
        else
        {
            // 元の大きさに戻る
            PlayerScale = CJellyBound.Expand_Width(PlayerScale, 1.0f, 0.25f);
        }


        // 変形した（しなかった）ものをプレイヤーの拡大縮小として代入
        this.transform.parent.localScale = PlayerScale;
    }

    
    // // 当たったとき // //
    void OnTriggerEnter2D(Collider2D coll)
    {
        // 当たった先のタグが PlayerCrush なら
        if (coll.gameObject.tag == "PlayerCrush")
        {
            // 変形フラグＯＮ
            Crush_Flag_Width = true;
        }
    }


    // // 当たっていないとき // //
    void OnTriggerExit2D(Collider2D coll)
    {
        // 変形フラグＯＦＦ
        Crush_Flag_Width = false;
    }
}
