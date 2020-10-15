
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
    GameObject rawImage;
    float move;

    // // 初期化 // /
    void Start()
    {
        rawImage = GameObject.Find("Background");

        move = 0.0f;
    }


    // // 更新 // //
    void Update()
    {
        move += 0.025f * Time.deltaTime;
        if (move > 1.0f)
        {
            move = 0.0f;
        }

        rawImage.GetComponent<RawImage>().uvRect = new Rect(move, move, 1.0f, 1.0f);
    }
}
