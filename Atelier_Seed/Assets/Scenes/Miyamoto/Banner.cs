
// //                       // //
// //   Author;宮本早希     // //
// //   バナー出現演出      // //
// //                       // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// // クラス // //
public class Banner : MonoBehaviour
{
    // UI の transform
    private RectTransform thisTransform;

    // 移動用
    private Vector3 Move;

    // ストップカウンタ
    private float Count;

    // バッジ取得フラグ
    public bool Get;


    // バナー移動フラグ
    private bool Down;
    private bool Up;


    // // 初期化 // //
    void Start()
    {
        // 取得フラグＯＦＦ
        Get = false;

        // 移動用リセット
        Move = new Vector3(0.0f, 0.0f, 0.0f);


        // 移動フラグＯＦＦ
        Down = false;
        Up = false;
    }


    // // 更新 // //
    void Update()
    {
        // RectTransform取得
        thisTransform = GameObject.Find("Banner").GetComponent<RectTransform>();

        // 座標取得
        Move = thisTransform.localPosition;


        // // 仮・取得フラグＯＮ // //
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Get = true;
        }


        // 取得したら
        if (Get)
        {
            // 下に動くＯＮ
            Down = true;


            // 下に動くＯＮかつ上に動くＯＦＦ
            if (Down && !Up)
            {
                Move.y -= 200.0f * Time.deltaTime;
            }


            // 下限に来たら
            if (Move.y < 270)
            {
                // 下限に設定
                Move.y = 270.0f;

                // ストップ用カウンタ加算
                Count += 1.0f * Time.deltaTime;
            }


            // カウンタが一定になったら
            if (Count > 1)
            {
                // 上に動くＯＮ、下に動くＯＦＦ
                Up = true;
                Down = false;
            }


            // 上に動く
            if (Up)
            {
                Move.y += 200.0f * Time.deltaTime;
            }


            // 上限に達したら
            if (Move.y > 420)
            {
                // 上限で止める
                Move.y = 410.0f;

                // 移動フラグOFF
                Up = false;
                Get = false;

                // カウンターリセット
                Count = 0.0f;
            }
        }


        // 移動を適用
        thisTransform.localPosition = Move;
    }
}
