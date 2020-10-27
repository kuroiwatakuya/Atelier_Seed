
// //                                       // //
// //   Author:宮本 早希                    // //
// //   トロフィー取得時の演出              // //
// //   ついでに常に上下に揺れる演出付き    // //
// //                                       // //


// // インクルードファイル // //
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// // クラス // //
public class CTrophyGetEffect : MonoBehaviour
{
    // トランスフォーム取得用
    private Transform thisTransform;

    // スプライトレンダラー取得用
    private SpriteRenderer thisSpriteRenderer;


    // 現在座標（ｙ値のみ）
    private float nowPos;

    // 現在拡大縮小値
    private Vector3 nowScale;


    // 当たったか判定
    private bool Hit = false;

    // 横に伸びるフラグ
    private bool Yoko = false;

    // 縦に伸びるフラグ
    private bool Tate = false;


    // フェードの速さ
    private float FadeTime = 0.5f;

    // フェード計測用
    private float CurrentRemainTime;

    // ------------------------------------------------------------------------------------------------

    // // 初期化 // //
    void Start()
    {
        // トランスフォーム取得
        thisTransform = this.transform;

        // 現在座標（ｙ値のみ）取得
        nowPos = thisTransform.position.y;

        // レンダラー取得
        thisSpriteRenderer = GetComponent<SpriteRenderer>();


        // 判定ＯＦＦ
        Hit = false;    // 当たったか
        Yoko = false;   // 横に伸びる
        Tate = false;   // 縦に伸びる


        // フェードする残り時間をフェードの速さに設定
        CurrentRemainTime = FadeTime;
    }

    // ------------------------------------------------------------------------------------------------

    // // 更新 // //
    void Update()
    {
        // 現在拡大縮小値を取得
        nowScale = thisTransform.localScale;


        // 当たってないとき
        if (!Hit)
        {
            // 上下にふわふわ揺れる
            thisTransform.position = new Vector3(thisTransform.position.x,
                                                 nowPos + Mathf.PingPong(Time.time / 3, 0.2f),
                                                 thisTransform.position.z);
        }

        // 当たったとき
        else
        {
            // 横に伸びるフラグが立つ
            Yoko = true;

            // 横に伸びるフラグが立っていて、縦に伸びるフラグが立っていないとき
            if (Yoko && !Tate)
            {
                // 横に伸ばす
                nowScale.x += 5.0f * Time.deltaTime;
                nowScale.y -= 5.0f * Time.deltaTime;

                // 伸ばした拡大縮小値を代入
                thisTransform.localScale = nowScale;

                // ある程度伸びたら
                if (nowScale.x > 2.0f)
                {
                    // 縦に伸びるフラグが立つ
                    Tate = true;
                }
            }

            // 縦に伸びるフラグが立ったとき
            if (Tate)
            {
                // 縦に伸ばす
                nowScale.x -= 5.0f * Time.deltaTime;
                nowScale.y += 5.0f * Time.deltaTime;

                // 伸ばした拡大縮小値を代入
                thisTransform.localScale = nowScale;


                // フェード用時間処理
                CurrentRemainTime -= Time.deltaTime;

                // 時間調整
                if (CurrentRemainTime < 0.0f)
                {
                    CurrentRemainTime = 0.0f;
                }


                // フェード処理
                float alpha = CurrentRemainTime / FadeTime;
                var color = thisSpriteRenderer.color;
                color.a = alpha;
                thisSpriteRenderer.color = color;


                // 消えた後の処理
                if (nowScale.y > 2.0f)
                {
                    Tate = false;
                    Yoko = false;
                    Hit = false;
                    return;
                }
            }
        }
    }

    // ------------------------------------------------------------------------------------------------

    // // 当たった判定 // //
    void OnTriggerEnter2D(Collider2D coll)
    {
        // 当たった先のタグが Player なら
        if (coll.gameObject.tag == "Player")
        {
            // 当たったフラグON
            Hit = true;
        }
    }
}
