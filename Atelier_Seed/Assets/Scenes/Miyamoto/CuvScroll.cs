
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
    RawImage Bg;
    GameObject rawImage;
    float move;
    Rect scroll;

    // // 初期化 // /
    void Start()
    {
        rawImage = GameObject.Find("Background");
        Bg = rawImage.GetComponent<RawImage>();

        move = 0.0f;

        Bg.uvRect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
    }


    // // 更新 // //
    void Update()
    {
        move += 0.025f * Time.deltaTime;
        if (move > 1.0f)
        {
            move = 0.0f;
        }

        Bg.uvRect = new Rect(move, move, 1.0f, 1.0f);
    }
}
