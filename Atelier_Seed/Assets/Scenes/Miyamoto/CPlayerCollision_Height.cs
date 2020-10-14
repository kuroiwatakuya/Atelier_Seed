
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
public class CPlayerCollision_Height : MonoBehaviour
{
    // 変形フラグ
    public bool Crush_Flag_Height;
    

    // プレイヤー取得関係
    GameObject Player;              // プレイヤーのオブジェクト
    CPlayerScript PlayerScript;     // CPlayerScript
    Vector2 PlayerVelocity;         // プレイヤーのVelocity
    Vector3 PlayerScale;            // プレイヤーのScale
    Vector3 PlayerInitialScale;     // プレイヤーの初期Scale記憶用


    // 速度調整用
    [SerializeField] private float Velocity_Max_Plus = 3.0f;    // 速度が大きい値（正方向）
    [SerializeField] private float Velocity_Max_Minus = -3.0f;  // 速度が大きい値（負方向）
    [SerializeField] private float Velocity_Min_Plus = 1.0f;    // 速度が小さい値（正方向）
    [SerializeField] private float Velocity_Min_Minus = -1.0f;  // 速度が小さい値（負方向）


    // 潰す大きさ調整用
    [SerializeField] private float CrushPower = 50.0f;          // 一度に潰す量
    [SerializeField] private float CrushMin_Larger = 0.75f;     // 大きく潰すときの最低値
    [SerializeField] private float CrushMin_Smaller = 1.0f;     // あまり潰さないときの最低値


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
        Crush_Flag_Height = false;        
    }


    // // 更新 // //
    void Update()
    {
        // プレイヤーの速度を取得
        PlayerVelocity = PlayerScript.Velocity;

        // プレイヤーの拡大縮小を取得
        PlayerScale = this.transform.parent.localScale;


        // 変形フラグがＯＮになっていたら
        if (Crush_Flag_Height)
        {
            // プレイヤーの速度が大きかったとき
            if (PlayerVelocity.x >= Velocity_Max_Plus || PlayerVelocity.x <= Velocity_Max_Minus ||
                PlayerVelocity.y >= Velocity_Max_Plus || PlayerVelocity.y <= Velocity_Max_Minus)
            {
                // 大きく変形する
                PlayerScale = CJellyBound.Crush_Height(PlayerScale, CrushMin_Larger, CrushPower);
            }


            // プレイヤーの速度がほぼなかったとき
            else if (PlayerVelocity.x < Velocity_Min_Plus && PlayerVelocity.x > Velocity_Min_Minus ||
                     PlayerVelocity.y < Velocity_Min_Plus && PlayerVelocity.y > Velocity_Min_Minus)
            {
                // 変形フラグをＯＦＦにする
                Crush_Flag_Height = false;
            }


            // プレイヤーの速度が小さかったとき
            else if (PlayerVelocity.x < Velocity_Max_Plus && PlayerVelocity.x > Velocity_Max_Minus ||
                     PlayerVelocity.y < Velocity_Max_Plus && PlayerVelocity.y > Velocity_Max_Minus)
            {
                // 小さく変形する
                PlayerScale = CJellyBound.Crush_Height(PlayerScale, CrushMin_Smaller, CrushPower);
            }
        }

        // 変形フラグがＯＦＦになっていたら
        else
        {
            // 元の大きさに戻る
            PlayerScale = CJellyBound.Expand_Height(PlayerScale, PlayerInitialScale.y, CrushPower);
        }


        // 変形した（しなかった）ものをプレイヤーの拡大縮小として代入
        this.transform.parent.localScale = PlayerScale;
    }


    // // 当たったとき // //
    void OnTriggerEnter2D(Collider2D coll)
    {
        // 当たった先が PlayerCrush だったら
        if (coll.gameObject.tag == "PlayerCrush")
        {
            // 変形フラグＯＮ
            Crush_Flag_Height = true;
        }
    }


    // // 当たっていないとき // //
    void OnTriggerExit2D(Collider2D coll)
    {
        // 変形フラグＯＦＦ
        Crush_Flag_Height = false;
    }
}
