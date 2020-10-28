
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
    public GameObject AwardEffectObject;


    // 変形用拡大縮小値
    private Vector3 Scale;


    // オブジェクト保存用空オブジェクトのtransform
    private Transform AwardPool;


    // 広がる速度
    public float ExpandSpeed = 2.0f;

    // 拡大最大値
    public  float MaxScale = 1.0f;

    // エフェクト発生フラグ
    private bool EffectRun;

    // エフェクト再生カウント
    private float RunTime;


    // // 初期化 // //
    void Start()
    {
        // エフェクト保存用のオブジェクトを生成する
        AwardPool = new GameObject("AwardEffect").transform;

        // 再生カウントリセット
        RunTime = 0.0f;

        // エフェクトオブジェクトがあったら
        if (AwardEffectObject != null)
        {
            // 再生フラグＯＮ
            EffectRun = true;
        }
    }


    // // 更新 // //
    void Update()
    {
        // トランスフォーム取得
        Transform thisTransform = this.transform;

        // 拡大させる
        Scale.x += ExpandSpeed * Time.deltaTime;
        Scale.y += ExpandSpeed * Time.deltaTime;


        // 再生フラグがＯＮなら
        if (EffectRun)
        {
            // エフェクト生成
            GetObject(AwardEffectObject, thisTransform.position, Quaternion.identity);

            // カウント開始
            RunTime += 1.0f * Time.deltaTime;


            // カウントを過ぎたら
            if (RunTime > 0.4f)
            {
                // カウントリセット
                RunTime = 0.0f;

                // 再生フラグＯＦＦ
                EffectRun = false;
            }

        }


        // 拡大しすぎないように調整
        if (Scale.x > MaxScale)
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


    // // ゲームオブジェクｔのアクティブ判別と生成 // //
    void GetObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in AwardPool)
        {
            // オブジェクトが非アクティブなら使いまわし
            if (!transform.gameObject.activeSelf)
            {
                transform.SetPositionAndRotation(pos, qua);
                transform.gameObject.SetActive(true);
                return;
            }
        }

        // 非アクティブなオブジェクトがなければ生成する
        Instantiate(obj, pos, qua, AwardPool);
    }
}
