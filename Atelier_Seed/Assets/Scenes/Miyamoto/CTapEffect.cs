
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

    // オブジェクト保存用空オブジェクトのtransform;
    private Transform Tapool;    


    // // 初期化 // //
    void Start()
    {
        // タップエフェクトプレハブを検索して代入
        TapEffectObject = (GameObject)Resources.Load("Effect_Tap_RandS");
        
        // タップエフェクトのオブジェクトを生成する
        Tapool = new GameObject("Tap").transform;
    }


    // // 更新 // //
    void Update()
    {
        // エディタで実行中
        if(Application.isEditor)
        {
            // マウスカーソル位置取得
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 MousePos = Input.mousePosition;     // マウス座標を取得
                MousePos.z = 5.0f;                          // ｚ座標値補間


                // マウスカーソルがいるところの座標をエフェクト座標として設定
                Vector3 EffectPos = Camera.main.ScreenToWorldPoint(MousePos);


                // エフェクト再生
                GetObject(TapEffectObject, EffectPos, Quaternion.identity);
            }
        }

        // 実機で実行中
        else
        {
            if (Input.touchCount > 0)
            {
                // タッチ情報取得
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 TouchPos = touch.position;
                    TouchPos.z = 5.0f;

                    //タッチ座標をエフェクト座標として設定
                    Vector3 EffPos = Camera.main.ScreenToWorldPoint(TouchPos);

                    // エフェクト再生
                    GetObject(TapEffectObject, EffPos, Quaternion.identity);
                }
            }
        }

    }

    // // ゲームオブジェクトのアクティブ判別と生成 // //
    void GetObject(GameObject obj, Vector3 pos, Quaternion qua)
    {
        foreach (Transform transform in Tapool)
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
        Instantiate(obj, pos, qua, Tapool);
    }

}
