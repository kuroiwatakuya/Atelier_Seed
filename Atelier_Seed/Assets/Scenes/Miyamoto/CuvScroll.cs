
// //                       // //
// //   Author:宮本 早希    // //
// //   uvスクロール        // //
// //                       // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// // クラス // //
public class CuvScroll : MonoBehaviour
{
    // 背景のオブジェクト取得用
    GameObject rawImage;

    // 背景のロウイメージ取得用
    RawImage Bg;

    // uvスクロールする値
    float move;


    // // 初期化 // /
    void Start()
    {
        // 背景オブジェクト検索
        rawImage = GameObject.Find("Background");

        
        // ロウイメージ取得
        Bg = rawImage.GetComponent<RawImage>();


        // スクロールする値のリセット
        move = 0.0f;


        // 背景uv値リセット
        Bg.uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
    }


    // // 更新 // //
    void Update()
    {
        // uvスクロール
        move += 0.025f * Time.deltaTime;

        // 移動値が１を越えたらリセット
        if (move > 1.0f)
        {
            move = 0.0f;
        }

        // 背景uv値を変更
        Bg.uvRect = new Rect(move, move, 1.0f, 1.0f);
    }
}
