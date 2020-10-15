﻿
// //                                     // //
// //   Author:宮本 早希                  // //
// //   リザルト画面トロフィー達成演出    // //
// //                                     // //


// // インクルードファイル的なやつ // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class CAwardEffect : MonoBehaviour
{
    // 達成エフェクト格納用
    private GameObject AwardEffectObject;


    // 変形用拡大縮小値
    private Vector3 Scale;


    // 広がる速度
    [SerializeField] float ExpandSpeed = 2.0f;

    // 拡大最大値
    [SerializeField] private float MaxScale = 1.0f;


    // 仮・取得判定
    private bool Get = false;
    private bool EffectRun = false;


    // // 初期化 // //
    void Start()
    {
        AwardEffectObject = (GameObject)Resources.Load("Effect_Award");
    }


    // // 更新 // //
    void Update()
    {
        // トランスフォーム取得
        Transform thisTransform = this.transform;

        // 仮・取得判定ＯＦＦなら
        if(!Get)
        {
            Scale = new Vector3(0.0f, 0.0f, 0.0f);
            thisTransform.localScale = Scale;
        }

        // 仮・取得判定ＯＮなら
        else
        {
            // 拡大させる
            Scale.x += ExpandSpeed * Time.deltaTime;
            Scale.y += ExpandSpeed * Time.deltaTime;


            // エフェクト生成（一回だけ）
            if (EffectRun)
            {
                Instantiate(AwardEffectObject, new Vector3(thisTransform.position.x, thisTransform.position.y, thisTransform.position.z), Quaternion.identity);
                EffectRun = false;
            }


            // 拡大しすぎないように調整
            if(Scale.x > MaxScale)
            {
                Scale.x = MaxScale;
            }

            if (Scale.y > MaxScale)
            {
                Scale.y = MaxScale;
            }

            // 代入
            thisTransform.localScale = Scale;
        }

        // 仮・取得判定ON
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Get = true;
            EffectRun = true;
        }
    }
}