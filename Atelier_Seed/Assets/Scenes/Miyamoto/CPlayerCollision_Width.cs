
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


    // 速度調整用
//    [SerializeField] private float Velocity_Max_Plus = 3.0f;    // 速度が大きい値（正方向）
//    [SerializeField] private float Velocity_Max_Minus = -3.0f;  // 速度が大きい値（負方向）
//    [SerializeField] private float Velocity_Min_Plus = 1.0f;    // 速度が小さい値（正方向）
//    [SerializeField] private float Velocity_Min_Minus = -1.0f;  // 速度が小さい値（負方向）


    // 潰す大きさ調整用
    [SerializeField] private float CrushPower = 25.0f;  // 一度に潰す量
    [SerializeField] private float CrushMin = 1.0f;     // 大きく潰すときの最低値
//    [SerializeField] private float CrushMin_Smaller = 1.0f;     // あまり潰さないときの最低値

    float count;


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

        count = 0;
    }


    // // 更新 // //
    void Update()
    {
        // プレイヤーの速度を取得
//        PlayerVelocity = PlayerScript.Velocity;

        // プレイヤーの拡大縮小を取得
        PlayerScale = this.transform.parent.localScale;

        if (Crush_Flag_Width)
        {
            if (PlayerVelocity.y > 1.0f || PlayerVelocity.y < -1.0f)
            {
                PlayerScale = CJellyBound.Crush_Width(PlayerScale, CrushMin, CrushPower);
            }

            count += 1.0f * Time.deltaTime;

            if (count > 0.1)
            {
                Crush_Flag_Width = false;
                count = 0;
            }
        }
        else
        {
            PlayerScale = CJellyBound.Expand_Width(PlayerScale, PlayerInitialScale.x, CrushPower);
        }

/*
        // 変形フラグがＯＮになっていたら
        if (Crush_Flag_Width)
        {
            // プレイヤーの速度が大きかったとき
            if (PlayerVelocity.x >= Velocity_Max_Plus || PlayerVelocity.x <= Velocity_Max_Minus ||
                PlayerVelocity.y >= Velocity_Max_Plus || PlayerVelocity.y <= Velocity_Max_Minus)
            {
                // 大きく変形する
                PlayerScale = CJellyBound.Crush_Width(PlayerScale, CrushMin_Larger, CrushPower);
                Debug.Log("変形：横：大");
            }


            // プレイヤーの速度がほぼなかったとき
            else if (PlayerVelocity.x < Velocity_Min_Plus && PlayerVelocity.x > Velocity_Min_Minus ||
                     PlayerVelocity.y < Velocity_Min_Plus && PlayerVelocity.y > Velocity_Min_Minus)
            {
                // 変形フラグをＯＦＦにする
                Crush_Flag_Width = false;
            }


            // プレイヤーの速度が小さかったとき
            else if (PlayerVelocity.x < Velocity_Max_Plus && PlayerVelocity.x > Velocity_Max_Minus ||
                     PlayerVelocity.y < Velocity_Max_Plus && PlayerVelocity.y > Velocity_Max_Minus)
            {
                // 小さく変形する
                PlayerScale = CJellyBound.Crush_Width(PlayerScale, CrushMin_Smaller, CrushPower);
                Debug.Log("変形：横：小");
            }
        }

        // 変形フラグがＯＦＦになっていたら
        else
        {
            // 元の大きさに戻る
            PlayerScale = CJellyBound.Expand_Width(PlayerScale, PlayerInitialScale.x, CrushPower);
            if (PlayerScale.x > PlayerInitialScale.x)
            {
                PlayerScale = CJellyBound.Expand_Width(PlayerScale, PlayerInitialScale.x, CrushPower);
                Debug.Log("変形：横：戻る");
            }
        }
*/

        // 変形した（しなかった）ものをプレイヤーの拡大縮小として代入
        this.transform.parent.localScale = PlayerScale;
    }


    // // 当たったとき // //
    void OnTriggerEnter2D(Collider2D coll)
    {
        // 当たった先のタグが PlayerCrush なら
   //     if (coll.gameObject.tag == "PlayerCrush")
        {
            // 変形フラグＯＮ
            Crush_Flag_Width = true;
        }
    }


    // // 当たっていないとき // //
    void OnTriggerExit2D(Collider2D coll)
    {
        // 変形フラグＯＦＦ
   //     Crush_Flag_Width = false;
    }
}
