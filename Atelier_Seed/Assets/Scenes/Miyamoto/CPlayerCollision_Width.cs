
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
    Vector3 PlayerInitialScale;     // プレイヤーの初期Scale記憶用
    


    // 潰す大きさ調整用
    [SerializeField] private float CrushPower = 25.0f;  // 一度に潰す量
    [SerializeField] private float CrushMin = 1.0f;     // 潰す最低値


    // 変形時間
    float Count;


    // // 初期化 // //
    void Start()
    {
        // プレイヤーのオブジェクトを検索して代入
        Player = GameObject.Find("Player");

        // CPlayerScript を取得
        PlayerScript = Player.GetComponent<CPlayerScript>();

        // プレイヤーの初期拡大縮小値記憶
        PlayerInitialScale = this.transform.parent.localScale;


        // 変形フラグＯＦＦ
        Crush_Flag_Width = false;


        // 変形時間リセット
        Count = 0;
    }


    // // 更新 // //
    void Update()
    {
        // プレイヤーの拡大縮小を取得
        PlayerScale = this.transform.parent.localScale;


        // 変形フラグがONなら
        if (Crush_Flag_Width)
        {
            // スピードが遅くないとき
            if (PlayerVelocity.y > 1.0f || PlayerVelocity.y < -1.0f)
            {
                // 変形する
                PlayerScale = CJellyBound.Crush_Width(PlayerScale, CrushMin, CrushPower);
            }

            // 変形時間
            Count += 1.0f * Time.deltaTime;

            // 変形時間が終わったら
            if (Count > 0.1f)
            {
                // 変形フラグOFF
                Crush_Flag_Width = false;

                // 変形時間リセット
                Count = 0;
            }
        }

        // 変形フラグOFFの時
        else
        {
            // 元に戻す
            PlayerScale = CJellyBound.Expand_Width(PlayerScale, PlayerInitialScale.x, CrushPower);
        }


        // 変形した（しなかった）ものをプレイヤーの拡大縮小として代入
        this.transform.parent.localScale = PlayerScale;
    }


    // // 当たったとき // //
    void OnTriggerEnter2D(Collider2D coll)
    {
        // 変形フラグＯＮ
        Crush_Flag_Width = true;
    }    
}
