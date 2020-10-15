
// //                               // //
// //   Author:宮本 早希            // //
// //   タップエフェクト呼出関数    // //
// //                               // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class CTapEffect : MonoBehaviour
{
    // タップエフェクトプレハブ格納用ゲームオブジェクト
    private GameObject TapEffectObject;


    // // 初期化 // //
    void Start()
    {
        // タップエフェクトプレハブを検索して代入
        TapEffectObject = (GameObject)Resources.Load("Effect_Tap_RandS");
    }


    // // 更新 // //
    void Update()
    {
        // マウスカーソル位置取得
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 MousePos = Input.mousePosition;     // マウス座標を取得
            MousePos.z = 5.0f;                          // ｚ座標値補間


            // マウスカーソルがいるところの座標をエフェクト座標として設定
            Vector3 EffectPos = Camera.main.ScreenToWorldPoint(MousePos);


            // エフェクトを生成
            Instantiate(TapEffectObject, new Vector3(EffectPos.x, EffectPos.y, EffectPos.z), Quaternion.identity);
        }
    }
}
